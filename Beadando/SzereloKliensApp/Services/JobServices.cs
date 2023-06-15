using Beadando.Contract;
using System;
using System.Net.Http.Json;
using SzereloKliensApp.Services;


namespace IrodaiKliensApp.Services
{
    public class JobService : IJobsServices
    {
        private readonly HttpClient _httpClient;

        public JobService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<IEnumerable<Job>?> GetAllJobAsync()
        {
            return _httpClient.GetFromJsonAsync<IEnumerable<Job>>("Job");
        }


        public Task<Job?> GetJobsByIdAsync(int id) =>
            _httpClient.GetFromJsonAsync<Job?>($"Job/{id}");

        public async Task UpdatePersonAsync(int id, Job job) =>
           await _httpClient.PutAsJsonAsync($"Job/{id}", job);

        public async Task DeletePersonAsync(int id) =>
            await _httpClient.DeleteAsync($"Job/{id}");

        public async Task AddPersonAsync(Job job) =>
            await _httpClient.PostAsJsonAsync("Job", job);
    }
}
