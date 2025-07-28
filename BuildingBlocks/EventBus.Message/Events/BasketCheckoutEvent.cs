

namespace EventBus.Message.Events
{
    public class BasketCheckoutEvent:IntegrationBaseEvent
    {
       
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double TotalPrice { get; set; }
        public string City { get; set; }
    }
}
