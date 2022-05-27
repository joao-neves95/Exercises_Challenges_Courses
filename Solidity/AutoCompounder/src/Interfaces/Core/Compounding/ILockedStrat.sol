// SPDX-License-Identifier: AGPL-3.0-or-later
pragma solidity ^0.8.11;

interface ILockedStrat {

    function getTvl() external view returns (uint256);

    function getDeployedBalance() external view returns (uint256);

    function getPendingRewardAmount() external view returns (uint256);

    function panic() external;

    function unpanic() external;

    function retire() external;

    function withdrawAll() external;

    function withdraw(uint256 _amount) external;

    function deploy() external;

    function execute() external;

}
