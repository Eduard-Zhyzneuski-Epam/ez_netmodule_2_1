using Autofac;
using NetModule2_1.DAL;

namespace NetModule2_1.LiteDb
{
    internal class LiteDbModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CartRepository>().As<ICartRepository>().SingleInstance();
        }
    }
}
