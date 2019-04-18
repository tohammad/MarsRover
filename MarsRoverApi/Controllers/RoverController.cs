using MarsRoverApi.Enums;
using MarsRoverApi.Factory;
using MarsRoverApi.Filters;
using MarsRoverApi.Models;
using MarsRoverApi.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarsRoverApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [HandleException]
    public class RoverController : ControllerBase
    {
        /// <summary>
        /// Create new instance of Rover
        /// </summary>
        [HttpPost]
        public IActionResult Post(RoverModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.RoverId <= 0)
            {
                return BadRequest(Messages.InvalidId);
            }
            try
            {
                var rover = RoverFactory.Create(model.RoverId, model.RoverName, 0, 0, Direction.N);
                return Ok(new { RoverId = rover.Id, RoverName = rover.Name });
            }
            catch (System.Exception ex)
            {
                if (ex.Message == "EXISTS")
                {
                    return Conflict(new ApiResponse
                    {
                        Status = 409,
                        Message = Messages.RoverAlreadyExist
                    });
                }
                else
                {
                    return StatusCode(500);
                }
            }
        }

        /// <summary>
        /// Rename and existing instance of Rover
        /// </summary>
        [HttpPut("/{roverId:int}")]
        public IActionResult Put(int roverId, RoverModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var rover = RoverFactory.Update(roverId, model.RoverName);
            return Ok(new { RoverId = rover.Id, RoverName = rover.Name });
        }

        /// <summary>
        /// Move Rover as per instruction
        /// </summary>
        [HttpPost("move/{roverId:int}")]
        public IActionResult Move(int roverId, RoverMoveModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var rover = RoverFactory.GetRover(roverId);
            if (rover == null)
            {
                throw new RoverException(Messages.RoverDoesnotExist);
            }
            var movements = new List<Movement>();
            foreach (var inst in model.MovementInstruction.ToUpper())
            {
                switch (inst)
                {
                    case 'L':
                        movements.Add(Movement.Left);
                        break;

                    case 'R':
                        movements.Add(Movement.Right);
                        break;

                    case 'M':
                        movements.Add(Movement.Move);
                        break;
                }
            }
            rover.Move(movements);
            return Ok(new RoverMoveResponse
            {
                RoverId = rover.Id,
                RoverName = rover.Name,
                CurrentX = rover.Position.X,
                CurrentY = rover.Position.Y,
                CurrentDirection = rover.Position.Direction.ToString()
            });
        }

        /// <summary>
        /// Get current postion of Rover
        /// </summary>
        /// <param name="roverId"></param>
        /// <returns></returns>
        [HttpGet("position/{roverId:int}")]
        public IActionResult GetPosition(int roverId)
        {
            var rover = RoverFactory.GetRover(roverId);
            if (rover == null)
            {
                return NotFound();
            }
            return Ok(new RoverMoveResponse
            {
                RoverId = rover.Id,
                RoverName = rover.Name,
                CurrentX = rover.Position.X,
                CurrentY = rover.Position.Y,
                CurrentDirection = rover.Position.Direction.ToString()
            });
        }
    }
}