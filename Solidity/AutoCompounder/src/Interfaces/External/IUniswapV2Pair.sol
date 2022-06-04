// SPDX-License-Identifier: MIT
pragma solidity ^0.8.11;

// Souce: https://github.com/Uniswap/v2-core/blob/master/contracts/interfaces/IUniswapV2Pair.sol

interface IUniswapV2Pair {

    function token0() external view returns (address);
    function token1() external view returns (address);

    function price0CumulativeLast() external view returns (uint);
    function price1CumulativeLast() external view returns (uint);
    function kLast() external view returns (uint);

}
