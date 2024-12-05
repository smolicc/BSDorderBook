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
            Result response = new MatchOrdersService().MatchOrders(true, amount);

            //Temp response
            return Ok(response);
        }

        [HttpPost("sell/{amount}")]
        public IActionResult SellOrder(decimal amount)
        {
            Result response = new MatchOrdersService().MatchOrders(false, amount);


            //Temp response
            return Ok(response);
        }
    }
}
