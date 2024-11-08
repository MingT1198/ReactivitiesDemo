using Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Repository.ViewModels;

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
            try
            {
                return Ok(await _activityService.GetAsync());
            }
            catch (Exception ex)
            {
                string errorMeg;
                if (ex.InnerException != null)
                {
                    errorMeg = ex.InnerException.Message;
                }
                else
                {
                    errorMeg = ex.Message;
                }

                return BadRequest(errorMeg);
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(Guid Id)
        {
            try
            {
                return Ok(await _activityService.GetByIdAsync(Id));
            }
            catch (Exception ex)
            {
                string errorMeg;
                if (ex.InnerException != null)
                {
                    errorMeg = ex.InnerException.Message;
                }
                else
                {
                    errorMeg = ex.Message;
                }

                return BadRequest(errorMeg);
            }
        }
    }
}