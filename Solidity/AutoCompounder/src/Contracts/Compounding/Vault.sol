// SPDX-License-Identifier: AGPL-3.0-or-later
pragma solidity ^0.8.11;

import "../../Libraries/@openzeppelin/v4.4/access/Ownable.sol";
import "../../Libraries/@openzeppelin/v4.4/security/Pausable.sol";
import "../../Libraries/@openzeppelin/v4.4/security/ReentrancyGuard.sol";
import "../../Libraries/@openzeppelin/v4.4/token/ERC20/IERC20.sol";
import "../../Libraries/@openzeppelin/v4.4/token/ERC20/ERC20.sol";
import "../../Libraries/@openzeppelin/v4.4/token/ERC20/utils/SafeERC20.sol";
import "../../Libraries/@openzeppelin/v4.4/utils/math/SafeMath.sol";

import "../../Interfaces/Core/Compounding/IVault.sol";
import "../../Interfaces/Core/Compounding/IStrategy.sol";
import "../IndirectTranferablePositionERC20.sol";

contract Vault is IVault, IndirectTranferablePositionERC20, Ownable, Pausable {
    using SafeERC20 for IERC20;
    using SafeMath for uint256;

    address private underlyingAssetAddress;

    IERC20 private underlyingAssetContract;

    address private strategyAddress;

    IStrategy private strategyContract;

    constructor(
        string memory _name,
        string memory _symbol,
        address _underlyingAssetAddress,
        address _strategyAddress
    ) IndirectTranferablePositionERC20(_name, _symbol) {
        _pause();
        _setUnderlyingAsset(_underlyingAssetAddress);
        setStrategyContract(_strategyAddress);
        _unpause();
    }

    // #region EVENTS

    event Deposit(address indexed beneficiary, uint256 _amount);
    event Withdraw(address indexed beneficiary, uint256 _amount);
    event StrategyUpgrade(address indexed newStrategyAddress);

    // #endregion EVENTS

    // #region PRIVATE METHODS

    /// @dev The underlying asset can only be set during instantiation.
    ///      Any strategy update must use the same asset.
    function _setUnderlyingAsset(address _address) private {
        require(underlyingAssetAddress == address(0), "The underlying asset cannot be changed after instantiation.");

        underlyingAssetAddress = _address;
        underlyingAssetContract = IERC20(_address);
    }

    /// @dev Deploys/transfers the available underling asset to the Strategy.
    function _deployAvailableUnderlyingToStrategy() internal whenNotPaused {
        require(strategyAddress != address(0), "Strategy not set, that burns!");

        uint256 vaultBalance = getVaultBalance();

        if (vaultBalance == 0) {
            return;
        }

        underlyingAssetContract.safeTransfer( strategyAddress, vaultBalance );
    }

    function _getUnderlyingBalanceFromShares(uint _shareAmount) public view returns(uint256) {
        return ( getVaultTvl().mul(_shareAmount) ).div(totalSupply());
    }

    // #endregion PRIVATE METHODS

    // #region GOV METHODS

    /// @dev Upgrades to a new underlying Strategy, retiring the previous.
    ///      To use in case of a bug/optimisation on the Strategy.
    function setStrategyContract(address _address) public onlyOwner whenPaused {
        IStrategy newStrategyContract = IStrategy(_address);

        require(_address != address(0), "Strategy not defined!");
        require(_address != strategyAddress, "Cannot upgrade to the same strategy.");
        require(newStrategyContract.getUnderlyingAssetAddress() == underlyingAssetAddress, "The underlying asset must be the same.");

        if (strategyAddress != address(0)) {
            strategyContract.withdrawAllToVault();
            strategyContract.retire();
        }

        strategyAddress = _address;
        strategyContract = IStrategy(_address);
        emit StrategyUpgrade(_address);
    }

    function pause() public onlyOwner {
        _pause();

        if (strategyAddress != address(0)) {
            strategyContract.pause();
        }
    }

    function unpause() public onlyOwner {
        if (strategyAddress != address(0)) {
            strategyContract.unpause();
        }

        _unpause();
    }

    function panic() external onlyOwner {
        pause();
        strategyContract.panic();
    }

    function unpanic() external onlyOwner {
        strategyContract.unpanic();
        unpause();
        _deployAvailableUnderlyingToStrategy();
    }

    function untuckTokens(address _token) external onlyOwner {
        require(_token != address(underlyingAssetAddress), "Invalid token. Cannot 'unstuck' the underlying asset.");

        IERC20 tokenContract = IERC20(_token);
        tokenContract.safeTransfer(msg.sender, tokenContract.balanceOf(address(this)));
    }

    // #endregion GOV METHODS

    // #region PUBLIC METHODS

    function getUnderlyingAssetAddress() external view returns(address) {
        return underlyingAssetAddress;
    }

    function getStrategyAddress() external view returns(address) {
        return strategyAddress;
    }

    /// @dev Gets the available underlying asset balance of this Vault, not deployed to the strategy.
    function getVaultBalance() public view returns (uint256) {
        return underlyingAssetContract.balanceOf( address(this) );
    }

    /// @dev Gets the Total Value Locked, taking into account both the Vault and Strategy balances.
    function getVaultTvl() public view returns (uint256) {
        if (strategyAddress == address(0)) {
            // No strategy is set.
            return getVaultBalance();
        }

        return getVaultBalance().add( strategyContract.getInvestedBalance() );
    }

    function getUnderlyingHolderBalance() public view returns(uint256) {
        // We will use "msg.sender" throughout instead of receiving as argument for user privacy.
        return _getUnderlyingBalanceFromShares( balanceOf(msg.sender) );
    }

    function execute() external whenNotPaused {
        _deployAvailableUnderlyingToStrategy();
        strategyContract.execute();
    }

    /// @dev The holder must first call ".approve()" for the underlying ERC20.
    function depositAll() external {
        deposit( underlyingAssetContract.balanceOf(msg.sender) );
    }

    /// @dev The holder must first call ".approve()" for the underlying ERC20.
    function deposit(uint256 _amount) public nonReentrant {
        _deposit( _amount );
    }

    function _deposit(uint256 _amount) internal whenNotPaused {
        require(_amount > 0, "Amount must be more than 0.");

        strategyContract.beforeDeposit();

        uint256 initialTvl = getVaultTvl();

        underlyingAssetContract.safeTransferFrom(msg.sender, address(this), _amount);
        _deployAvailableUnderlyingToStrategy();

        strategyContract.afterDeposit();

        uint256 newTvl = getVaultTvl();
        _amount = newTvl.sub(initialTvl); // Additional check for deflationary tokens.

        if (totalSupply() > 0) {
            // From here, "amount" refers to the Vault shares to mint, instead of the underlying deposit amount.
            _amount = ( _amount.mul(totalSupply()) ).div(initialTvl);
        }

        _mint(msg.sender, _amount);
        emit Deposit(msg.sender, _amount);
    }

    function withdrawAll() external {
        withdraw( balanceOf(msg.sender) );
    }

    function withdraw(uint256 _amount) public nonReentrant {
        _withdraw(_amount);
    }

    function _withdraw(uint256 _amount) internal {
        require(totalSupply() > 0, "The vault has no shares.");
        require(_amount > 0, "The withdrawal amount must be higher than 0.");

        uint256 underlyingWithdrawAmount = _getUnderlyingBalanceFromShares(_amount);
        _burn(msg.sender, _amount);

        uint underlyingVaultBalance = getVaultBalance();

        if (underlyingVaultBalance < underlyingWithdrawAmount) {
            uint amountFromStrategy = underlyingWithdrawAmount.sub(underlyingVaultBalance);
            strategyContract.withdrawToVault(amountFromStrategy);

            // Deflationary tokens.
            uint newUnderlyingVaultBalance = getVaultTvl();
            uint256 balanceDifference = newUnderlyingVaultBalance.sub(underlyingVaultBalance);
            if (balanceDifference < amountFromStrategy) {
                underlyingWithdrawAmount = underlyingVaultBalance.add(balanceDifference);
            }
        }

        // ".safeTransfer()" reverts everything in case of error.
        underlyingAssetContract.safeTransfer(msg.sender, underlyingWithdrawAmount);
        emit Withdraw(msg.sender, underlyingWithdrawAmount);
    }

    // #endregion PUBLIC METHODS

}
