using ApiProject.Entities.Enumerators;
using ApiProject.Entities.Models;
using ApiProject.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;


namespace ApiProject.Test
{
    [TestClass]
    public class OperationsServiceTest : DatabaseTest
    {
        [Fact]
        public async Task Add_Operation_Client_NotFound()
        {
            CreateProduct(1);
            CreateSeller(1);

            var svc = new OperationService(Log.Object, Repository);

            var res = await svc.AddOperationAsync(new OperationModel
            {
                ClientId = 1,
                ProductId = 1,
                SellerId = 1,
                Value = 10,
                OperationType = EOperationType.Credit,
                OperationDate = DateTime.Now,
            });

            Log.Verify(_ => _.Error("Client with id 1 not found."));

            Assert.IsFalse(res);
        }

        [Fact]
        public async Task Add_Operation_Product_NotFound()
        {
            CreateSeller(1);

            var svc = new OperationService(Log.Object, Repository);

            var res = await svc.AddOperationAsync(new OperationModel
            {
                ProductId = 1,
                SellerId = 1,
                Value = 10,
                OperationType = EOperationType.Credit,
                OperationDate = DateTime.Now,
            });

            Log.Verify(_ => _.Error("Product with id 1 not found."));

            Assert.IsFalse(res);
        }

        [Fact]
        public async Task Add_Operation_Seller_NotFound()
        {
            CreateProduct(1);

            var svc = new OperationService(Log.Object, Repository);

            var res = await svc.AddOperationAsync(new OperationModel
            {
                ProductId = 1,
                SellerId = 1,
                Value = 10,
                OperationType = EOperationType.Credit,
                OperationDate = DateTime.Now,
            });

            Log.Verify(_ => _.Error("Seller with id 1 not found."));

            Assert.IsFalse(res);
        }

        [Fact]
        public async Task Add_Operation_Without_Client()
        {
            CreateProduct(1);
            CreateSeller(1);

            var svc = new OperationService(Log.Object, Repository);

            var res = await svc.AddOperationAsync(new OperationModel
            {
                ProductId = 1,
                SellerId = 1,
                Value = 10,
                OperationType = EOperationType.Credit,
                OperationDate = DateTime.Now,
            });

            Log.Verify(_ => _.Debug("Created"));

            Assert.IsTrue(res);
        }

        [Fact]
        public async Task Add_Operation_With_Client()
        {
            CreateClient(1);
            CreateProduct(1);
            CreateSeller(1);

            var svc = new OperationService(Log.Object, Repository);

            var res = await svc.AddOperationAsync(new OperationModel
            {
                ClientId = 1,
                ProductId = 1,
                SellerId = 1,
                Value = 10,
                OperationType = EOperationType.Credit,
                OperationDate = DateTime.Now,
            });

            Log.Verify(_ => _.Debug("Created"));

            Assert.IsTrue(res);
        }

        [Fact]
        public async Task Add_Operation_Exists()
        {
            var operationDate = DateTime.Now;

            CreateOperation(1, 1, 1, operationDate, EOperationType.Credit, 10);

            var svc = new OperationService(Log.Object, Repository);

            var res = await svc.AddOperationAsync(new OperationModel
            {
                ProductId = 1,
                SellerId = 1,
                ClientId = 1,
                Value = 10,
                OperationType = EOperationType.Credit,
                OperationDate = operationDate,
            });

            Log.Verify(_ => _.Error("Operation with same values already exists."));

            Assert.IsFalse(res);
        }

        [Fact]
        public async Task Add_Operation_Whitout_Client_Exists()
        {
            var operationDate = DateTime.Now;

            CreateOperation(1, 1, null, operationDate, EOperationType.Credit, 10);

            var svc = new OperationService(Log.Object, Repository);

            var res = await svc.AddOperationAsync(new OperationModel
            {
                ProductId = 1,
                SellerId = 1,
                Value = 10,
                OperationType = EOperationType.Credit,
                OperationDate = operationDate,
            });

            Log.Verify(_ => _.Debug("Created"));

            Assert.IsTrue(res);
        }

        [Fact]
        public async Task Remove_Operation_Not_Found()
        {
            var operationDate = DateTime.Now;

            var svc = new OperationService(Log.Object, Repository);

            var res = await svc.RemoveOperationAsync(new OperationModel
            {
                ProductId = 1,
                SellerId = 1,
                Value = 10,
                OperationType = EOperationType.Credit,
                OperationDate = operationDate,
            });

            Log.Verify(_ => _.Error("Operation doesn't not exists."));

            Assert.IsFalse(res);
        }

        [Fact]
        public async Task Remove_Operation_Exists()
        {
            var operationDate = DateTime.Now;

            CreateOperation(1, 1, null, operationDate, EOperationType.Credit, 10);

            var svc = new OperationService(Log.Object, Repository);

            var res = await svc.RemoveOperationAsync(new OperationModel
            {
                ProductId = 1,
                SellerId = 1,
                Value = 10,
                OperationType = EOperationType.Credit,
                OperationDate = operationDate,
            });

            Assert.IsTrue(res);
        }

        [Fact]
        public async Task Add_Operation_Without_Value()
        {
            CreateClient(1);
            CreateProduct(1);
            CreateSeller(1);

            var svc = new OperationService(Log.Object, Repository);

            var res = await svc.AddOperationAsync(new OperationModel
            {
                ClientId = 1,
                ProductId = 1,
                SellerId = 1,
                OperationType = EOperationType.Credit,
                OperationDate = DateTime.Now,
            });

            Log.Verify(_ => _.Error("Invalid Value 0."));

            Assert.IsFalse(res);
        }
    }
}