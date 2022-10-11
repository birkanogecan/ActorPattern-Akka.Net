namespace Akka.Sample.Actors.Messages
{
    public class CreateOrderRequest
    {
        //Actor pattern message objeleri immutable olması gerekir.
        private readonly int _orderId;

        private readonly int _productId;
        private readonly int _orderPrice;

        public CreateOrderRequest(int orderId, int productId, int orderPrice)
        {
            _orderId = orderId;
            _productId = productId;
            _orderPrice = orderPrice;
        }

        public int OrderId
        { get { return _orderId; } }
        public int ProductId
        { get { return _productId; } }
        public int OrderPrice
        { get { return _orderPrice; } }
    }
}