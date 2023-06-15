using Beadando.Contract;
using System;
using System.Net.Http.Json;


namespace IrodaiKliensApp.Services
{
    public class JobService : IJobsService
    {
        private readonly HttpClient _httpClient;

        public JobService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<IEnumerable<Job>?> GetAllJobAsync()
        {
            return _httpClient.GetFromJsonAsync<IEnumerable<Job>>("JobContoller");
        }
        

        public Task<Job?> GetJobsByIdAsync(int id) =>
            _httpClient.GetFromJsonAsync<Job?>($"JobContoller/{id}");

        public async Task UpdatePersonAsync(int id, Job job) =>
           await _httpClient.PutAsJsonAsync($"JobContoller/{id}", job);

        public async Task DeletePersonAsync(int id) =>
            await _httpClient.DeleteAsync($"JobContoller/{id}");

        public async Task AddPersonAsync(Job job) =>
            await _httpClient.PostAsJsonAsync("JobContoller", job);
    }
}
