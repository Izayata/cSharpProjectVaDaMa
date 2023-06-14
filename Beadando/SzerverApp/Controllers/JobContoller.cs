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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> Get()
        {
            _logger.LogInformation("Jobs endpoint was called");
            var people = await _beadandoContext.Jobs.ToListAsync();
            return Ok(people);
        }

        

    }
}
