﻿

using MediatR;

namespace Ordering.Application.features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand:IRequest
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public double TotalPrice { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddres { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string BankName { get; set; }
        public string RefCode { get; set; }
        public int PaymentMethod { get; set; }
    }
}
