using Akka.Sample.Actors.Messages;

namespace Akka.Sample.Services.Impl
{
    public interface IOrderService
    {
        int CreateOrder (CreateOrderRequest createOrderRequest);
    }
}