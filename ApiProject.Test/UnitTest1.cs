using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBomber.Contracts.Stats;
using NBomber.CSharp;

namespace ApiProject.Test
{
    [TestClass]
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var t1 = Scenario.Create("teste", async ctx =>
            {
                return Response.Ok();

            })
            .WithoutWarmUp()
            .WithLoadSimulations(Simulation.Inject(10, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10)));

            NBomberRunner
                .RegisterScenarios(t1)
                .WithReportFileName("fetch_users_report")
                .WithReportFolder("fetch_users_reports")
                .WithReportFormats(ReportFormat.Html)
                .Run();
        }
    }
}