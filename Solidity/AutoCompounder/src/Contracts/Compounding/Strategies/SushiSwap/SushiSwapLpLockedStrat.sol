// SPDX-License-Identifier: AGPL-3.0-or-later
pragma solidity ^0.8.11;

import "../../../../Libraries/@openzeppelin/v4.4/token/ERC20/IERC20.sol";
import "../../../../Libraries/@openzeppelin/v4.4/utils/math/SafeMath.sol";

import "../../../../Interfaces/External/IUniswapV2Pair.sol";
import "../../../../Interfaces/External/UniswapV2RouterEth.sol";
import "../../../../Interfaces/External/SushiSwap/IMiniChefV2.sol";

import "../../LockedStratLpBase.sol";

contract SushiSwapLpLockedStrat is LockedStratLpBase {
    using SafeERC20 for IERC20;
    using SafeMath for uint256;

    address private nativeAssetAddress;
    address[] private nativeToRewardRoute;

    address private chefAddress;
    IMiniChefV2 private miniChefV2;
    uint256 private poolId;

    /// @param _poolId The index of the pool (inside IMiniChefV2's `.poolInfo`).
    constructor(
        address _underlyingAssetAddress,
        address _rewardAssetAddress,
        address _nativeAssetAddress,
        uint256 _poolId,
        address _chefAddress,
        address _unirouterAddress
    ) LockedStratLpBase(
        _underlyingAssetAddress,
        _rewardAssetAddress,
        _unirouterAddress
    )
    {
        nativeAssetAddress = _nativeAssetAddress;
        nativeToRewardRoute = [_nativeAssetAddress, _rewardAssetAddress];

        chefAddress = _chefAddress;
        miniChefV2 = IMiniChefV2(_chefAddress);
        poolId = _poolId;

        _giveAllowances();
    }

    function getDeployedBalance() override public view returns (uint256) {
        (uint256 _amount, ) = miniChefV2.userInfo(poolId, address(this));
        return _amount;
    }

    function getPendingRewardAmount() override external view returns (uint256) {
        return miniChefV2.pendingSushi( poolId, address(this) );
    }

    function panic() override external onlyOwner {
        miniChefV2.emergencyWithdraw(poolId, msg.sender);
        _removeAllowances();
    }

    function unpanic() override external onlyOwner {
        _giveAllowances();
    }

    function retire() override external onlyOwner {
        miniChefV2.withdrawAndHarvest( poolId, getDeployedBalance(), msg.sender );

        address payable ownerAddy = payable(msg.sender);
        selfdestruct(ownerAddy);
    }

    function withdrawAll() override external onlyOwner {
        IERC20 underlyingAssetContract = IERC20(underlyingAssetAddress);
        underlyingAssetContract.safeTransfer( msg.sender, underlyingAssetContract.balanceOf(address(this)) );

        miniChefV2.withdrawAndHarvest(poolId, getDeployedBalance(), msg.sender);
    }

    function withdraw(uint256 _amount) override external onlyOwner {
        IERC20 underlyingAssetContract = IERC20(underlyingAssetAddress);
        uint256 underlyingBal = underlyingAssetContract.balanceOf(address(this));

        if (underlyingBal < _amount) {
            miniChefV2.withdraw( poolId, _amount.sub(underlyingBal), address(this) );
            underlyingBal = underlyingAssetContract.balanceOf(address(this));
        }

        if (underlyingBal > _amount) {
            underlyingBal = _amount;
        }

        underlyingAssetContract.safeTransfer(msg.sender, underlyingBal);
    }

    function execute() override external {
        miniChefV2.harvest(poolId, address(this));

        // SushiSwap's v2 rewards in the native token too.
        uint256 toOutput = IERC20(nativeAssetAddress).balanceOf(address(this));

        if (toOutput > 0) {
            uniswapV2RouterEth.swapExactTokensForTokens(
                toOutput, 0, nativeToRewardRoute, address(this), block.timestamp
            );
        }

        addLiquidity();

        uint256 underlyingBalance = IERC20(underlyingAssetAddress).balanceOf(address(this));

        if (underlyingBalance > 0) {
            miniChefV2.deposit(poolId, underlyingBalance, address(this));
        }
    }

    function _giveAllowances() private {
        address uniswapV2RouterEthAddress = address(uniswapV2RouterEth);

        IERC20(underlyingAssetAddress).safeApprove(chefAddress, type(uint256).max);

        IERC20(rewardAssetAddress).safeApprove(uniswapV2RouterEthAddress, type(uint256).max);
        // Needed for the SushiSwap v2 harvester.
        IERC20(nativeAssetAddress).safeApprove(uniswapV2RouterEthAddress, type(uint256).max);

        IERC20(lpToken0).safeApprove(uniswapV2RouterEthAddress, 0);
        IERC20(lpToken0).safeApprove(uniswapV2RouterEthAddress, type(uint256).max);

        IERC20(lpToken1).safeApprove(uniswapV2RouterEthAddress, 0);
        IERC20(lpToken1).safeApprove(uniswapV2RouterEthAddress, type(uint256).max);
    }

    function _removeAllowances() private {
        address uniswapV2RouterEthAddress = address(uniswapV2RouterEth);

        IERC20(underlyingAssetAddress).safeApprove(chefAddress, 0);
        IERC20(rewardAssetAddress).safeApprove(uniswapV2RouterEthAddress, 0);
        IERC20(nativeAssetAddress).safeApprove(uniswapV2RouterEthAddress, 0);
        IERC20(lpToken0).safeApprove(uniswapV2RouterEthAddress, 0);
        IERC20(lpToken1).safeApprove(uniswapV2RouterEthAddress, 0);
    }
}
