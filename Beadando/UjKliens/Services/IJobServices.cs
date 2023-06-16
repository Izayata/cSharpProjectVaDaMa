using Beadando.Contract;
using System;

namespace UjKliens.Services
{
    public interface IJobServices
    {
        Task<IEnumerable<Job>?> GetAllJobAsync();

        Task<Job?> GetJobsByIdAsync(int id);

        Task UpdateJobStatusAsync(int id, JobStatus status);

        Task DeletePersonAsync(int id);

        Task AddPersonAsync(Job job);
    }
}
