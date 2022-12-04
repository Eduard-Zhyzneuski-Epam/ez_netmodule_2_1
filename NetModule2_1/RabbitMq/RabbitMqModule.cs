using Autofac;
using NetModule2_1.EAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetModule2_1.RabbitMq
{
    public class RabbitMqModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ChangedItemEventSubscriber>().As<IChangedItemEventSubscriber>().InstancePerLifetimeScope();
        }
    }
}
