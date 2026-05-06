using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Create_View_Select_Order_with_customer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var tsql = @"CREATE VIEW Select_Order_With_Customer
                        AS
                        Select C.EmailId,C.[Role], OrderId,O.OrderDate from CustomerInfo C
                        INNER JOIN 
                        [Order] O
                        ON
                        C.EmailId=O.EmailId";
            migrationBuilder.Sql(tsql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP View Select_Order_With_Customer");
        }
    }
}
