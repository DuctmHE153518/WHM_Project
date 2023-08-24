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
    internal class NhapKhoService : BaseService
    {
        private readonly HttpClient httpClient;

        public NhapKhoService()
        {
            httpClient = new HttpClient();
        }

        public async Task<List<Nhapkho>?> GetNhapKhos()
        {
            List<Nhapkho>? nhapkhos = await GetData<List<Nhapkho>>("nhapkho/list");
            return nhapkhos;
        }

        public async Task<Nhapkho> GetNhapKho(int id)
        {
            Nhapkho nhapkho = await GetData<Nhapkho>($"nhapkho/get/{id}");
            return nhapkho;
        }

        public async Task<bool> AddNhapKho(Nhapkho nhapkho)
        {
            string url = "nhapkho/add";
            try
            {
                var statusCode = await PushData(url, nhapkho);
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

        public async Task<bool> EditNhapKho(Nhapkho nhapkho)
        {
            string url = "nhapkho/edit";
            try
            {
                var statusCode = await PutData(url, nhapkho);
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

        public async Task<bool> DeleteNhapKho(int id)
        {
            string url = $"nhapkho/delete/{id}";
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

        //////////////////////////////////////////////////////

        public async Task<List<Nhapkho>?> GetCTNhapKhos()
        {
            List<Nhapkho>? nhapkhos = await GetData<List<Nhapkho>>("nhapkho/list");
            return nhapkhos;
        }

        public async Task<Nhapkho> GetCTNhapKho(int id)
        {
            Nhapkho nhapkho = await GetData<Nhapkho>($"nhapkho/get/{id}");
            return nhapkho;
        }

        public async Task<bool> AddCTNhapKho(Nhapkho nhapkho)
        {
            string url = "nhapkho/add";
            try
            {
                var statusCode = await PushData(url, nhapkho);
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

        public async Task<bool> EditCTNhapKho(Nhapkho nhapkho)
        {
            string url = "nhapkho/edit";
            try
            {
                var statusCode = await PutData(url, nhapkho);
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

        public async Task<bool> DeleteCTNhapKho(int id)
        {
            string url = $"nhapkho/delete/{id}";
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
