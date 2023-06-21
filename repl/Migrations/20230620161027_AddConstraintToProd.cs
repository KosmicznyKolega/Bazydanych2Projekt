using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace repl.Migrations
{
    /// <inheritdoc />
    public partial class AddConstraintToProd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE products ADD CONSTRAINT CK_units_in_stock CHECK (units_in_stock >= 0)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
