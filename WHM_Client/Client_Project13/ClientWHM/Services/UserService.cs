using ClientWHM.Models;
using ClientWHM.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

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
                    // Xử lý các trường hợp khác (nếu cần)
                    // ...
                    throw new Exception($"Login failed with status code: {statusCode}");
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ (nếu cần)
                // ...
                throw;
            }
        }

        public async Task<List<Nhanvien>?> GetNhanViens()
        {
            List<Nhanvien>? nhanviens = await GetData<List<Nhanvien>>("user/list");
            return nhanviens;
        }    
    }
}
