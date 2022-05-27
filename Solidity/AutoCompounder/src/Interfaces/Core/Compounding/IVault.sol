// SPDX-License-Identifier: AGPL-3.0-or-later
pragma solidity ^0.8.11;

interface IVault {

    function getUnderlyingAssetAddress() external view returns(address);

    function getStrategyAddress() external view returns(address);

    function getVaultBalance() external view returns (uint256);

    function getVaultTvl() external view returns (uint256);

    function getUnderlyingHolderBalance() external view returns(uint256);

    function execute() external;

    function depositAll() external;

    function deposit(uint256 _amount) external;

    function withdrawAll() external;

    function withdraw(uint256 _amount) external;

}
