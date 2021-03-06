﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TrueAutoShops.Models;

namespace TrueAutoShops.Services
{
    public interface IShopDataService
    {
        Task<List<ShopInfo>> GetListofShopsByCityName(string cityName);
        Task<List<ShopInfo>> GetShopsByCityId(CancellationToken cancellationToken, int zipcityid = 0);

        Task<List<ShopsSearch>> GetShopsByCityName(CancellationToken cancellationToken, string cityName);
    }
}