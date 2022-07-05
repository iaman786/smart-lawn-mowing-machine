namespace Slmm.WebApplication.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using SmartLawnMowing.Domain.Presentation;
    using SmartLawnMowing.Domain.Presentation.Views;
    using slmm.LawnMowing.Api.Service;

    [Route("api/[controller]")]
    [ApiController]
    public class MowerController : ControllerBase
    {
        private readonly MowerService _service;

        public MowerController(MowerService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("position")]
        public async Task<ActionResult<PositionView>> Get()
        {
            var position = await _service.GetPosition();
            return new ActionResult<PositionView>(position);
        }

        [HttpPost]
        [Route("move")]
        public async Task<ActionResult> MoveMower()
        {
            var result = await _service.Move();
            return CreateResponse(result);
        }

        [HttpPost]
        [Route("turn")]
        public async Task<ActionResult> TurnMower([FromBody] string rotateInDirection)
        {
            var result = await _service.Turn(rotateInDirection);
            
            return CreateResponse(result);
        }

        private ActionResult CreateResponse(MowerResponse mowerResponseResult)
        {
            if (mowerResponseResult == MowerResponse.InvalidInput)
            {
                return BadRequest();
            }

            if (mowerResponseResult == MowerResponse.OutOfRange)
            {
                return Accepted("", "The Mower cannot move beyond the garden boundary");
            }

            return Ok();
        }
    }
}
