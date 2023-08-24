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
    internal class BatchService : BaseService
    {
        private readonly HttpClient httpClient;

        public BatchService()
        {
            httpClient = new HttpClient();
        }

        public async Task<List<Loaisanpham>?> GetLoaiSanPhams()
        {
            List<Loaisanpham>? loaisanphams = await GetData<List<Loaisanpham>>("loaisp/list");
            return loaisanphams;
        }

        public async Task<Loaisanpham> GetLoaiSanPham(int id)
        {
            Loaisanpham loaisanpham = await GetData<Loaisanpham>($"loaisp/detail/{id}");
            return loaisanpham;
        }

        public async Task<bool> AddLoaiSanPham(Loaisanpham loaisanpham)
        {
            string url = "loaisp/add";
            try
            {
                var statusCode = await PushData(url, loaisanpham);
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

        public async Task<bool> EditLoaiSanPham(Loaisanpham loaisanpham)
        {
            string url = "loaisp/update";
            try
            {
                var statusCode = await PutData(url, loaisanpham);
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

        public async Task<bool> DeleteLoaiSanPham(int id)
        {
            string url = $"loaisp/remove/{id}";
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

        public async Task<List<Loaisanpham>> SearchLoaiSanPham(string text)
        {
            List<Loaisanpham> loaisanphams = await GetData<List<Loaisanpham>>($"loaisp/search/{text}");
            return loaisanphams;
        }
    }
}
