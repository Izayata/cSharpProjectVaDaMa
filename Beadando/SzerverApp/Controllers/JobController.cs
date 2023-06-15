using Beadando.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> Get()
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

            this._beadandoContext.Jobs.Add(job);
            await this._beadandoContext.SaveChangesAsync();

            return this.Ok();
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
