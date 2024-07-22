using Discount.gRPC.Data;
using Discount.gRPC.Data.Entities;
using Grpc.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Services
{
    public class DiscountService(ApplicationContext applicationContext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await applicationContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName );

            if (coupon is null)
            {
                coupon = new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount" };
            }

            logger.LogInformation("Discount is retrieved for ProductName: {productName}, Amount: {amount}", coupon.ProductName, coupon.Amount);

            return coupon.Adapt<CouponModel>();
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();

            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
            }

            applicationContext.Coupons.Add(coupon);
            await applicationContext.SaveChangesAsync();

            logger.LogInformation("Discount is successfully created. ProductName: {productName}, Amount: {amount}", coupon.ProductName, coupon.Amount);

            return coupon.Adapt<CouponModel>();
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();

            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
            }

            applicationContext.Coupons.Update(coupon);
            await applicationContext.SaveChangesAsync();

            logger.LogInformation("Discount is successfully updated. ProductName: {productName}, Amount: {amount}", coupon.ProductName, coupon.Amount);

            return coupon.Adapt<CouponModel>();
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await applicationContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (coupon is null)
            {
                throw new RpcException(
                    new Status
                    (
                        StatusCode.NotFound,
                        $"Discount with ProductName = {request.ProductName} is not found"
                    )
                );
            }

            applicationContext.Coupons.Remove(coupon);
            await applicationContext.SaveChangesAsync();

            logger.LogInformation("Discount is successfully deleted. ProductName: {productName}", coupon.ProductName);

            return new DeleteDiscountResponse { Success = true };
        }
    }
}
