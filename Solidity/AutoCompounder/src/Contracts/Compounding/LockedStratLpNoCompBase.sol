// SPDX-License-Identifier: AGPL-3.0-or-later
pragma solidity ^0.8.11;

import "../../Libraries/@openzeppelin/v4.4/token/ERC20/IERC20.sol";
import "../../Libraries/@openzeppelin/v4.4/utils/math/SafeMath.sol";
import "../../Interfaces/External/UniswapV2RouterEth.sol";
import "../../Interfaces/External/IUniswapV2Pair.sol";
import "../../Interfaces/External/IMasterChef.sol";

import "./LockedStratBase.sol";

contract LockedStratLpNoCompBase is LockedStratBase {
    using SafeERC20 for IERC20;
    using SafeMath for uint256;

    address[] internal rewardToKeepTokenRoute;

    address internal chefAddress;
    uint256 internal poolId;

    address internal keepTokenAddress;

    IUniswapV2RouterEth internal uniswapV2RouterEth;

    constructor(
        address _underlyingAssetAddress,
        address _rewardAssetAddress,
        address _unirouterAddress,
        address _chefAddress,
        uint256 _poolId,
        bool _keepToken0
    ) LockedStratBase (
        _underlyingAssetAddress,
        _rewardAssetAddress
    ) {
        chefAddress = _chefAddress;
        poolId = _poolId;

        keepTokenAddress = _keepToken0 ?
            IUniswapV2Pair(underlyingAssetAddress).token0() :
            IUniswapV2Pair(underlyingAssetAddress).token1();

        uniswapV2RouterEth = IUniswapV2RouterEth(_unirouterAddress);

        if (_rewardAssetAddress == uniswapV2RouterEth.WETH()
            || keepTokenAddress == uniswapV2RouterEth.WETH()
        ) {
            rewardToKeepTokenRoute = [
                _rewardAssetAddress,
                keepTokenAddress
            ];

        } else {
            rewardToKeepTokenRoute = [
                _rewardAssetAddress,
                uniswapV2RouterEth.WETH(),
                keepTokenAddress
            ];
        }

        _giveAllowances();
    }

    function getDeployedBalance() override virtual public view returns (uint256 amount) {
        (amount, ) = IMasterChef(chefAddress).userInfo(poolId, address(this));
    }

    function getPendingRewardAmount() override virtual external view returns (uint256) {
        // Not yet implemented.
        return 0;
    }

    function panic() override virtual external onlyOwner {
        IMasterChef(chefAddress).emergencyWithdraw( poolId );
        _removeAllowances();
    }

    function unpanic() override virtual external onlyOwner {
        _giveAllowances();
        deploy();
    }

    function retire() override virtual external onlyOwner {
        IMasterChef(chefAddress).withdraw( poolId, getDeployedBalance() );
        withdrawAllUndeployed();

        // If .retire() starts failing, send some keepToken to the contract.
        IERC20 keepTokenContract = IERC20(keepTokenAddress);
        keepTokenContract.safeTransfer( msg.sender, keepTokenContract.balanceOf(address(this)) );

        selfdestruct(payable(msg.sender));
    }

    function withdrawAll() override virtual external onlyOwner {
        IMasterChef(chefAddress).withdraw( poolId, getDeployedBalance() );

        IERC20 underlyingAssetContract = IERC20(underlyingAssetAddress);
        underlyingAssetContract.safeTransfer( msg.sender, underlyingAssetContract.balanceOf(address(this)) );

        IERC20 keepTokenContract = IERC20(keepTokenAddress);
        keepTokenContract.safeTransfer( msg.sender, keepTokenContract.balanceOf(address(this)) );
    }

    function withdraw(uint256 _amount) override virtual external onlyOwner {
        IERC20 underlyingAssetContract = IERC20(underlyingAssetAddress);
        uint256 underlyingBal = underlyingAssetContract.balanceOf(address(this));

        if (underlyingBal < _amount) {
            IMasterChef(chefAddress).withdraw( poolId, _amount.sub(underlyingBal) );
            underlyingBal = underlyingAssetContract.balanceOf( address(this) );
        }

        if (underlyingBal > _amount) {
            underlyingBal = _amount;
        }

        underlyingAssetContract.safeTransfer( msg.sender, underlyingBal );

        // If this starts failing, send some keepToken to the contract.
        IERC20 keepTokenContract = IERC20(keepTokenAddress);
        keepTokenContract.safeTransfer( msg.sender, keepTokenContract.balanceOf(address(this)) );
    }

    function deploy() override virtual public onlyOwner {
        IMasterChef(chefAddress).deposit( poolId, IERC20(underlyingAssetAddress).balanceOf( address(this) ) );
    }

    /// @dev A check to know if there is a profitable reward should be done off-chain.
    function execute() override virtual external {
        IMasterChef(chefAddress).withdraw( poolId, 0 );

        uniswapV2RouterEth.swapExactTokensForTokensSupportingFeeOnTransferTokens(
            IERC20(rewardAssetAddress).balanceOf(address(this)),
            0,
            rewardToKeepTokenRoute,
            address(this),
            block.timestamp
        );
    }

    function _giveAllowances() virtual internal {
        IERC20(underlyingAssetAddress).safeApprove(chefAddress, type(uint256).max);
        IERC20(rewardAssetAddress).safeApprove(address(uniswapV2RouterEth), type(uint256).max);
    }

    function _removeAllowances() virtual internal {
        IERC20(underlyingAssetAddress).safeApprove(chefAddress, 0);
        IERC20(rewardAssetAddress).safeApprove(address(uniswapV2RouterEth), 0);
    }

}
