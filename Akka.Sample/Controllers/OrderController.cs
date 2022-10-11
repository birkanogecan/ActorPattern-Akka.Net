using Akka.Actor;
using Akka.DependencyInjection;
using Akka.Sample.Actors;
using Akka.Sample.Actors.Messages;
using Microsoft.AspNetCore.Mvc;

namespace Akka.Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private ActorSystem _actorSystem;
        private readonly IServiceProvider _serviceProvider;

        public OrderController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var bootstrap = BootstrapSetup.Create();
            var diSetup = DependencyResolverSetup.Create(_serviceProvider);

            // merge this setup (and any others) together into ActorSystemSetup
            var actorSystemSetup = bootstrap.And(diSetup);

            // start ActorSystem
            _actorSystem = ActorSystem.Create("actor-system", actorSystemSetup);
        }

        [HttpGet]
        public string CreateOrder()
        {
            var actor = _actorSystem.ActorOf(Props.Create<OrderActor>(_serviceProvider));

            CreateOrderRequest createOrderRequest = new CreateOrderRequest(1, 2, 3);
            actor.Tell(createOrderRequest);

            return "İşleme alındı";

            // _actorSystem.Stop(actor);
        }
    }
}