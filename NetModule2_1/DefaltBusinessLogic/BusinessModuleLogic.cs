using Autofac;
using NetModule2_1.BAL;

namespace NetModule2_1.DefaultBusinessLogic
{
    internal class BusinessModuleLogic : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CartService>().As<ICartService>();
        }
    }
}
