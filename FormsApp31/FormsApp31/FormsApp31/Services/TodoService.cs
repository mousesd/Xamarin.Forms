using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using FormsApp31.Miscellaneous;
using FormsApp31.Models;

namespace FormsApp31.Services
{
    public class TodoService : ITodoService
    {
        #region == Fields & Constructors ==
        private readonly HttpClient client;

        public TodoService()
        {
            //# 기본인증 사용시 아래 주석 제거
            //string authData = $"{Constants.UserName}:{Constants.Password}";
            //string parameter = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            client = new HttpClient { MaxResponseContentBufferSize = 256000 };
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", parameter);
        }
        #endregion

        #region == Implement members of the ITodoService interface ==
        public async Task<List<TodoItem>> RefreshTodoListAsync()
        {
            List<TodoItem> todoItems = null;
            var requestUri = new Uri(string.Format(Constants.Url, string.Empty));

            try
            {
                using (var response = await client.GetAsync(requestUri))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        todoItems = JsonConvert.DeserializeObject<List<TodoItem>>(content);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"StackTrace: {ex.StackTrace}");
            }
            return todoItems;
        }

        public async Task SaveTodoItemAsync(TodoItem item, bool isNewItem)
        {
            string jsonString = JsonConvert.SerializeObject(item);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var requestUri = new Uri(string.Format(Constants.Url, string.Empty));

            try
            {
                HttpResponseMessage response;
                if (isNewItem)
                    response = await client.PostAsync(requestUri, content);
                else
                    response = await client.PutAsync(requestUri, content);

                if (response.IsSuccessStatusCode)
                    Debug.WriteLine("TodoItem successfully saved.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"StackTrace: {ex.StackTrace}");
            }
        }

        public async Task DeleteTodoItemAsync(string id)
        {
            var requestUri = new Uri(string.Format(Constants.Url, id));

            try
            {
                var response = await client.DeleteAsync(requestUri);
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine("TodoItem successfully deleted.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"StackTrace: {ex.StackTrace}");
            }
        } 
        #endregion
    }
}
