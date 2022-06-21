using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopMVC.DAL.Migrations
{
    public partial class seedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Product", new string[] { "Price", "Manufacturer", "Name", "Type", "Description" }, new string[] { "800", "Apple", "IPhone 13", "1", "A very nice phone from US" }, "dbo");
            migrationBuilder.InsertData("Product", new string[] { "Price", "Manufacturer", "Name", "Type", "Description" }, new string[] { "1000", "Samsung", "Samsung Galaxy", "1", "Have a nice camera" }, "dbo");
            migrationBuilder.InsertData("Product", new string[] { "Price", "Manufacturer", "Name", "Type", "Description" }, new string[] { "2200", "GT Racer", "X-2323", "3", "Comfortable armchair" }, "dbo");
            migrationBuilder.InsertData("Product", new string[] { "Price", "Manufacturer", "Name", "Type", "Description" }, new string[] { "2100", "2Е", "BUSHIDO", "3", "Goodlooking comfortable armchair" }, "dbo");
            migrationBuilder.InsertData("Product", new string[] { "Price", "Manufacturer", "Name", "Type", "Description" }, new string[] { "3050", "Artline", "Gaming X51", "2", "Top gaming PC" }, "dbo");
            migrationBuilder.InsertData("Product", new string[] { "Price", "Manufacturer", "Name", "Type", "Description" }, new string[] { "3200", "Asus", "S500MC", "2", "The topiest gaming PC!" }, "dbo");
            migrationBuilder.InsertData("Product", new string[] { "Price", "Manufacturer", "Name", "Type", "Description" }, new string[] { "300", "Logitech", "MX Master 3", "4", "Wireless gaming mouse" }, "dbo");
            migrationBuilder.InsertData("Product", new string[] { "Price", "Manufacturer", "Name", "Type", "Description" }, new string[] { "600", "Mad Catz", "R.A.T. PRO X3", "4", "Technologies in this mouse are insane" }, "dbo");
            migrationBuilder.InsertData("Product", new string[] { "Price", "Manufacturer", "Name", "Type", "Description" }, new string[] { "600", "Samsung", "Odyssey G5", "5", "31.5 inches with 2k QHD" }, "dbo");
            migrationBuilder.InsertData("Product", new string[] { "Price", "Manufacturer", "Name", "Type", "Description" }, new string[] { "720", "Dell", "S2722QC", "5", "27 inches with 4k UltraHD" }, "dbo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
