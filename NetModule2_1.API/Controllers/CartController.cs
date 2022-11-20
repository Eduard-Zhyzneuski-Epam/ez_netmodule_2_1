using Microsoft.AspNetCore.Mvc;
using NetModule2_1.API.Models;
using NetModule2_1.BAL;
using Swashbuckle.AspNetCore.Annotations;

namespace NetModule2_1.API.Controllers
{
    /// <summary>
    /// Cart service only controller
    /// </summary>
    [ApiController]
    [ApiVersion("1.0"), ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}")]
    public class CartController : ControllerBase
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        /// <summary>
        /// Cart list of items
        /// </summary>
        /// <param name="id">Cart id</param>
        /// <returns>Returns list of items from cart identified by route parameter</returns>
        [MapToApiVersion("2.0")]
        [HttpGet("cart/{id}", Name = nameof(GetCartInfo))]
        [SwaggerResponse(200, Type = typeof(List<Models.Item>), ContentTypes = new[] { "application/json" })]
        public List<Models.Item> GetCartInfo([FromRoute] string id)
        {
            var rawItems = cartService.GetItemsList(id);
            var items = Mapping.MapList<BAL.Item, Models.Item>(rawItems);
            return items;
        }

        /// <summary>
        /// Cart model
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns cart model from cart identified </returns>
        [MapToApiVersion("1.0")]
        [HttpGet("cart/{id}", Name = nameof(GetCartInfo))]
        [SwaggerResponse(200, Type = typeof(Cart), ContentTypes = new[] { "application/json" })]
        public Cart GetCartInfoObsolete([FromRoute] string id)
        {
            var rawItems = cartService.GetItemsList(id);
            var items = Mapping.MapList<BAL.Item, Models.Item>(rawItems);
            return new Cart
            {
                Id = id,
                Items = items
            };
        }

        /// <summary>
        /// Cart item - create
        /// </summary>
        /// <param name="cartId">String cart id</param>
        /// <param name="item">Item id</param>
        [MapToApiVersion("1.0"), MapToApiVersion("2.0")]
        [HttpPost("cart/{cartId}/items", Name = nameof(AddItem))]
        [SwaggerResponse(200)]
        public void AddItem([FromRoute] string cartId, [FromBody] Models.Item item)
        {
            var rawItem = Mapping.Map<Models.Item, BAL.Item>(item);
            cartService.AddItem(cartId, rawItem);
        }

        /// <summary>
        /// Cart item - remove
        /// </summary>
        /// <param name="cartId">String cart id</param>
        /// <param name="itemId">Item id</param>
        [MapToApiVersion("1.0"), MapToApiVersion("2.0")]
        [HttpDelete("cart/{cartId}/item/{itemId}", Name = nameof(DeleteItem))]
        [SwaggerResponse(200)]
        public void DeleteItem([FromRoute] string cartId, [FromRoute] int itemId)
        {
            cartService.RemoveItem(cartId, itemId);
        }
    }
}