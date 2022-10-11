using Akka.Sample.Actors.Messages;
using Akka.Sample.Services.Impl;

namespace Akka.Sample.Services
{
    public class OrderService : IOrderService
    {
        public int CreateOrder(CreateOrderRequest createOrderRequest)
        {
            var id = createOrderRequest.OrderId;

            Thread.Sleep(2000);

            Console.WriteLine("Ben servisim " + id);

            return 2;
        }
    }
}