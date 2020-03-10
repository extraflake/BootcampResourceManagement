using Data.Context;
using Data.Models;
using Data.Repositories.Interfaces;
using Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly MyContext _myContext;
        IEnumerable<Asset> resultList = null;
        IEnumerable<AssetDisplayVM> resultListVM = null;
        Asset result = null;
        AssetCountVM resultAssetVM = null;
        bool status = false;

        public AssetRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public IEnumerable<Asset> Get()
        {
            resultList = _myContext.Assets.FromSql($"call sp_retrieve_assets").ToList().OrderByDescending(x => x.create_date).OrderBy(x => x.type);
            return resultList;
        }

        public IEnumerable<AssetDisplayVM> GetDisplay()
        {
            resultListVM = _myContext.AssetDisplayVMs.FromSql($"call sp_retrieve_assets").ToList().OrderByDescending(x => x.create_date).OrderBy(x => x.type);
            return resultListVM;
        }

        public Asset Get(string id)
        {
            result = _myContext.Assets.FirstOrDefault(x => x.id == id);
            return result;
        }

        public bool Insert(Asset asset)
        {
            var get = _myContext.Assets.Where(x => x.id == asset.id || x.number == asset.number).FirstOrDefault();
            if (get != null)
            {
                return false;
            }
            else
            {
                //var push = new Asset(asset.id, asset.number, asset.type, asset.employee);
                //_myContext.Assets.Add(push);
                //var result = _myContext.SaveChanges();
                //return result > 0;
                string connStr = "server=mejakerja.mysql.database.azure.com;user id=mejaadmin@mejakerja;password=M3tr0dat@P@s5;port=3306;persistsecurityinfo=True;database=db_brm;allowuservariables=True;Convert Zero Datetime='True'";
                MySqlConnection conn = new MySqlConnection(connStr);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_insert_assets", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@number", asset.number);
                cmd.Parameters.AddWithValue("@type", asset.type);
                try
                {
                    var result = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (result > 0)
                    {
                        status = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    throw;
                }
            }
            return status;

        }

        public bool Update(string id, Asset asset)
        {
            //var get = _myContext.Assets.Where(x => x.id == asset.id).FirstOrDefault();
            //var getsecondlayer = _myContext.Assets.Where(x => x.employee == asset.employee && x.type == asset.type).FirstOrDefault();
            //if (get != null)
            //{
            //    if (getsecondlayer != null)
            //    {
            //        return false;
            //    }
            //    else
            //    {
            //        string connStr = "server=mejakerja.mysql.database.azure.com;user id=mejaadmin@mejakerja;password=M3tr0dat@P@s5;port=3306;persistsecurityinfo=True;database=db_brm;allowuservariables=True;Convert Zero Datetime='True'";
            //        MySqlConnection conn = new MySqlConnection(connStr);
            //        conn.Open();
            //        MySqlCommand cmd = new MySqlCommand("tg_sp_update_asset", conn)
            //        {
            //            CommandType = CommandType.StoredProcedure
            //        };
            //        cmd.Parameters.AddWithValue("@asset", get.id);
            //        cmd.Parameters.AddWithValue("@employee_id", asset.employee);
            //        try
            //        {
            //            var result = cmd.ExecuteNonQuery();
            //            conn.Close();
            //            if (result > 0)
            //            {
            //                status = true;
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.Write(ex.Message);
            //        }
            //    }
            //}
            //return status;
            var get = _myContext.Assets.Where(x => x.id == asset.id || x.number == asset.number).FirstOrDefault();
            if (get == null)
            {
                return false;
            }
            else
            {
                //var push = new Asset(asset.id, asset.number, asset.type, asset.employee);
                //_myContext.Assets.Add(push);
                string connStr = "server=mejakerja.mysql.database.azure.com;user id=mejaadmin@mejakerja;password=M3tr0dat@P@s5;port=3306;persistsecurityinfo=True;database=db_brm;allowuservariables=True;Convert Zero Datetime='True'";
                MySqlConnection conn = new MySqlConnection(connStr);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("tg_sp_update_asset", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@asset", asset.id);
                cmd.Parameters.AddWithValue("@employee_id", asset.employee);
                try
                {
                    var result = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (result > 0)
                    {
                        status = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    throw;
                }
            }
            return status;
        }

        public bool Delete(string id)
        {
            var get = Get(id);
            get.employee = null;
            try
            {
                string connStr = "server=mejakerja.mysql.database.azure.com;user id=mejaadmin@mejakerja;password=M3tr0dat@P@s5;port=3306;persistsecurityinfo=True;database=db_brm;allowuservariables=True;Convert Zero Datetime='True'";
                MySqlConnection conn = new MySqlConnection(connStr);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("tg_sp_update_asset", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@asset", id);
                cmd.Parameters.AddWithValue("@employee_id", "");
                var result = cmd.ExecuteNonQuery();
                conn.Close();
                if (result > 0)
                {
                    status = true;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return status;

        }

        public AssetCountVM GetCount()
        {
            resultAssetVM = _myContext.assetCountVMs.FromSql($"call sp_retrieve_asset_count").FirstOrDefault();
            return resultAssetVM;
        }
    }
}
