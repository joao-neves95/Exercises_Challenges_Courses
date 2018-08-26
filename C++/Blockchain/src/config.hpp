// --------------------------------------------------------------------------
//
// Copyright (c) 2018 shivayl (João Neves - https://github.com/joao-neves95)
//
// Licensed under the MIT License (https://opensource.org/licenses/MIT).
//
// The license of this source code can be found in the MIT-LICENSE file 
// located in the root of this project.
//
// --------------------------------------------------------------------------

#pragma once
#include <string>
#include "enums\environmentType.hpp"

static const EnvironmentType ENVIRONMENT = kDevelopment;

static const std::string DATABASE_BLOCKS = "data/blocks";
static const std::string kLastBlockKey = "latest_block";

static const unsigned short SERVER_PORT = 3080;
