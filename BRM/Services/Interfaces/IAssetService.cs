using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services.Interfaces
{
    public interface IAssetService
    {
        IEnumerable<Asset> Get();
        IEnumerable<AssetDisplayVM> GetDisplay();
        Asset Get(string id);
        AssetCountVM GetCount();
        bool Insert(Asset asset);
        bool Update(string id, Asset asset);
        bool Delete(string id);
    }
}
