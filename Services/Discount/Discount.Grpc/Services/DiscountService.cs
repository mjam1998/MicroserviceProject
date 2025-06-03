using AutoMapper;
using Discount.Grpc.Entites;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using System.Threading.Tasks;

namespace Discount.Grpc.Services
{
    public class DiscountService: DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository discountRepository,IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
        }
        public override async Task<CouponModel> GetDiscounrt(GetDiscounrtRequest request, ServerCallContext context)
        {
            var coupon = await _discountRepository.GetDiscount(request.Productname);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount for {request.Productname} not fpund"));
            }
            return _mapper.Map<CouponModel>(coupon);
        }
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon=_mapper.Map<Coupon>(request.Coupon);
            await _discountRepository.CreateDiscount(coupon);
             var couponModel=_mapper.Map<CouponModel>(coupon);
            return couponModel;
        }
        public override async Task<CouponModel> UpdateDiscount(UpdateDiscounRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            await _discountRepository.UpdateDiscount(coupon);

            return _mapper.Map<CouponModel>(coupon);
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted= await _discountRepository.DeleteDiscount(request.Productname);
            return new DeleteDiscountResponse
            {
                Success = deleted
            };
        }
    }
}
