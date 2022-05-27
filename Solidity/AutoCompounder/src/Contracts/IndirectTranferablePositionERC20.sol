// SPDX-License-Identifier: AGPL-3.0-or-later
pragma solidity ^0.8.11;

import "../Libraries/@openzeppelin/v4.4/security/ReentrancyGuard.sol";
import "../Libraries/@openzeppelin/v4.4/token/ERC20/IERC20.sol";
import "../Libraries/@openzeppelin/v4.4/token/ERC20/ERC20.sol";
import "../Libraries/@openzeppelin/v4.4/token/ERC20/utils/SafeERC20.sol";
import "../Libraries/@openzeppelin/v4.4/utils/math/SafeMath.sol";

import "../Interfaces/Core/IIndirectTranferablePositionERC20.sol";

abstract contract IndirectTranferablePositionERC20 is IIndirectTranferablePositionERC20, ERC20, ReentrancyGuard {
    using SafeERC20 for IERC20;
    using SafeMath for uint256;

    constructor(
        string memory _name,
        string memory _symbol
    ) ERC20(_name, _symbol) {
    }

    mapping(address => uint256) private unredeemedShares;

    function indirectPositionTransferAll(address _recipient) external {
        indirectPositionTransfer( _recipient, this.balanceOf(msg.sender) );
    }

    function indirectPositionTransfer(address _recipient, uint256 _amount) public nonReentrant {
        require(_amount > 0 && _amount <= this.balanceOf(msg.sender), "Invalid amount.");

        unredeemedShares[_recipient].add(_amount);
        this.transferFrom( msg.sender, address(this), _amount );
    }

    function getUnredeemedPositionTranferAmount() public view returns (uint256) {
        return unredeemedShares[msg.sender];
    }

    function redeemPositionTransfer() external nonReentrant {
        uint256 unredeemedAmount = unredeemedShares[msg.sender];

        require(unredeemedAmount > 0, "No tokens to redeem.");
        require(unredeemedAmount <= balanceOf(address(this)), "Not enough shares.");

        unredeemedShares[msg.sender] = 0;
        this.transfer( msg.sender, unredeemedAmount );
    }

}
