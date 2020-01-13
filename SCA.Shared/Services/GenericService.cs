﻿using Newtonsoft.Json;
using SCA.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Shared.Services
{
    public class GenericService<T> : IGenericService<T>
    {
        protected readonly HttpClient _clientHttp;
        public string _url { get; set; }
        public string _token { get; set; }

        public GenericService()
        {
            _clientHttp = new HttpClient();
        }

        public void SetUrl(string url)
        {
            this._url = url;
        }

        public void SetToken(string token)
        {   
            if (this._token != token)
            {
                this._token = token;
                this._clientHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this._token);
            }
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            
            var response = await _clientHttp.GetAsync(this._url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            IEnumerable<T> lista = JsonConvert.DeserializeObject<IEnumerable<T>>(responseBody);

            return lista;
        }

        public async Task<T> FindByIdAsync(int? id)
        {
            if (id == null)
            {
                return default(T);
            }

            var response = await _clientHttp.GetAsync(string.Concat(this._url, $"/{id}"));
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            T obj = JsonConvert.DeserializeObject<T>(responseBody);

            return obj;
        }

        public async Task<bool> InsertAsync(T obj)
        {
            string url = String.Concat(this._url);
            var jsonContent = JsonConvert.SerializeObject(obj);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _clientHttp.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                if (response.Content != null)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ResultApi resultApi = JsonConvert.DeserializeObject<ResultApi>(responseContent);
                    return resultApi.status;
                }
            }

            return false;
        }

        public async Task<bool> UpdateAsync(int? id, T obj)
        {
            string url = String.Concat(this._url);
            var jsonContent = JsonConvert.SerializeObject(obj);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _clientHttp.PutAsync(string.Concat(this._url, $"/{id}"), content);

            if (response.IsSuccessStatusCode)
            {
                if (response.Content != null)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ResultApi resultApi = JsonConvert.DeserializeObject<ResultApi>(responseContent);
                    return resultApi.status;
                }
            }

            return false;
        }

        public async Task<bool> DeleteAsync(int? id)
        {
            if (id == null)
            {
                return false;
            }

            HttpResponseMessage response =  await _clientHttp.DeleteAsync(string.Concat(this._url, $"/{id}"));
            
            if (response.IsSuccessStatusCode)
            {
                if (response.Content != null)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ResultApi resultApi = JsonConvert.DeserializeObject<ResultApi>(responseContent);
                    return resultApi.status;
                }
            }

            return false;
        }
    }
}