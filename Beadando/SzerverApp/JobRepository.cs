using Beadando.Contract;
using System;

namespace SzerverApp
{
    public class JobRepository : IJobRepository
    {
        private readonly Dictionary<int, Job> _job = new();

        public void Delete(int id) => _job.Remove(id);

        public Job Get(int id) => _job.TryGetValue(id, out var job) ? job : null;

        public IEnumerable<Job> GetAll() => _job.Values;

        public void Upsert(Job job) => _job[job.Id] = job;
    }
}
