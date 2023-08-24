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
    internal class XuatKhoService : BaseService
    {
        private readonly HttpClient httpClient;

        public XuatKhoService()
        {
            httpClient = new HttpClient();
        }

        public async Task<List<Xuatkho>?> GetXuatKhos()
        {
            List<Xuatkho>? xuatkhos = await GetData<List<Xuatkho>>("xuatkho/list");
            return xuatkhos;
        }

        public async Task<Xuatkho> GetXuatKho(int id)
        {
            Xuatkho xuatkho = await GetData<Xuatkho>($"xuatkho/get/{id}");
            return xuatkho;
        }

        public async Task<bool> AddXuatKho(Xuatkho xuatkho)
        {
            string url = "xuatkho/add";
            try
            {
                var statusCode = await PushData(url, xuatkho);
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

        public async Task<bool> EditXuatKho(Xuatkho xuatkho)
        {
            string url = "xuatkho/edit";
            try
            {
                var statusCode = await PutData(url, xuatkho);
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

        public async Task<bool> DeleteXuatKho(int id)
        {
            string url = $"xuatkho/delete/{id}";
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

        /////////////////////////////////////////////

        public async Task<List<Chitietxuatkho>?> GetCTXuatKhos()
        {
            List<Chitietxuatkho>? chitietxuatkhos = await GetData<List<Chitietxuatkho>>("chitietxuatkho/list");
            return chitietxuatkhos;
        }

        public async Task<Chitietxuatkho> GetCTXuatKho(int id)
        {
            Chitietxuatkho chitietxuatkho = await GetData<Chitietxuatkho>($"chitietxuatkho/get/{id}");
            return chitietxuatkho;
        }

        public async Task<bool> AddCTXuatKho(List<Chitietxuatkho> chitietxuatkhos, int id)
        {
            string url = "xuatkho/add";
            try
            {
                var statusCode = await PushData(url, chitietxuatkhos);
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

        public async Task<bool> EditCTXuatKho(Chitietxuatkho xuatkho)
        {
            string url = "xuatkho/edit";
            try
            {
                var statusCode = await PutData(url, xuatkho);
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

        public async Task<bool> DeleteCTXuatKho(int id)
        {
            string url = $"xuatkho/delete/{id}";
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
    }
}
