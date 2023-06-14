using Beadando.Contract;
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
	}
}
