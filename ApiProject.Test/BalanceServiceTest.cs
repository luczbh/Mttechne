using ApiProject.Entities.Enumerators;
using ApiProject.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;


namespace ApiProject.Test
{
    [TestClass]
    public class BalanceServiceTest : DatabaseTest
    {
        private readonly DateTime dataRef;
        public BalanceServiceTest()
        {
            dataRef = DateTime.Now;

            for (int i = 0; i < 1000; i++)
            {
                CreateOperation(1, 1, 1, dataRef, EOperationType.Credit, 1000);
                CreateOperation(2, 1, 1, dataRef, EOperationType.Credit, 1000);
                CreateOperation(3, 1, 1, dataRef, EOperationType.Credit, 1000);
                CreateOperation(3, 1, 1, dataRef, EOperationType.Debit, 50);
                CreateOperation(4, 2, null, dataRef, EOperationType.Credit, 1000);
            }
        }

        [Fact]
        public async Task Consolidate_Client()
        {
            var svc = new BalanceService(Log.Object, Repository);

            await svc.ConsolidateAsync();

            var res = await svc.GetBalanceByClients(dataRef);

            Assert.IsTrue(res.TotalCredits == 3000000);
            Assert.IsTrue(res.TotalDebits == 50000);
            Assert.IsTrue(res.Operations.Count() == 1);
        }

        [Fact]
        public async Task Consolidate_Product()
        {
            var svc = new BalanceService(Log.Object, Repository);

            await svc.ConsolidateAsync();

            var res = await svc.GetBalanceByProducts(dataRef);

            Assert.IsTrue(res.TotalCredits == 4000000);
            Assert.IsTrue(res.TotalDebits == 50000);
            Assert.IsTrue(res.Operations.Count() == 4);
        }

        [Fact]
        public async Task Consolidate_Seller()
        {
            var svc = new BalanceService(Log.Object, Repository);

            await svc.ConsolidateAsync();

            var res = await svc.GetBalanceBySellers(dataRef);

            Assert.IsTrue(res.TotalCredits == 4000000);
            Assert.IsTrue(res.TotalDebits == 50000);
            Assert.IsTrue(res.Operations.Count() == 2);
        }
    }
}