// SPDX-License-Identifier: AGPL-3.0-or-later
pragma solidity ^0.8.11;

import "../../Libraries/@openzeppelin/v4.4/token/ERC20/IERC20.sol";
import "../../Libraries/@openzeppelin/v4.4/token/ERC20/utils/SafeERC20.sol";
import "../../Libraries/@openzeppelin/v4.4/utils/math/SafeMath.sol";

import "../../Interfaces/Core/Compounding/ILockedStrat.sol";
import "./LockedStratVault.sol";

abstract contract LockedStratBase is ILockedStrat, LockedStratVault {
    using SafeERC20 for IERC20;
    using SafeMath for uint256;

    address internal rewardAssetAddress;

    constructor(
        address _underlyingAssetAddress,
        address _rewardAssetAddress
    ) LockedStratVault(_underlyingAssetAddress) {
        rewardAssetAddress = _rewardAssetAddress;
    }

    function getTvl() external view returns (uint256) {
        return getUndeployedBalance().add( getDeployedBalance() );
    }

    function getDeployedBalance() virtual public view returns (uint256) {
        // Not yet implemented.
        return 0;
    }

    function getPendingRewardAmount() virtual external view returns (uint256) {
        // Not yet implemented.
        return 0;
    }

    function panic() virtual external onlyOwner {
        IERC20 underlyingAssetContract = IERC20(underlyingAssetAddress);
        underlyingAssetContract.safeTransfer( msg.sender, underlyingAssetContract.balanceOf(address(this)) );
    }

    function unpanic() virtual external onlyOwner {
        require(false == true, "Not yet implemented");
    }

    function retire() virtual external onlyOwner {
        selfdestruct(payable(msg.sender));
    }

    function withdrawAll() virtual external onlyOwner {
        withdrawAllUndeployed();
    }

    function withdraw(uint256 _amount) virtual external onlyOwner {
        withdrawAllUndeployed();
    }

    function deploy() virtual external onlyOwner {
        require(false == true, "Not yet implemented");
    }

    function execute() virtual external {
        require(false == true, "Not yet implemented");
    }

}
