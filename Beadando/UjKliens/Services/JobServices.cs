using Beadando.Contract;
using System;
using System.Net.Http.Json;


namespace UjKliens.Services
{
    public class JobServices : IJobServices
    {
        private readonly HttpClient _httpClient;

        public JobServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<IEnumerable<Job>?> GetAllJobAsync()
        {
            return _httpClient.GetFromJsonAsync<IEnumerable<Job>>("Job");
        }


        public Task<Job?> GetJobsByIdAsync(int id)
        {
            return _httpClient.GetFromJsonAsync<Job?>($"Job/{id}");
        }

        public async Task UpdateJobStatusAsync(int id, JobStatus status) =>
           await _httpClient.PutAsJsonAsync($"Job/{id}/{status}", status);

        public async Task DeletePersonAsync(int id) =>
            await _httpClient.DeleteAsync($"Job/{id}");

        public async Task AddPersonAsync(Job job) =>
            await _httpClient.PostAsJsonAsync("Job", job);
    }
}
