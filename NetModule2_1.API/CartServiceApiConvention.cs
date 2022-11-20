using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace NetModule2_1.API
{
    public static class CartServiceApiConvention
    {
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        public static void Action() { }
    }
}
