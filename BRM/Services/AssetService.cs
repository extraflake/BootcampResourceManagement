using BRM.Services.Interfaces;
using Data.Models;
using Data.Repositories.Interfaces;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services
{
    public class AssetService : IAssetService
    {
        bool status = false;

        private IAssetRepository _assetRepository;

        public AssetService(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        public bool Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return status;
            }
            else
            {
                return _assetRepository.Delete(id);
            }
        }

        public IEnumerable<Asset> Get()
        {
            return _assetRepository.Get();
        }

        public Asset Get(string id)
        {
            return _assetRepository.Get(id);
        }

        public AssetCountVM GetCount()
        {
            return _assetRepository.GetCount();
        }

        public IEnumerable<AssetDisplayVM> GetDisplay()
        {
            return _assetRepository.GetDisplay();
        }

        public bool Insert(Asset asset)
        {
            if (string.IsNullOrWhiteSpace(asset.number.ToString()) ||
                string.IsNullOrWhiteSpace(asset.type.ToString()))
            {
                return status;
            }
            else
            {
                return _assetRepository.Insert(asset);
            }
        }

        public bool Update(string id, Asset asset)
        {
            if (string.IsNullOrWhiteSpace(id) ||
                string.IsNullOrWhiteSpace(asset.number.ToString()) ||
                string.IsNullOrWhiteSpace(asset.type.ToString()) ||
                string.IsNullOrWhiteSpace(asset.employee))
            {
                return status;
            }
            else
            {
                return _assetRepository.Update(id, asset);
            }
        }
    }
}
