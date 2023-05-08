﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using repl.Models;

#nullable disable

namespace repl.Migrations
{
    [DbContext(typeof(NorthwindContext))]
    partial class NorthwindContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CustomerCustomerDemo", b =>
                {
                    b.Property<string>("CustomerId")
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)")
                        .HasColumnName("customer_id");

                    b.Property<string>("CustomerTypeId")
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)")
                        .HasColumnName("customer_type_id");

                    b.HasKey("CustomerId", "CustomerTypeId")
                        .HasName("pk_customer_customer_demo");

                    b.HasIndex("CustomerTypeId");

                    b.ToTable("customer_customer_demo", (string)null);
                });

            modelBuilder.Entity("EmployeeTerritory", b =>
                {
                    b.Property<short>("EmployeeId")
                        .HasColumnType("smallint")
                        .HasColumnName("employee_id");

                    b.Property<string>("TerritoryId")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("territory_id");

                    b.HasKey("EmployeeId", "TerritoryId")
                        .HasName("pk_employee_territories");

                    b.HasIndex("TerritoryId");

                    b.ToTable("employee_territories", (string)null);
                });

            modelBuilder.Entity("repl.Models.Category", b =>
                {
                    b.Property<short>("CategoryId")
                        .HasColumnType("smallint")
                        .HasColumnName("category_id");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("category_name");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<byte[]>("Picture")
                        .HasColumnType("bytea")
                        .HasColumnName("picture");

                    b.HasKey("CategoryId")
                        .HasName("pk_categories");

                    b.ToTable("categories", (string)null);
                });

            modelBuilder.Entity("repl.Models.Customer", b =>
                {
                    b.Property<string>("CustomerId")
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)")
                        .HasColumnName("customer_id");

                    b.Property<string>("Address")
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)")
                        .HasColumnName("address");

                    b.Property<string>("City")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("city");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("company_name");

                    b.Property<string>("ContactName")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("contact_name");

                    b.Property<string>("ContactTitle")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("contact_title");

                    b.Property<string>("Country")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("country");

                    b.Property<string>("Fax")
                        .HasMaxLength(24)
                        .HasColumnType("character varying(24)")
                        .HasColumnName("fax");

                    b.Property<string>("Phone")
                        .HasMaxLength(24)
                        .HasColumnType("character varying(24)")
                        .HasColumnName("phone");

                    b.Property<string>("PostalCode")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("postal_code");

                    b.Property<string>("Region")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("region");

                    b.HasKey("CustomerId")
                        .HasName("pk_customers");

                    b.ToTable("customers", (string)null);
                });

            modelBuilder.Entity("repl.Models.CustomerDemographic", b =>
                {
                    b.Property<string>("CustomerTypeId")
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)")
                        .HasColumnName("customer_type_id");

                    b.Property<string>("CustomerDesc")
                        .HasColumnType("text")
                        .HasColumnName("customer_desc");

                    b.HasKey("CustomerTypeId")
                        .HasName("pk_customer_demographics");

                    b.ToTable("customer_demographics", (string)null);
                });

            modelBuilder.Entity("repl.Models.Employee", b =>
                {
                    b.Property<short>("EmployeeId")
                        .HasColumnType("smallint")
                        .HasColumnName("employee_id");

                    b.Property<string>("Address")
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)")
                        .HasColumnName("address");

                    b.Property<DateOnly?>("BirthDate")
                        .HasColumnType("date")
                        .HasColumnName("birth_date");

                    b.Property<string>("City")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("city");

                    b.Property<string>("Country")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("country");

                    b.Property<string>("Extension")
                        .HasMaxLength(4)
                        .HasColumnType("character varying(4)")
                        .HasColumnName("extension");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("first_name");

                    b.Property<DateOnly?>("HireDate")
                        .HasColumnType("date")
                        .HasColumnName("hire_date");

                    b.Property<string>("HomePhone")
                        .HasMaxLength(24)
                        .HasColumnType("character varying(24)")
                        .HasColumnName("home_phone");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("last_name");

                    b.Property<string>("Notes")
                        .HasColumnType("text")
                        .HasColumnName("notes");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("bytea")
                        .HasColumnName("photo");

                    b.Property<string>("PhotoPath")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("photo_path");

                    b.Property<string>("PostalCode")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("postal_code");

                    b.Property<string>("Region")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("region");

                    b.Property<short?>("ReportsTo")
                        .HasColumnType("smallint")
                        .HasColumnName("reports_to");

                    b.Property<string>("Title")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("title");

                    b.Property<string>("TitleOfCourtesy")
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("title_of_courtesy");

                    b.HasKey("EmployeeId")
                        .HasName("pk_employees");

                    b.HasIndex("ReportsTo");

                    b.ToTable("employees", (string)null);
                });

            modelBuilder.Entity("repl.Models.Order", b =>
                {
                    b.Property<short>("OrderId")
                        .HasColumnType("smallint")
                        .HasColumnName("order_id");

                    b.Property<string>("CustomerId")
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)")
                        .HasColumnName("customer_id");

                    b.Property<short?>("EmployeeId")
                        .HasColumnType("smallint")
                        .HasColumnName("employee_id");

                    b.Property<float?>("Freight")
                        .HasColumnType("real")
                        .HasColumnName("freight");

                    b.Property<DateOnly?>("OrderDate")
                        .HasColumnType("date")
                        .HasColumnName("order_date");

                    b.Property<DateOnly?>("RequiredDate")
                        .HasColumnType("date")
                        .HasColumnName("required_date");

                    b.Property<string>("ShipAddress")
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)")
                        .HasColumnName("ship_address");

                    b.Property<string>("ShipCity")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("ship_city");

                    b.Property<string>("ShipCountry")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("ship_country");

                    b.Property<string>("ShipName")
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("ship_name");

                    b.Property<string>("ShipPostalCode")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("ship_postal_code");

                    b.Property<string>("ShipRegion")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("ship_region");

                    b.Property<short?>("ShipVia")
                        .HasColumnType("smallint")
                        .HasColumnName("ship_via");

                    b.Property<DateOnly?>("ShippedDate")
                        .HasColumnType("date")
                        .HasColumnName("shipped_date");

                    b.HasKey("OrderId")
                        .HasName("pk_orders");

                    b.HasIndex("CustomerId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ShipVia");

                    b.ToTable("orders", (string)null);
                });

            modelBuilder.Entity("repl.Models.OrderDetail", b =>
                {
                    b.Property<short>("OrderId")
                        .HasColumnType("smallint")
                        .HasColumnName("order_id");

                    b.Property<short>("ProductId")
                        .HasColumnType("smallint")
                        .HasColumnName("product_id");

                    b.Property<float>("Discount")
                        .HasColumnType("real")
                        .HasColumnName("discount");

                    b.Property<short>("Quantity")
                        .HasColumnType("smallint")
                        .HasColumnName("quantity");

                    b.Property<float>("UnitPrice")
                        .HasColumnType("real")
                        .HasColumnName("unit_price");

                    b.HasKey("OrderId", "ProductId")
                        .HasName("pk_order_details");

                    b.HasIndex("ProductId");

                    b.ToTable("order_details", (string)null);
                });

            modelBuilder.Entity("repl.Models.Product", b =>
                {
                    b.Property<short>("ProductId")
                        .HasColumnType("smallint")
                        .HasColumnName("product_id");

                    b.Property<short?>("CategoryId")
                        .HasColumnType("smallint")
                        .HasColumnName("category_id");

                    b.Property<int>("Discontinued")
                        .HasColumnType("integer")
                        .HasColumnName("discontinued");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("product_name");

                    b.Property<string>("QuantityPerUnit")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("quantity_per_unit");

                    b.Property<short?>("ReorderLevel")
                        .HasColumnType("smallint")
                        .HasColumnName("reorder_level");

                    b.Property<short?>("SupplierId")
                        .HasColumnType("smallint")
                        .HasColumnName("supplier_id");

                    b.Property<float?>("UnitPrice")
                        .HasColumnType("real")
                        .HasColumnName("unit_price");

                    b.Property<short?>("UnitsInStock")
                        .HasColumnType("smallint")
                        .HasColumnName("units_in_stock");

                    b.Property<short?>("UnitsOnOrder")
                        .HasColumnType("smallint")
                        .HasColumnName("units_on_order");

                    b.HasKey("ProductId")
                        .HasName("pk_products");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SupplierId");

                    b.ToTable("products", (string)null);
                });

            modelBuilder.Entity("repl.Models.Region", b =>
                {
                    b.Property<short>("RegionId")
                        .HasColumnType("smallint")
                        .HasColumnName("region_id");

                    b.Property<string>("RegionDescription")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)")
                        .HasColumnName("region_description");

                    b.HasKey("RegionId")
                        .HasName("pk_region");

                    b.ToTable("region", (string)null);
                });

            modelBuilder.Entity("repl.Models.Shipper", b =>
                {
                    b.Property<short>("ShipperId")
                        .HasColumnType("smallint")
                        .HasColumnName("shipper_id");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("company_name");

                    b.Property<string>("Phone")
                        .HasMaxLength(24)
                        .HasColumnType("character varying(24)")
                        .HasColumnName("phone");

                    b.HasKey("ShipperId")
                        .HasName("pk_shippers");

                    b.HasIndex("CompanyName")
                        .IsUnique();

                    b.ToTable("shippers", (string)null);
                });

            modelBuilder.Entity("repl.Models.Supplier", b =>
                {
                    b.Property<short>("SupplierId")
                        .HasColumnType("smallint")
                        .HasColumnName("supplier_id");

                    b.Property<string>("Address")
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)")
                        .HasColumnName("address");

                    b.Property<string>("City")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("city");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("company_name");

                    b.Property<string>("ContactName")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("contact_name");

                    b.Property<string>("ContactTitle")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("contact_title");

                    b.Property<string>("Country")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("country");

                    b.Property<string>("Fax")
                        .HasMaxLength(24)
                        .HasColumnType("character varying(24)")
                        .HasColumnName("fax");

                    b.Property<string>("Homepage")
                        .HasColumnType("text")
                        .HasColumnName("homepage");

                    b.Property<string>("Phone")
                        .HasMaxLength(24)
                        .HasColumnType("character varying(24)")
                        .HasColumnName("phone");

                    b.Property<string>("PostalCode")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("postal_code");

                    b.Property<string>("Region")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("region");

                    b.HasKey("SupplierId")
                        .HasName("pk_suppliers");

                    b.ToTable("suppliers", (string)null);
                });

            modelBuilder.Entity("repl.Models.Territory", b =>
                {
                    b.Property<string>("TerritoryId")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("territory_id");

                    b.Property<short>("RegionId")
                        .HasColumnType("smallint")
                        .HasColumnName("region_id");

                    b.Property<string>("TerritoryDescription")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)")
                        .HasColumnName("territory_description");

                    b.HasKey("TerritoryId")
                        .HasName("pk_territories");

                    b.HasIndex("RegionId");

                    b.ToTable("territories", (string)null);
                });

            modelBuilder.Entity("repl.Models.UsState", b =>
                {
                    b.Property<short>("StateId")
                        .HasColumnType("smallint")
                        .HasColumnName("state_id");

                    b.Property<string>("StateAbbr")
                        .HasMaxLength(2)
                        .HasColumnType("character varying(2)")
                        .HasColumnName("state_abbr");

                    b.Property<string>("StateName")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("state_name");

                    b.Property<string>("StateRegion")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("state_region");

                    b.HasKey("StateId")
                        .HasName("pk_usstates");

                    b.ToTable("us_states", (string)null);
                });

            modelBuilder.Entity("CustomerCustomerDemo", b =>
                {
                    b.HasOne("repl.Models.Customer", null)
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .IsRequired()
                        .HasConstraintName("fk_customer_customer_demo_customers");

                    b.HasOne("repl.Models.CustomerDemographic", null)
                        .WithMany()
                        .HasForeignKey("CustomerTypeId")
                        .IsRequired()
                        .HasConstraintName("fk_customer_customer_demo_customer_demographics");
                });

            modelBuilder.Entity("EmployeeTerritory", b =>
                {
                    b.HasOne("repl.Models.Employee", null)
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .IsRequired()
                        .HasConstraintName("fk_employee_territories_employees");

                    b.HasOne("repl.Models.Territory", null)
                        .WithMany()
                        .HasForeignKey("TerritoryId")
                        .IsRequired()
                        .HasConstraintName("fk_employee_territories_territories");
                });

            modelBuilder.Entity("repl.Models.Employee", b =>
                {
                    b.HasOne("repl.Models.Employee", "ReportsToNavigation")
                        .WithMany("InverseReportsToNavigation")
                        .HasForeignKey("ReportsTo")
                        .HasConstraintName("fk_employees_employees");

                    b.Navigation("ReportsToNavigation");
                });

            modelBuilder.Entity("repl.Models.Order", b =>
                {
                    b.HasOne("repl.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("fk_orders_customers");

                    b.HasOne("repl.Models.Employee", "Employee")
                        .WithMany("Orders")
                        .HasForeignKey("EmployeeId")
                        .HasConstraintName("fk_orders_employees");

                    b.HasOne("repl.Models.Shipper", "ShipViaNavigation")
                        .WithMany("Orders")
                        .HasForeignKey("ShipVia")
                        .HasConstraintName("fk_orders_shippers");

                    b.Navigation("Customer");

                    b.Navigation("Employee");

                    b.Navigation("ShipViaNavigation");
                });

            modelBuilder.Entity("repl.Models.OrderDetail", b =>
                {
                    b.HasOne("repl.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("fk_order_details_orders");

                    b.HasOne("repl.Models.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("fk_order_details_products");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("repl.Models.Product", b =>
                {
                    b.HasOne("repl.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("fk_products_categories");

                    b.HasOne("repl.Models.Supplier", "Supplier")
                        .WithMany("Products")
                        .HasForeignKey("SupplierId")
                        .HasConstraintName("fk_products_suppliers");

                    b.Navigation("Category");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("repl.Models.Territory", b =>
                {
                    b.HasOne("repl.Models.Region", "Region")
                        .WithMany("Territories")
                        .HasForeignKey("RegionId")
                        .IsRequired()
                        .HasConstraintName("fk_territories_region");

                    b.Navigation("Region");
                });

            modelBuilder.Entity("repl.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("repl.Models.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("repl.Models.Employee", b =>
                {
                    b.Navigation("InverseReportsToNavigation");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("repl.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("repl.Models.Product", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("repl.Models.Region", b =>
                {
                    b.Navigation("Territories");
                });

            modelBuilder.Entity("repl.Models.Shipper", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("repl.Models.Supplier", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
