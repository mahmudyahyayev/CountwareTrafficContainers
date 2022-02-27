using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Sensormatic.Tool.Scheduler.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQuartzSchedulerService _schedulerService;
        private readonly IConfiguration _configuration;

        public HomeController(IQuartzSchedulerService schedulerService, IConfiguration configuration)
        {
            _schedulerService = schedulerService;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var jobs = await _schedulerService.GetJobsAsync();
            return View();
        }

        [HttpGet("jobs")]
        public async Task<IActionResult> GetJobs()=> Json(await _schedulerService.GetJobsAsync());

        [HttpPost("jobs/{jobName}")]
        public async Task TriggerJob(string jobName)=> await _schedulerService.TriggerJobAsync(jobName);

        [HttpDelete("jobs/{jobName}")]
        public async Task PauseJob(string jobName)=> await _schedulerService.PauseJobAsync(jobName);

        [HttpPut("jobs/{jobName}")]
        public async Task ResumeJob(string jobName) => await _schedulerService.ResumeJobAsync(jobName);

        [HttpDelete("jobs")]
        public async Task PauseJobs(string jobName) => await _schedulerService.PauseJobsAsync();

        [HttpPut("jobs")]
        public async Task ResumeJobs(string jobName) => await _schedulerService.ResumeJobsAsync();

        public IActionResult Error()
        {
            ViewData["RequestId"] = HttpContext.TraceIdentifier;
            return View();
        }
    }
}
