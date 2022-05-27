// SPDX-License-Identifier: AGPL-3.0-or-later
pragma solidity ^0.8.11;

import "../../Libraries/@openzeppelin/v4.4/token/ERC20/IERC20.sol";
import "../../Libraries/@openzeppelin/v4.4/token/ERC20/utils/SafeERC20.sol";

import "../../Libraries/Core/PrivatelyOwnable.sol";
import "../../Interfaces/Core/Compounding/ILockedStratVault.sol";

abstract contract LockedStratVault is ILockedStratVault, PrivatelyOwnable {
    using SafeERC20 for IERC20;

    address internal underlyingAssetAddress;

    constructor(address _underlyingAssetAddress) {
        underlyingAssetAddress = _underlyingAssetAddress;
    }

    function getUndeployedBalance() override public view returns (uint256) {
        return IERC20(underlyingAssetAddress).balanceOf(address(this));
    }

    function depositAll() override external {
        deposit( IERC20(underlyingAssetAddress).balanceOf(msg.sender) );
    }

    function deposit(uint256 _amount) override public {
        IERC20(underlyingAssetAddress).safeTransferFrom( msg.sender, address(this), _amount );
    }

    function withdrawAllUndeployed() override public onlyOwner {
        IERC20 underlyingAssetContract = IERC20(underlyingAssetAddress);
        underlyingAssetContract.safeTransfer( msg.sender, underlyingAssetContract.balanceOf(address(this)) );
    }

    function untuckTokens(address _token) override external onlyOwner {
        IERC20 tokenContract = IERC20(_token);
        tokenContract.safeTransfer( msg.sender, tokenContract.balanceOf(address(this)) );
    }

}
