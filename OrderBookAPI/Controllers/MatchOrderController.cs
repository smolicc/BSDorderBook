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
            List<CryptoExchange> cryptoExchanges = dr.LoadCryptoExchanges();
            List<OrderBook> orderBooks = dr.LoadOrderBooks();

            Result response = new MatchOrdersService().MatchOrders(cryptoExchanges, orderBooks, true, amount);

            //Temp response
            return Ok(response);
        }

        [HttpPost("sell/{amount}")]
        public IActionResult SellOrder(decimal amount)
        {
            List<CryptoExchange> cryptoExchanges = dr.LoadCryptoExchanges();
            List<OrderBook> orderBooks = dr.LoadOrderBooks();

            Result response = new MatchOrdersService().MatchOrders(cryptoExchanges, orderBooks, false, amount);

            //Temp response
            return Ok(response);
        }
    }
}
