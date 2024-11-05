using ApiProject.Entities.Models;
using ApiProject.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class BalanceController : ControllerBase
    {
        private readonly IBalanceService _balanceService;

        public BalanceController(IBalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        /// <summary>
        /// Consolidate operations data
        /// </summary>
        /// <returns></returns>
        [HttpPost("Consolidate")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Consolidate()
        {
            var res = await _balanceService.ConsolidateAsync();

            if (res)
                return Created(string.Empty, new { Success = true });

            return BadRequest();

        }

        /// <summary>
        /// Get Balance by date
        /// </summary>
        /// <param name="date">Send the date using dd-MM-yyyy format</param>
        /// <returns></returns>
        [HttpGet("{date}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Produces(typeof(BalanceModel))]
        public async Task<BalanceModel> Get([FromRoute] string date)
        {
            var dt = DateTime.ParseExact(date, "dd-MM-yyyy", null);
            return await _balanceService.GetBalance(dt);
        }

        /// <summary>
        /// Get Balance by client and date
        /// </summary>
        /// <param name="date">Send the date using dd-MM-yyyy format</param>
        /// <returns></returns>
        [HttpGet("{date}/ByClients")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Produces(typeof(BalanceModel))]
        public async Task<BalanceModel> GetByClients([FromRoute] string date)
        {
            var dt = DateTime.ParseExact(date, "dd-MM-yyyy", null);
            return await _balanceService.GetBalanceByClients(dt);
        }

        /// <summary>
        /// Get Balance by products and date
        /// </summary>
        /// <param name="date">Send the date using dd-MM-yyyy format</param>
        /// <returns></returns>
        [HttpGet("{date}/ByProducts")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Produces(typeof(BalanceModel))]
        public async Task<BalanceModel> GetByProducts([FromRoute] string date)
        {
            var dt = DateTime.ParseExact(date, "dd-MM-yyyy", null);
            return await _balanceService.GetBalanceByProducts(dt);
        }

        /// <summary>
        /// Get Balance by sellers and date
        /// </summary>
        /// <param name="date">Send the date using dd-MM-yyyy format</param>
        /// <returns></returns>
        [HttpGet("{date}/BySellers")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Produces(typeof(BalanceModel))]
        public async Task<BalanceModel> GetBySellers([FromRoute] string date)
        {
            var dt = DateTime.ParseExact(date, "dd-MM-yyyy", null);
            return await _balanceService.GetBalanceBySellers(dt);
        }

    }
}
