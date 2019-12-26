using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Interfaces
{
    public interface IAssetRepository
    {
        IEnumerable<Asset> Get();
        IEnumerable<AssetDisplayVM> GetDisplay();
        Asset Get(string id);
        AssetCountVM GetCount();
        bool Update(string id, Asset asset);
        bool Insert(Asset asset);
        bool Delete(string id);
    }
}
