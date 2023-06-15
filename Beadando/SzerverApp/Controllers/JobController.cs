using Beadando.Contract;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.RegularExpressions;

namespace SzerverApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private readonly BeadandoContext _beadandoContext;
        private readonly ILogger<JobController> _logger;

        public JobController(BeadandoContext beadandoContext, ILogger<JobController> logger)
        {
            this._beadandoContext = beadandoContext;
            this._logger = logger;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingJob = await this._beadandoContext.Jobs.FindAsync(id);

            if (existingJob is null)
            {
                return this.NotFound();
            }

            this._beadandoContext.Jobs.Remove(existingJob);
            await this._beadandoContext.SaveChangesAsync();

            return this.NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetAll()
        {
            this._logger.LogInformation("Jobs endpoint was called");
            var jobs = await this._beadandoContext.Jobs.ToListAsync();
            return this.Ok(jobs);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> Get(int id)
        {
            var job = await this._beadandoContext.Jobs.FindAsync(id);

            if (job is null)
            {
                return this.NotFound();
            }


            return this.Ok(job);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Job job)
        {
            if (!JobValidation(job))
            {
                return this.UnprocessableEntity();
            }

            job.ManHourEstimation = ManHourEstimation(job);
            job.Status = JobStatus.Accepted;

            this._beadandoContext.Jobs.Add(job);
            await this._beadandoContext.SaveChangesAsync();

            return this.Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Job job)
        {
            if (id != job.Id)
            {
                return this.BadRequest();
            }

            var existingJob = await this._beadandoContext.Jobs.FindAsync(id);

            if (existingJob is null)
            {
                return this.NotFound();
            }

            if (!JobValidation(job))
            {
                return this.UnprocessableEntity();
            }

            existingJob.ClientFirstName = job.ClientFirstName;
            existingJob.ClientLastName = job.ClientLastName;
            existingJob.CarType = job.CarType;
            existingJob.LicensePlateNumber = job.LicensePlateNumber;
            existingJob.ManufacturingYear = job.ManufacturingYear;
            existingJob.Category = job.Category;
            existingJob.Description = job.Description;
            existingJob.Severity = job.Severity;
            existingJob.ManHourEstimation = ManHourEstimation(existingJob);
            await this._beadandoContext.SaveChangesAsync();

            return this.NoContent();
        }

        [HttpPut("{id}/{status}")]
        public async Task<IActionResult> Put(int id, JobStatus status)
        {

            var existingJob = await this._beadandoContext.Jobs.FindAsync(id);

            if (existingJob is null)
            {
                return this.NotFound();
            }

            if (!Enum.IsDefined(typeof(JobStatus), status))
            {
                return this.UnprocessableEntity();
            }

            existingJob.Status = status;
            await this._beadandoContext.SaveChangesAsync();

            return this.NoContent();
        }

        private static bool JobValidation(Job job)
        {
            if (job is null)
            {
                return false;
            }

            job.ClientFirstName = ValidateString(job.ClientFirstName);
            if (job.ClientFirstName.IsNullOrEmpty())
            {
                return false;
            }

            job.ClientLastName = ValidateString(job.ClientLastName);
            if (job.ClientLastName.IsNullOrEmpty())
            {
                return false;
            }

            job.CarType = ValidateString(job.CarType);
            if (job.CarType.IsNullOrEmpty())
            {
                return false;
            }

            Regex rgx = new Regex("[A-Z]{3}-[0-9]{3}");
            if (!rgx.IsMatch(job.LicensePlateNumber)) {
                return false;
            }


            if (!Enum.IsDefined(typeof(JobCategory), job.Category))
            {
                return false;
            }

            if (!Enum.IsDefined(typeof(JobStatus), job.Status))
            {
                return false;
            }

            return true;
        }

        private static string ValidateString(string str)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9]");
            str = rgx.Replace(str, "");

            return str;
        }

        private static double ManHourEstimation(Job job)
        {
            double jobCategoryValue = 0;
            double ageValue = 0;
            double severityValue = 0;

            switch (job.Category)
            {
                case JobCategory.Body:
                    jobCategoryValue = 3;
                    break;
                case JobCategory.Engine:
                    jobCategoryValue = 8;
                    break;
                case JobCategory.Undercarriage:
                    jobCategoryValue = 6;
                    break;
                case JobCategory.Brakes:
                    jobCategoryValue = 4;
                    break;
            }

            ageValue = DateTime.Now.Year - job.ManufacturingYear;

            if (ageValue >= 0 && ageValue < 5)
            {
                ageValue = 0.5;
            }
            else if (ageValue >= 5 && ageValue < 10)
            {
                ageValue = 1;
            }
            else if (ageValue >= 10 && ageValue < 20)
            {
                ageValue = 1.5;
            }
            else if (ageValue >= 20)
            {
                ageValue = 2;
            }


            int severity = job.Severity;

            if (severity >= 1 && severity <= 2)
            {
                severityValue = 0.2;
            }
            else if (severity >= 3 && severity <= 4)
            {
                severityValue = 0.4;
            }
            else if (severity >= 5 && severity <= 7)
            {
                severityValue = 0.6;
            }
            else if (severity >= 8 && severity <= 9)
            {
                severityValue = 0.8;
            }
            else if (severity == 10)
            {
                severityValue = 1;
            }

            return jobCategoryValue * ageValue * severityValue;
        }

    }
}
