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
        readonly DataRepository dr = new("CryptoExchangesData.json", "OrderBooksData.json");

        [HttpPost("buy/{amount}")]
        public IActionResult BuyOrder(decimal amount)
        {
            if (amount <= 0)
                return BadRequest(new { Message = "Amount must be greater than zero." });

            try
            {
                List<CryptoExchange> cryptoExchanges = dr.LoadCryptoExchanges();
                List<OrderBook> orderBooks = dr.LoadOrderBooks();

                Result result = new MatchOrdersService().MatchOrders(cryptoExchanges, orderBooks, "buy", amount);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing the request.", Details = ex.Message });
            }
        }

        [HttpPost("sell/{amount}")]
        public IActionResult SellOrder(decimal amount)
        {
            if (amount <= 0)
                return BadRequest(new { Message = "Amount must be greater than zero." });

            try
            {
                List<CryptoExchange> cryptoExchanges = dr.LoadCryptoExchanges();
                List<OrderBook> orderBooks = dr.LoadOrderBooks();

                Result result = new MatchOrdersService().MatchOrders(cryptoExchanges, orderBooks, "sell", amount);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing the request.", Details = ex.Message });
            }
        }
    }
}
