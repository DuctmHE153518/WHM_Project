﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace ClientWHM.Services
{
    internal class BaseService
    {
        private string? _rootUrl;
        public BaseService() { 
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _rootUrl = config.GetSection("ApiUrls")["MyApi"];
        }
        public async Task<T?> GetData<T>(string url, string? accepttype = null)
        {
            T? result = default(T);
            HttpClient client = new HttpClient();
            url = _rootUrl + url;
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (responseMessage.Content is not null)
                    result = responseMessage.Content.ReadFromJsonAsync<T>().Result;
                return result;
            }
            else throw new Exception(responseMessage.StatusCode.ToString());
        }

        public async Task<HttpStatusCode> PushData<T>(string url, T value, string? accepttype = null)
        {
            url = _rootUrl + url;
            HttpClient client = new HttpClient();
            var jsonStr = JsonSerializer.Serialize(value);
            HttpContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync(url, content);
            return responseMessage.StatusCode;
        }

        public async Task<HttpStatusCode> PutData<T>(string url, T value, string? accepttype = null)
        {
            url = _rootUrl + url;
            HttpClient client = new HttpClient();
            var jsonStr = JsonSerializer.Serialize(value);
            HttpContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PutAsync(url, content);
            return responseMessage.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteData(string url, string? accepttype = null)
        {
            url = _rootUrl + url;
            HttpClient client = new HttpClient();
            HttpResponseMessage responseMessage = await client.DeleteAsync(url);
            return responseMessage.StatusCode;
        }

        public async Task<HttpStatusCode> PushListData<T>(string url, T value, string? accepttype = null)
        {
            url = _rootUrl + url;
            HttpClient client = new HttpClient();
            var jsonStr = JsonSerializer.Serialize(value);
            HttpContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync(url, content);
            return responseMessage.StatusCode;
        }

        public async Task<T> Filter<T>(DateTime dpTuNgay, DateTime dpDenNgay)
        {
            T? result = default(T);
            HttpClient client = new HttpClient();
            string url = $"bill/filter/{dpTuNgay.ToString("yyyy-MM-dd")}/{dpDenNgay.ToString("yyyy-MM-dd")}";
            HttpResponseMessage responseMessage = await client.GetAsync(_rootUrl + url);
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (responseMessage.Content is not null)
                    result = responseMessage.Content.ReadFromJsonAsync<T>().Result;
                return result;
            }
            else throw new Exception(responseMessage.StatusCode.ToString());

            /*List<T> result = new List<T>();
            string url = $"bill/filter/{dpTuNgay.ToString("yyyy-MM-dd")}/{dpDenNgay.ToString("yyyy-MM-dd")}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(_rootUrl + url);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    result = JsonSerializer.Deserialize<List<T>>(json);
                }
                else
                {
                    throw new Exception(response.StatusCode.ToString());
                }
            }
            return result;*/
        }
    }
}
