using Discount.Grpc.Protos;
using System.Threading.Tasks;

namespace Basket.Api.GrpcServices
{
    public class DiscountGrpcService//ارتباط با سرویس تخفیف بوسیله grpc
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
        {
            _discountProtoService = discountProtoService;
        }
        public async Task<CouponModel> GetDiscounts(string productName)
        {
            var deiscountRequest = new GetDiscounrtRequest { Productname = productName };
            return await _discountProtoService.GetDiscounrtAsync(deiscountRequest);
        }   
    }    
}
