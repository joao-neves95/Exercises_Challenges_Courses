// SPDX-License-Identifier: AGPL-3.0-or-later
pragma solidity ^0.8.11;

interface IStrategy {

    function getUnderlyingAssetAddress() external view returns(address);

    function getTvl() external view returns (uint256);

    function getNotInvestedBalance() external view returns (uint256);

    function getInvestedBalance() external view returns (uint256);

    function getUnclaimedRewardBalance() external view returns (uint256);

    function withdrawAllToVault() external;

    function withdrawToVault(uint256 _amount) external;

    function pause() external;

    function unpause() external;

    /// @dev Must panic. Liquidate all and withraw everything to the vault.
    function panic() external;

    function unpanic() external;

    function retire() external;

    function beforeDeposit() external;

    function afterDeposit() external;

    function execute() external;

}
