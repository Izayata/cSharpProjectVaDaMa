using Beadando.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace SzerverApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobContoller : ControllerBase
    {
        private readonly BeadandoContext _beadandoContext;
        private readonly ILogger<JobContoller> _logger;

        public JobContoller(BeadandoContext beadandoContext, ILogger<JobContoller> logger)
        {
            _beadandoContext = beadandoContext;
            _logger = logger;
        }

    

        private double ManHourEstimation(Job job)
        {
            double jobCategoryValue = 0;
            double ageValue = 0;
            double severityValue = 0;

            switch (job.Category) {
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
