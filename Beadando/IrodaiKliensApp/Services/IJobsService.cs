using Beadando.Contract;

namespace IrodaiKliensApp.Services
{
	public interface IJobsService
	{
		Task<IEnumerable<Job>?> GetAllJobAsync();
		Task<Job?> GetJobsByIdAsync(int id);
	}
}
