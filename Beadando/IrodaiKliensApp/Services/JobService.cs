﻿using Beadando.Contract;
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

        public Task<IEnumerable<Job>?> GetAllJobAsync() =>
            _httpClient.GetFromJsonAsync<IEnumerable<Job>>("Jobs");

        public Task<Job?> GetJobsByIdAsync(int id) =>
            _httpClient.GetFromJsonAsync<Job?>($"Jobs/{id}");

        public async Task UpdatePersonAsync(int id, Job job) =>
           await _httpClient.PutAsJsonAsync($"Job/{id}", job);

        public async Task DeletePersonAsync(int id) =>
            await _httpClient.DeleteAsync($"Job/{id}");

        public async Task AddPersonAsync(Job job) =>
            await _httpClient.PostAsJsonAsync("Job", job);
    }
}
