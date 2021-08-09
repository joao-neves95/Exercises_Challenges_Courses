// SPDX-License-Identifier: AGPL-3.0-or-later
pragma solidity ^0.4.25;

contract SimpleSmartContract {

    string name;

    constructor() public {
        name = "SHIVAYL";
    }

    function getName() public view returns (string) {
        return name;
    }

    function setName(string _name) public {
        name = _name;
    }

}
