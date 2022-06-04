// SPDX-License-Identifier: AGPL-3.0-or-later
pragma solidity ^0.8.11;

interface IMasterChef {

    function userInfo(uint256 _pid, address _user) external view returns (uint256, uint256);

    function deposit(uint256 _pid, uint256 _amount) external;

    function withdraw(uint256 _pid, uint256 _amount) external;

    function emergencyWithdraw(uint256 _pid) external;

}
