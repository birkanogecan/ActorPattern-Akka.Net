using Akka.Actor;

namespace Akka.Sample.Actors
{
    public class PaymentActor : ReceiveActor
    {
        public PaymentActor(IServiceCollection services)
        {
            Receive<int>(message =>
            {
                var orderId = message;
                Console.WriteLine("ben child aktörüm " + orderId);
            });
        }

        protected override void PreStart() => Console.WriteLine("Child Actor started");

        protected override void PostStop() => Console.WriteLine("Child Actor stopped");
    }
}