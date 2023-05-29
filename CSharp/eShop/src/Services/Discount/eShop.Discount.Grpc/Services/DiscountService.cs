using eShop.Discount.Grpc.Protos;
using eShop.Discount.Shared.Entities;
using eShop.Discount.Shared.Repositories;

using AutoMapper;
using Grpc.Core;

namespace eShop.Discount.Grpc.Services
{
    public class DiscountService : DiscountGrpcService.DiscountGrpcServiceBase
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _autoMapper;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(
            IDiscountRepository discountRepository,
            ILogger<DiscountService> logger,
            IMapper autoMapper)
        {
            _discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _autoMapper = autoMapper ?? throw new ArgumentNullException(nameof(autoMapper));
        }

        public override async Task<CouponModels> GetAll(GetAllDiscountsRequest request, ServerCallContext context)
        {
            var allCoupons = new CouponModels();

            allCoupons.Coupons.AddRange(
                (await _discountRepository.GetAll())?
                .Select(coupon => _autoMapper.Map<CouponModel>(coupon))
                ?? Enumerable.Empty<CouponModel>());

            return allCoupons;
        }

        public override async Task<CouponModel> Get(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _discountRepository.Get(request.ProductName)
                ?? throw new RpcException(new Status(StatusCode.NotFound, request.ProductName));

            _logger.LogInformation(
                "Returned coupon - ProductName: {productName}, Amount: {amount}", coupon.ProductName, coupon.Amount);

            return _autoMapper.Map<CouponModel>(coupon);
        }

        public override async Task<SuccessStatusResponse> Create(CreateDiscountRequest request, ServerCallContext context)
        {
            var created = await _discountRepository.Create(_autoMapper.Map<Coupon>(request.Coupon));

            return new SuccessStatusResponse()
            {
                Success = created,
            };
        }

        public override async Task<SuccessStatusResponse> Update(UpdateDiscountRequest request, ServerCallContext context)
        {
            var updated = await _discountRepository.Update(_autoMapper.Map<Coupon>(request.Coupon));

            return new SuccessStatusResponse()
            {
                Success = updated,
            };
        }

        public override async Task<SuccessStatusResponse> Delete(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted = await _discountRepository.Delete(request.ProductName);

            return new SuccessStatusResponse()
            {
                Success = deleted,
            };
        }
    }
}
