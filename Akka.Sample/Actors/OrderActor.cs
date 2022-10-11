using Akka.Actor;
using Akka.Sample.Actors.Messages;
using Akka.Sample.Services.Impl;

namespace Akka.Sample.Actors
{
    public class OrderActor : ReceiveActor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IOrderService _orderService;
        private readonly IActorRef _childActor;

        public OrderActor(IServiceProvider serviceProvider)
        {
            _orderService = serviceProvider.GetService<IOrderService>();
            _childActor = Context.ActorOf(Props.Create<PaymentActor>(_serviceProvider));

            Receive<CreateOrderRequest>(message =>
            {
                var orderId = message.OrderId;
                var orderPrice = message.OrderPrice;
                Console.WriteLine("ben aktörüm " + orderId);
                var generatedId = _orderService.CreateOrder(message);

                _childActor.Tell(generatedId);
            });
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(
                maxNrOfRetries: 10,
                withinTimeRange: TimeSpan.FromSeconds(10),
                localOnlyDecider: ex =>
                {
                    return ex switch
                    {
                        ArgumentException ae => Directive.Resume,
                        NullReferenceException ne => Directive.Restart,
                        _ => Directive.Stop
                    };
                }
                );
        }

        protected override void PreStart() => Console.WriteLine("Actor started");

        protected override void PostStop() => Console.WriteLine("Actor stopped");
    }
}