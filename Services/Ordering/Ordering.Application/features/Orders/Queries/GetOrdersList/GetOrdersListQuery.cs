

using MediatR;
using System.Collections.Generic;

namespace Ordering.Application.features.Orders.Queries.GetOrdersList
{
    public class GetOrdersListQuery:IRequest<List<OrdersVm>>//کلاس درخواست و درون irequest  پاسخ رو میخواد
    {
        public string UserName { get; set; }

        public GetOrdersListQuery(string userName)
        {
            UserName = userName;
        }
    }
}
