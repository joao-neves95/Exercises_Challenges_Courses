// SPDX-License-Identifier: AGPL-3.0-or-later
pragma solidity ^0.8.11;

import "../../Libraries/@openzeppelin/v4.4/token/ERC20/IERC20.sol";
import "../../Libraries/@openzeppelin/v4.4/utils/math/SafeMath.sol";

import "../../Interfaces/External/IUniswapV2Pair.sol";
import "../../Interfaces/External/UniswapV2RouterEth.sol";

import "./LockedStratBase.sol";

abstract contract LockedStratLpBase is LockedStratBase {
    using SafeMath for uint256;

    address internal lpToken0;
    address internal lpToken1;
    address[] internal rewardToLp0Route;
    address[] internal rewardToLp1Route;

    IUniswapV2Pair internal uniswapV2Pair;
    IUniswapV2RouterEth internal uniswapV2RouterEth;

    constructor(
        address _underlyingAssetAddress,
        address _rewardAssetAddress,
        address _unirouterAddress
    ) LockedStratBase(
        _underlyingAssetAddress,
        _rewardAssetAddress
    )
    {
        uniswapV2RouterEth = IUniswapV2RouterEth(_unirouterAddress);
        uniswapV2Pair = IUniswapV2Pair(underlyingAssetAddress);

        lpToken0 = uniswapV2Pair.token0();
        lpToken1 = uniswapV2Pair.token1();
        rewardToLp0Route = [rewardAssetAddress, lpToken0];
        rewardToLp1Route = [rewardAssetAddress, lpToken1];
    }

    function addLiquidity() virtual internal {
        uint256 rewardBalance = IERC20(rewardAssetAddress).balanceOf(address(this));

        if (rewardBalance == 0) {
            return;
        }

        uint256 halfReward = rewardBalance.div(2);

        if (lpToken0 != underlyingAssetAddress) {
            uniswapV2RouterEth.swapExactTokensForTokensSupportingFeeOnTransferTokens(
                halfReward, 0, rewardToLp0Route, address(this), block.timestamp
            );
        }

        if (lpToken1 != underlyingAssetAddress) {
            uniswapV2RouterEth.swapExactTokensForTokensSupportingFeeOnTransferTokens(
                halfReward, 0, rewardToLp1Route, address(this), block.timestamp
            );
        }

        // Mint the underlying asset (the LP).
        uniswapV2RouterEth.addLiquidity(
            lpToken0, lpToken1,
            IERC20(lpToken0).balanceOf(address(this)), IERC20(lpToken1).balanceOf(address(this)),
            1, 1,
            address(this),
            block.timestamp
        );
    }
}
