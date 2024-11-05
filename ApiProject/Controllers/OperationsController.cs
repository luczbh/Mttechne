using ApiProject.Entities.DB;
using ApiProject.Entities.Models;
using ApiProject.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class OperationsController : ControllerBase
    {
        private readonly IOperationService _operationService;

        public OperationsController(IOperationService operationService)
        {
            _operationService = operationService;
        }

        /// <summary>
        /// Add operation
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Add(OperationModel operation)
        {
            var res = await _operationService.AddOperationAsync(operation);

            if (res)
                return Created(string.Empty, new { Success = true });

            return BadRequest();
        }

        /// <summary>
        /// Remove operation
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(OperationModel operation)
        {
            var res = await _operationService.RemoveOperationAsync(operation);

            if (res)
                return Created(string.Empty, new { Success = true });

            return BadRequest();
        }
    }
}
