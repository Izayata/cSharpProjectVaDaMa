using Beadando.Contract;

namespace SzerverApp
{
    public interface IJobRepository
    {
        void Delete(int id);

        Job Get(int id);

        IEnumerable<Job> GetAll();

        void Upsert(Job job);
    }
}
