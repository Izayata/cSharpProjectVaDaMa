using Microsoft.AspNetCore.Mvc;

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

    }
}
