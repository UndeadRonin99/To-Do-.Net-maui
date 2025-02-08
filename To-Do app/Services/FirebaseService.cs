using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using To_Do_app.Pages;

namespace To_Do_app.Services
{
    internal class FirebaseService
    {
        private readonly HttpClient _httpClient;
        private const string FirebaseUrl = "https://to-do-7265f-default-rtdb.europe-west1.firebasedatabase.app/";

        public FirebaseService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> AddToDoItem(Item item)
        {
            var response = await _httpClient.PostAsJsonAsync($"{FirebaseUrl}/todos.json", item);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<Item>> GetUserTasks(string userId)
        {
            var response = await _httpClient.GetAsync($"{FirebaseUrl}/todos.json");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Failed to fetch data from Firebase.");
                return new List<Item>();
            }

            var json = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(json) || json == "null")
            {
                Console.WriteLine("Firebase data is empty or null.");
                return new List<Item>();
            }

            try
            {
                var allTasks = JsonConvert.DeserializeObject<Dictionary<string, Item>>(json);
                if (allTasks == null || allTasks.Count == 0)
                {
                    Console.WriteLine("Deserialization failed or returned no tasks.");
                    return new List<Item>();
                }

                // Filter tasks by UID
                return allTasks.Values.Where(t => t.UID == userId).ToList();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON Deserialization error: {ex.Message}");
                return new List<Item>();
            }
        }

        public async Task<bool> DeleteTask(Item task)
        {
            var response = await _httpClient.DeleteAsync($"{FirebaseUrl}/todos/{task.id}.json");

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTask(Item task)
        {
            var response = await _httpClient.PutAsJsonAsync($"{FirebaseUrl}/todos/{task.id}.json", task);

            return response.IsSuccessStatusCode;
        }

    }
}
