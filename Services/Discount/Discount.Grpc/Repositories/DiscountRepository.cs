
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;
using System.Threading.Tasks;
using Discount.Grpc.Repositories;
using Discount.Grpc.Entites;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration["DatabaseSetting:ConnectionString"]);
            var affected = await connection.ExecuteAsync
                ("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

            if (affected == 0) return false;

            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration["DatabaseSetting:ConnectionString"]);
            var affected = await connection.ExecuteAsync
               ("DELETE FROM Coupon WHERE ProductName=@ProductName",
               new { ProductName = productName });

            if (affected == 0) return false;

            return true;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration["DatabaseSetting:ConnectionString"]);
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupon WHERE ProductName= @ProductName", new { ProductName = productName });
            if(coupon == null)
            {
                return new Coupon()
                {
                    ProductName = "NO discount",
                    Amount = 0,
                    Description = "not found"
                };
               
            }
            return coupon;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration["DatabaseSetting:ConnectionString"]);

            var affected = await connection.ExecuteAsync
                ("UPDATE Coupon SET ProductName=@ProductName, Description=@Description, Amount=@Amount WHERE id=@CouponId",
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, CouponId=coupon.Id });

            if (affected == 0) return false;

            return true;
        }
    }
}
