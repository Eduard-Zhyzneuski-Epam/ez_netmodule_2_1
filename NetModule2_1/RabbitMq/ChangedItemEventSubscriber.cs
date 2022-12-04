using NetModule2_1.BAL;
using NetModule2_1.EAL;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NetModule2_1.RabbitMq
{
    internal class ChangedItemEventSubscriber : IChangedItemEventSubscriber
    {
        private readonly ICartService cartService;
        private IModel? session = null;
        private bool disposed = false;

        public ChangedItemEventSubscriber(ICartService cartService)
        {
            this.cartService = cartService;
        }

        public void Subscribe()
        {
            session ??= OpenSession();
            session.QueueDeclare("item_changed", true, false, false);
            var consumer = new EventingBasicConsumer(session);
            consumer.Received += (sender, ea) =>
            {
                try
                {
                    var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                    var changedItem = JsonSerializer.Deserialize<EAL.ChangedItem>(json);
                    var item = Mapping.Map<EAL.ChangedItem, BAL.Item>(changedItem);
                    cartService.UpdateItem(item);
                    session.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception)
                {
                    session.BasicNack(ea.DeliveryTag, false, false);
                }
            };
            session.BasicConsume("item_changed", false, consumer);
        }

        private IModel OpenSession()
        {
            var connectionFactory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
            var channel = connectionFactory.CreateConnection();
            return channel.CreateModel();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ChangedItemEventSubscriber() => Dispose(false);

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    session?.Dispose();
                }
                disposed = true;
            }
        }
    }
}
