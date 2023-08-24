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
    internal class BillService : BaseService
    {
        private readonly HttpClient httpClient;

        public BillService()
        {
            httpClient = new HttpClient();
        }

        public async Task<List<Hoadon>?> GetHoaDons()
        {
            List<Hoadon>? hoadons = await GetData<List<Hoadon>>("bill/list");
            return hoadons;
        }

        public async Task<Nhanvien> GetHoaDon(int id)
        {
            Nhanvien nhanvien = await GetData<Nhanvien>($"bill/get/{id}");
            return nhanvien;
        }

        public async Task<List<Hoadon>> SearchHoaDon(string text)
        {
            List<Hoadon> hoadons = await GetData<List<Hoadon>>($"bill/search/{text}");
            return hoadons;
        }

        public async Task<List<Chitiethoadon>?> GetChiTietHoaDon(int id)
        {
            List<Chitiethoadon>? chitiethoadon = await GetData<List<Chitiethoadon>>($"bill/listbilldetail/{id}");
            return chitiethoadon;
        }

        public async Task<bool> AddHoaDon(Hoadon hoadon)
        {
            string url = "bill/add";
            try
            {
                var statusCode = await PushData(url, hoadon);
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

        public async Task<bool> DeleteHoaDon(int id)
        {
            string url = $"bill/delete/{id}";
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

        //////////////////////////////////////////////////////////////////////////

        public async Task<bool> AddCTHoaDon(Chitiethoadon chitiethoadon)
        {
            string url = "bill/addbd";
            try
            {
                var statusCode = await PushData(url, chitiethoadon);
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
    }
}
