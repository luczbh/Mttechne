using ApiProject.Entities.DB;
using ApiProject.Entities.Enumerators;
using ApiProject.Infrastructure.Repository;
using ApiProject.Repository;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Test
{
    public abstract class DatabaseTest : BaseTest
    {
        protected OperationDBContext ctx;
        protected IApiProjectRepository Repository;

        public DatabaseTest()
        {
            var options = new DbContextOptionsBuilder<OperationDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            ctx = new OperationDBContext(options.Options);

            Repository = new ApiProjectRepository(ctx);
        }

        protected void CreateOperation(int productId, int sellerId, int? clientId, DateTime date, EOperationType type, decimal value = 0)
        {
            if (clientId.HasValue)
                CreateClient(clientId.Value);

            CreateProduct(productId);

            CreateSeller(sellerId);

            ctx.Operations.Add(new Operation
            {
                ClientId = clientId,
                ProductId = productId,
                SellerId = sellerId,
                OperationDate = date,
                OperationType = type,
                Value = value
            });

            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }

        protected void CreateClient(int clientId)
        {
            if (!ctx.Clients.Any(c => c.ClientId == clientId))
                ctx.Clients.Add(new Client { ClientId = clientId, Name = $"Client_{clientId}" });

            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }

        protected void CreateProduct(int productId)
        {
            if (!ctx.Products.Any(c => c.ProductId == productId))
                ctx.Products.Add(new Product { ProductId = productId, ProductName = $"Product_{productId}" });

            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }

        protected void CreateSeller(int sellerId)
        {
            if (!ctx.Sellers.Any(c => c.SellerId == sellerId))
                ctx.Sellers.Add(new Seller { SellerId = sellerId, SellerName = $"Seller_{sellerId}" });

            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }
    }
}
