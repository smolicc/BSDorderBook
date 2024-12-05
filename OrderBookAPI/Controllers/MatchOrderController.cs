using CoreLibrary.Services;
using CoreLibrary.Data;
using CoreLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchOrderController : ControllerBase
    {
        [HttpPost("buy/{amount}")]
        public IActionResult BuyOrder(decimal amount)
        {
            List<string> response = new MatchOrdersService().MatchOrders(true, amount);

            //Temp response
            return Ok(response[1]);
        }

        [HttpPost("sell/{amount}")]
        public IActionResult SellOrder(decimal amount)
        {
            List<string> response = new MatchOrdersService().MatchOrders(false, amount);

            //Temp response
            return Ok(response[1]);
        }
    }
}
