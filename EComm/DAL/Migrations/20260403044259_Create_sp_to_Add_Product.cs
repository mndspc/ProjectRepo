using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Create_sp_to_Add_Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var tsql = @"CREATE PROCEDURE usp_Add_Product @ProductId INT, @ProductName nvarchar(50), @Category nvarchar(50),@ListPrice float, @CreatedDate datetime2(7) AS Begin INSERT INTO Product VALUES(@ProductId,@ProductName,@Category,@ListPrice,@CreatedDate) End";
            migrationBuilder.Sql(tsql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE usp_Add_Product");
        }
    }
}
