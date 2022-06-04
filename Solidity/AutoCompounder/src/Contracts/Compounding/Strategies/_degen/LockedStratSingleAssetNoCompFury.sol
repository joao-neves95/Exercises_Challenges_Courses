// SPDX-License-Identifier: AGPL-3.0-or-later
pragma solidity ^0.8.11;

import "../../../../Interfaces/External/_degen/IFuryMasterChef.sol";

import "../../LockedStratSingleAssetNoCompBase.sol";

contract LockedStratSingleAssetNoCompFury is LockedStratSingleAssetNoCompBase {

    constructor(
        address _underlyingAssetAddress,
        address _rewardAssetAddress,
        address _unirouterAddress,
        address _chefAddress,
        uint256 _poolId
    )
    LockedStratSingleAssetNoCompBase(
        _underlyingAssetAddress,
        _rewardAssetAddress,
        _unirouterAddress,
        _chefAddress,
        _poolId
    )
    {
    }

    function getPendingRewardAmount() override external view returns (uint256) {
        return IFuryMasterChef(chefAddress).pendingFury( poolId, address(this) );
    }
}
