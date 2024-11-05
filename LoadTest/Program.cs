// See https://aka.ms/new-console-template for more information
using NBomber.Contracts.Stats;
using NBomber.CSharp;
using System.Text;
using System;
using ApiProject.Entities.Models;
using ApiProject.Entities.Enumerators;
using System.Text.Json.Serialization;
using System.Text.Json;

Console.WriteLine("Hello, World!");


var t1 = Scenario.Create("teste", async ctx =>
{
    var ope = new OperationModel();
    ope.ProductId = 1;
    ope.SellerId = 1;
    ope.Value = 10;
    ope.OperationDate = DateTime.Now;
    ope.OperationType = EOperationType.Credit;

    var json = JsonSerializer.Serialize(ope);
    var data = new StringContent(json, Encoding.UTF8, "application/json");

    var url = "https://localhost:8081/v1/Operations";
    using var client = new HttpClient();

    var response = await client.PostAsync(url, data);

    return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();

})
.WithoutWarmUp()
.WithLoadSimulations(Simulation.Inject(500, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(30)));

NBomberRunner
    .RegisterScenarios(t1)
    .WithReportFileName("fetch_users_report")
    .WithReportFolder("fetch_users_reports")
    .WithReportFormats(ReportFormat.Html)
    .Run();


Console.ReadLine();