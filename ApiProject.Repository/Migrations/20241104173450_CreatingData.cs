using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiProject.Repository.Migrations
{
    /// <inheritdoc />
    public partial class CreatingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT Clients ON");
            migrationBuilder.Sql(@"WITH 
                                    t0(i) AS (SELECT 0 UNION ALL SELECT 0), 
                                    t1(i) AS (SELECT 0 FROM t0 a, t0 b),    
                                    t2(i) AS (SELECT 0 FROM t1 a, t1 b),    
                                    t3(i) AS (SELECT 0 FROM t2 a, t2 b),    
                                    t4(i) AS (SELECT 0 FROM t3 a, t3 b),  
                                    n(i) AS (SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 0)) FROM t4)

                                    INSERT INTO Clients (ClientId, Name)
                                    SELECT i AS ClientId, concat('Client_', i) AS Name FROM n WHERE i BETWEEN 1 AND 1000");
            migrationBuilder.Sql("SET IDENTITY_INSERT Clients OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT Products ON");
            migrationBuilder.Sql("INSERT INTO Products (ProductId, ProductName) SELECT ClientId, concat('Product_', ClientId) FROM Clients");
            migrationBuilder.Sql("SET IDENTITY_INSERT Products OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT Sellers ON");
            migrationBuilder.Sql("INSERT INTO Sellers (SellerId, SellerName) SELECT ClientId, concat('Seller_', ClientId) FROM Clients");
            migrationBuilder.Sql("SET IDENTITY_INSERT Sellers OFF");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE Clients");
            migrationBuilder.Sql("TRUNCATE TABLE Products");
            migrationBuilder.Sql("TRUNCATE TABLE Sellers");
        }
    }
}

