using Beadando.Contract;
using System;

namespace IrodaiKliensApp.Services
{
    public interface IJobsService
    {
        Task<IEnumerable<Job>?> GetAllJobAsync();

        Task<Job?> GetJobsByIdAsync(int id);

        Task UpdatePersonAsync(int id, Job job);

        Task DeletePersonAsync(int id);

        Task AddPersonAsync(Job job);
    }
}
