using ClientWHM.Models;
using ClientWHM.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientWHM.Services
{
    internal class UserService : BaseService
    {
        private readonly HttpClient httpClient;

        public UserService()
        {
            httpClient = new HttpClient();
        }

        public async Task<bool> Login(LoginRequest loginRequest)
        {
            string url = "user/login";
            try
            {
                var statusCode = await PushData(url, loginRequest);
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
                    throw new Exception($"Login failed with status code: {statusCode}");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Register(RegisterRequest registerRequest)
        {
            string url = "user/register";
            try
            {
                var statusCode = await PushData(url, registerRequest);
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
                    throw new Exception($"Register failed with status code: {statusCode}");
                }
            }
            catch (Exception ex)
            {
                throw ;
            }
        }

        public async Task<List<Nhanvien>?> GetNhanViens()
        {
            List<Nhanvien>? nhanviens = await GetData<List<Nhanvien>>("user/list");
            return nhanviens;
        }

        public async Task<Nhanvien> GetNhanvien(int id)
        {
            Nhanvien nhanvien = await GetData<Nhanvien>($"user/get/{id}");
            return nhanvien;
        }

        public async Task<bool> AddNhanVien(Nhanvien nhanvien)
        {
            string url = "user/add";
            try
            {
                var statusCode = await PushData(url, nhanvien);
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

        public async Task<bool> EditNhanVien(Nhanvien nhanvien)
        {
            string url = "user/edit";
            try
            {
                var statusCode = await PutData(url, nhanvien);
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

        public async Task<bool> DeleteNhanvien(int id)
        {
            string url = $"user/delete/{id}";
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

        public async Task<List<Nhanvien>> SearchNhanvien(string text)
        {
            List<Nhanvien> nhanviens = await GetData<List<Nhanvien>>($"user/search/{text}");
            return nhanviens;
        }

    }
}
