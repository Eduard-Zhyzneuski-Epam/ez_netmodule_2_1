using Autofac;
using NetModule2_1.BAL;

namespace NetModule2_1.DefaultBusinessLogic
{
    public class BusinessLogicModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CartService>().As<ICartService>();
        }
    }
}
