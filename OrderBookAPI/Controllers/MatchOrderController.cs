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
            //update paths
            var dr = new DataRepository("CryptoExchangesData.json", "OrderBooksData.json");

            List<string> response = new MatchOrdersService().MatchOrders(dr.LoadCryptoExchanges(), dr.LoadOrderBooks(), true, amount);

            //Temp response
            return Ok(response[1]);
        }
    }
}
