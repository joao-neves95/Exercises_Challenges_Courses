﻿
namespace eShop.Catalog.Api.Models.Config
{
    public class MongoDbConfig
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string CollectionName { get; set; }
    }
}