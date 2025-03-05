using Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    public class ActivityController: ApiControllerBase
    {
        private readonly IActivityService _activityService;
        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _activityService.GetAsync());
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(Guid Id)
        {
            return Ok(await _activityService.GetByIdAsync(Id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(ActivityModel model)
        {
            await _activityService.AddAsync(model);
            return Ok();
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(Guid Id, ActivityModel model)
        {
            await _activityService.PutByIdAsync(Id, model);
            return Ok();
        }
    }
}