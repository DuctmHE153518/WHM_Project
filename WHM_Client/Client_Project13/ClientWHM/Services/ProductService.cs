using ClientWHM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClientWHM.Services
{
    internal class ProductService : BaseService
    {
        private readonly HttpClient httpClient;

        public ProductService()
        {
            httpClient = new HttpClient();
        }

        public async Task<List<Sanpham>?> GetSanPhams()
        {
            List<Sanpham>? sanphams = await GetData<List<Sanpham>>("sanpham/list");
            return sanphams;
        }

        public async Task<Sanpham> GetSanPham(int id)
        {
            Sanpham sanpham = await GetData<Sanpham>($"sanpham/detail/{id}");
            return sanpham;
        }

        public async Task<bool> AddSanPham(Sanpham sanpham)
        {
            string url = "sanpham/add";
            try
            {
                var statusCode = await PushData(url, sanpham);
                if (statusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else if (statusCode == HttpStatusCode.Conflict)
                {
                    return false;
                }
                else
                {
                    throw new Exception($"Add failed with status code: {statusCode}");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> EditSanPham(Sanpham sanpham)
        {
            string url = "sanpham/update";
            try
            {
                var statusCode = await PutData(url, sanpham);
                if (statusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else if (statusCode == HttpStatusCode.Conflict)
                {
                    return false;
                }
                else
                {
                    throw new Exception($"Edit failed with status code: {statusCode}");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteSanPham(int id)
        {
            string url = $"sanpham/remove/{id}";
            try
            {
                var statusCode = await DeleteData(url);
                if (statusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else if (statusCode == HttpStatusCode.Conflict)
                {
                    return false;
                }
                else
                {
                    throw new Exception($"Delete failed with status code: {statusCode}");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Sanpham>> SearchSanPham(string text)
        {
            List<Sanpham> sanphams = await GetData<List<Sanpham>>($"sanpham/search/{text}");
            return sanphams;
        }
    }
}
