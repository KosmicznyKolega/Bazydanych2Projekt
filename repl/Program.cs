using Microsoft.EntityFrameworkCore;
using repl.Models;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Infrastructure;

void writeUI(string answer){
    Console.WriteLine(answer);
    Console.WriteLine(" - Northwind - \n" +
        "Choose a command:\n" +
        "-add order\n-delete order\n-modify order\n-add shipper\n-add product\n-add supplier\n-exit");
}
int parseCmd(string text){
    if (text == "add order" || text == "1")
    {
        return 1;
    }
    else if (text == "delete order" || text == "2")
    {
        return 2;
    }
    else if (text == "modify order" || text == "3")
    {
        return 3;
    }
    else if (text=="add shipper" || text == "4")
    {
        return 4;
    }
    else if (text == "add product" || text == "5")
    {
        return 5;
    }
    else if (text == "add supplier" || text == "6")
    {
        return 6;
    }
    else if (text == "exit" || text == "7")
    {
        return 7;
    }

    else return 0;
}

string addOrder()
{
    using var context = new NorthwindContext();
    string answer = "";
    string[] fieldLabels = { "First Name", "Last Name", "adress", "Region", "PostalCode", "Country", "city" };
    List<String> fields = new List<string>();

    for (int i = 0; i < fieldLabels.Length; i++)
    {
        Console.Clear();
        Console.WriteLine($"Please input {fieldLabels[i]}");
        string temp = Console.ReadLine();
        if (temp == "")
        {
            while (temp == "")
            {
                Console.WriteLine("Cannot be null");
                temp = Console.ReadLine();
            }
            fields.Add(temp);
        }
        else
        {
            fields.Add(temp);
        }
    }
    // ==================================
    string firstName = fields[0];
    string lastName = fields[1];
    string address = fields[2];
    string shipTo = firstName + " " +lastName;
    string country = fields[5];
    string region = fields[3];
    string postalCode = fields[4];
    string city = fields[6];
    string newId = firstName.Substring(0, 2) + lastName.Substring(0, 2);
    string id = newId.ToUpper();
    bool f = false;
    Customer customer = new Customer();
    foreach (Customer el in context.Customers)
    {
        if (id == el.CustomerId)
        {
            f = true;
            customer = el;
        }
    }
    if (!f)
    {
        customer = new Customer
        {
            CustomerId = id,
            CompanyName = firstName + " " + lastName,
            ContactName = firstName + " " + lastName,
            ContactTitle = null,
            Address = address,
            City = city,
            Region = region,
            PostalCode = postalCode,
            Country = country,
            Phone = null,
            Fax = null
        };
    }
    bool ifEnd = false;
    while (!ifEnd)
    {
        context.ChangeTracker.Clear();
        List<OrderDetail> orders = new List<OrderDetail>();
        try
        {
            bool ifContinue = true;
            while (ifContinue)
            {
                string[] fieldLabels2 = { "what product you want to buy (id)", "how much of it" };
                List<String> fields2 = new List<string>();

                Product newProd = new Product();
                for (int i = 0; i < fieldLabels2.Length; i++)
                {
                    Console.Clear();
                    Console.WriteLine($"Please input {fieldLabels2[i]}");
                    string temp = Console.ReadLine();
                    if (temp == "")
                    {
                        while (temp == "")
                        {
                            Console.WriteLine("Cannot be null");
                            temp = Console.ReadLine();
                        }
                        fields2.Add(temp);
                    }
                    else if (i == 0)
                    {
                        while (true)
                        {
                            newProd = context.Products.Where(x => x.ProductId == Convert.ToInt16(temp)).AsNoTracking().FirstOrDefault();
                            if (newProd != null)
                            {
                                fields2.Add(temp);
                                break;
                            }
                            Console.Clear();
                            Console.WriteLine("There's no such Item, choose a correct one");
                            temp = Console.ReadLine();
                            while (temp == "")
                            {
                                Console.Clear();
                                Console.WriteLine("Cannot be null");
                                temp = Console.ReadLine();
                            }
                        }
                    }
                    else
                    {
                        fields2.Add(temp);
                    }
                    if (i == 1 && newProd.UnitsInStock < Convert.ToInt16(fields2[1]))
                    {
                        while (newProd.UnitsInStock < Convert.ToInt16(fields2[1]))
                        {
                            Console.Clear();
                            Console.WriteLine($"There's not enough of this product please select less than {newProd.UnitsInStock} units or 0 to continue\n");
                            temp = Console.ReadLine();
                            fields2[1] = temp;
                        }
                    }

                }
                if (fields2[1] != "0")
                {
                    orders.Add(new OrderDetail {  ProductId = newProd.ProductId, Discount = 0, UnitPrice = (float)(newProd.UnitPrice * Convert.ToInt16(fields2[1])), Quantity = Convert.ToInt16(fields2[1]),OrderId = -1, });
                }
                Console.Clear();
                Console.WriteLine("add another product to order? y/n\n");
                ifContinue = Console.ReadLine() == "y";
            }
            short newestid = 0;
            foreach (Order or in context.Orders) { if (or.OrderId >= newestid) { newestid = (short)(or.OrderId + 1); } }
            double fr = 0;
            foreach (OrderDetail order in orders)
            {
                order.OrderId = newestid;
                foreach (Product product in context.Products) {
                    if (product.ProductId == order.ProductId)
                    {
                        product.UnitsInStock = (short?)(product.UnitsInStock - order.Quantity);
                        break;
                    }
                }
                context.OrderDetails.Add(order);
                fr += order.Quantity * 0.51;
            }
            var rnd = new Random(DateTime.Now.Millisecond);
            short ticks = (short)rnd.Next(1, 6);
            short shipp = (short)rnd.Next(1, 3);
            Order newOrder = new Order
            {
                OrderId = newestid,
                CustomerId = id,
                EmployeeId = ticks,
                OrderDate = DateOnly.FromDateTime(DateTime.Now),
                ShippedDate = null,
                ShipVia = shipp,
                Freight = (float)fr,
                ShipName = firstName + " " + lastName,
                ShipAddress = address,
                ShipCity = city,
                ShipRegion = region,
                ShipPostalCode = postalCode,
                ShipCountry = country
            };
            if (!f) {
                ifEnd = true;
                context.Customers.Add(customer);
                ifEnd = false;
            }
            context.Orders.Add(newOrder);
            context.SaveChanges();
            answer = "Order added!";
            ifEnd = true; break;
        }
        catch (DbUpdateConcurrencyException)
        {
            answer = "couldn't add order due to concurrency exception";
            ifEnd = true;
        }
        catch (DbUpdateException)
        {
            if (ifEnd)
            {
                answer = "couldn't add order due to unique constraints (customer added mid-order)";
            }
            else
            {
                Console.Clear();
                Console.WriteLine("sorry the products you were trying to purchase are no longer available try again? y/n\n");
                ifEnd = (Console.ReadLine() != "y");
                answer = "couldn't add order due to products >0 constraint";
            }
        }
    }
    return answer;
}
Product createProduct(short suppId, short prodId)
{
    using var context = new NorthwindContext();
    string[] fieldLabels = { "Product Name", "Supplier Id", "Category Id", "Quantity Per Unit", "Unit Price", "Units In Stock", "Reorder Level" };
    List<String> fields = new List<string>();

    for (int i = 0; i < fieldLabels.Length; i++)
    {
        if (i == 1 && suppId != 0) { fields.Add($"{suppId}");i++ ; }
        Console.Clear();
        Console.WriteLine($"Please input {fieldLabels[i]}");
        string temp = Console.ReadLine();
        if (temp == "" && i >= 4)
        {
            fields.Add("0");
        }
        else if (temp == "")
        {
            while (temp == "")
            {
                Console.WriteLine("Cannot be null");
                temp = Console.ReadLine();
            }
            fields.Add(temp);
        }
        else if (i == 2)
        {
            bool f = true;
            while (f)
            {
                foreach (Category category in context.Categories)
                {
                    if (category.CategoryId == Convert.ToInt16(temp)) { fields.Add(temp); f = false; break; };
                }
                if (!f) { break; }
                Console.Clear();
                Console.WriteLine("There's no such Category, choose a correct one");
                temp = Console.ReadLine();
                while (temp == "")
                {
                    Console.Clear();
                    Console.WriteLine("Cannot be null");
                    temp = Console.ReadLine();
                }
            }
        }
        else if (i == 1)
        {
            bool f = true;
            while (f)
            {
                foreach (Supplier supplier in context.Suppliers)
                {
                    if (supplier.SupplierId == Convert.ToInt16(temp)) { fields.Add(temp); f = false; break; };
                }
                if (!f) { break; }
                Console.Clear();
                Console.WriteLine("There are no such Suppliers, choose a correct one");
                temp = Console.ReadLine();
                while (temp == "")
                {
                    Console.Clear();
                    Console.WriteLine("Cannot be null");
                    temp = Console.ReadLine();
                }
            }
        }
        else
        {
            fields.Add(temp);
        }
    }
    string prodName = fields[0];
    short supplierId = Convert.ToInt16(fields[1]); 
    short CategoryId = Convert.ToInt16(fields[2]);
    string qPU = fields[3];
    float uP = Convert.ToSingle(fields[4]);
    short uOO = 0;
    short uIS = Convert.ToInt16(fields[5]);
    short rL = Convert.ToInt16(fields[6]);
    Product product = new Product
    {
        ProductId = prodId,
        ProductName = prodName,
        SupplierId = supplierId,
        CategoryId = CategoryId,
        QuantityPerUnit = qPU,
        UnitPrice = uP,
        UnitsInStock = uIS,
        Discontinued = 0,
        ReorderLevel = rL,
        UnitsOnOrder = uOO


    };
    return product;
}
string addShipper()
{
    string answer;
    string[] fieldLabels = { "Company Name", "phone (unneccessary)" };
    List<String> fields= new List<string>();
    for (int i =0; i < fieldLabels.Length; i++)
    {
        Console.Clear();
        Console.WriteLine($"Please input {fieldLabels[i]}");
        string temp=Console.ReadLine();
        if (temp == "" && i == 1)
        {
            fields.Add("--");
        }
        else if (temp == "")
        {
            while (temp == "")
            {
                Console.WriteLine("Cannot be null");
                temp=Console.ReadLine();
            }
            fields.Add(temp);
        }
        else
        {
            fields.Add(temp);
        }
    }
    using (var context = new NorthwindContext())
    {
        short newId = 0;
        foreach(var e in context.Shippers)
        {
            if (newId <= e.ShipperId)
            {
                newId = (short)(e.ShipperId + 1);
            }
        }
        string rx =@"^\d{1}-\d{3}-\d{3}-\d{4}$";
        string rx2 = @"^\(\d{3}\) \d{3}-\d{4}$";
        if (fields[1] == "--" || !(Regex.IsMatch(fields[1], rx) || Regex.IsMatch(fields[1], rx2)))
        {
            context.Shippers.Add(new Shipper
            {
                ShipperId = newId,
                CompanyName = fields[0]
            }) ;

        }
        else
        {
            context.Shippers.Add(new Shipper
            {
                ShipperId = newId,
                CompanyName = fields[0],
                Phone = fields[1]
            });
        }
        try
        {
            context.SaveChanges();
            answer = "shipper added successfully";
        }
        catch (DbUpdateConcurrencyException)
        {
            answer = "couldn't add shipper due to concurrency exception";
        }
        catch (DbUpdateException)
        {
            answer = "couldn't add shipper due to unique constraints";
        }
    }
    return answer;
}
string addProduct()
{
    using var context = new NorthwindContext();
    string answer = "";
    short newId = 0;
    foreach(Product p  in context.Products)
    {
        if (p.ProductId >= newId) { newId=(short)(p.ProductId+1); }
    }
    Product product = createProduct(0, newId);
    try
    {
        context.Products.Add(product);
        context.SaveChanges();
        answer = "Product added successfully";
    }
    catch(DbUpdateConcurrencyException)
    {
        answer ="couldn't be added due to concurency exception";
    }
    catch (DbUpdateException)
    {
        answer = "couldn't be added due to constraints";
    }
    return answer;
}
string addSupplier()
{
    using var context = new NorthwindContext();
    string answer = "";
    string[] fieldLabels = { "Company Name", "Contact Name(unneccessary)", "Contact Title(unneccessary)", "Address(unneccessary)", "City(unneccessary)", "Region(unneccessary)", "PostalCode(unneccessary)", "Country(unneccessary)", "Phone(unneccessary)", "Fax(unneccessary)", "Homepage(unneccessary)"};
    List<String> fields = new List<string>();

    for (int i = 0; i < fieldLabels.Length; i++)
    {
        Console.Clear();
        Console.WriteLine($"Please input {fieldLabels[i]}");
        string temp = Console.ReadLine();
        if (temp == "" && i >= 1)
        {
            fields.Add("--");
        }
        else if (temp == "")
        {
            while (temp == "")
            {
                Console.WriteLine("Cannot be null");
                temp = Console.ReadLine();
            }
            fields.Add(temp);
        }
        else
        {
            fields.Add(temp);
        }
    }


    string rx = @"^\d{1}-\d{3}-\d{3}-\d{4}$";
    string rx2 = @"^\(\d{3}\) \d{3}-\d{4}$";
    string companyName = fields[0];
    string contactName;
    string contactTitle;
    string address;
    string city;
    string country;
    string phone;
    string region;
    string postalCode;
    string fax;
    string homepage;
    if (fields[1] == "--") { contactName = null; }
    else { contactName = fields[1]; }
    if (fields[2] == "--") { contactTitle = null; }
    else { contactTitle = fields[2]; }
    if (fields[3] == "--") { address = null; }
    else { address = fields[3]; }
    if (fields[4] == "--") { city = null; }
    else { city = fields[4]; }
    if (fields[5] == "--") { region = null; }
    else { region = fields[5]; }
    if (fields[6] == "--") { postalCode = null; }
    else { postalCode = fields[6]; }
    if (fields[7] == "--") { country = null; }
    else { country = fields[7]; }
    if (fields[8] == "--" || !(Regex.IsMatch(rx, fields[8]) || Regex.IsMatch(rx2, fields[8]))) { phone = null; }
    else { phone = fields[8]; }
    if (fields[9] == "--") { fax = null; }
    else { fax = fields[9]; }
    if (fields[10] == "--") { homepage = null; }
    else { homepage = fields[10]; }
    short newId = 0;
    Supplier supplier = new Supplier();
    try
    {
        foreach (var e in context.Suppliers)
        {
            if (newId <= e.SupplierId)
            {
                newId = (short)(e.SupplierId + 1);
            }
        }
        supplier = new Supplier 
        {
            SupplierId = newId,
            CompanyName = companyName,
            ContactName = contactName,
            ContactTitle = contactTitle,
            Region = region,
            PostalCode = postalCode,
            Country = country,
            Phone = phone,
            Fax = fax,
            Address = address,
            Homepage = homepage,
            Products = new List<Product>()
        };
        context.Suppliers.Add(supplier);
        context.SaveChanges();
        answer = "supplier added successfully";
    }
    catch (DbUpdateConcurrencyException)
    {
        answer = "couldn't add supplier due to concurrency exception";
        return answer;
    }
    catch (DbUpdateException)
    {
        answer = "couldn't add supplier due to unique constraints";
        return answer;
    }
    if (answer == "supplier added successfully")
    {
        Console.Clear();
        Console.WriteLine(answer);
        Console.WriteLine("Add any products it supplies? y/n");
        string temp = Console.ReadLine();
        while (temp == "y")
        {
            Product prod = new Product();
            try
            {
                
                short newProdId = 0;
                foreach (var e in context.Products)
                {
                    if (newProdId <= e.ProductId)
                    {
                        newProdId = (short)(e.ProductId + 1);
                    }
                }
                prod = createProduct(newId,newProdId);
                supplier.Products.Add(prod);
                prod.Supplier = supplier;
                context.Products.Add(prod);
                context.SaveChanges();
                Console.Clear();
                Console.WriteLine($"Product added succesfully\nAdd another product? y/n");
                temp = Console.ReadLine();
            }
            catch (DbUpdateConcurrencyException)
            {
                context.Entry(prod).State = EntityState.Detached;
                Console.Clear();
                Console.WriteLine("Product couldn't be added due to concurrency exception \nadd another one instead? y/n");
                temp = Console.ReadLine();
                
            }
            catch (DbUpdateException)
            {
                context.Entry(prod).State = EntityState.Detached;
                Console.Clear();
                Console.WriteLine("Product couldn't be added due to update exception\nadd another one instead? y/n");
                temp = Console.ReadLine();
            }
        }
    }
    return answer;
}

string deleteOrder()
{
    using var context = new NorthwindContext();
    string answer = "";
    Console.Clear();
    Console.WriteLine("What order to delete?\n");
    string temp = Console.ReadLine();
    Order order = context.Orders.Where(x => x.OrderId == Convert.ToInt16(temp)).FirstOrDefault();
    if (order==null)
    {
        answer = "no such order exists";
    }
    else if (order.ShippedDate == null)
    {
        try
        {
            List<OrderDetail> orderDetail = context.OrderDetails.Where(x => x.OrderId == order.OrderId).ToList();
            foreach (OrderDetail orderDetails in orderDetail)
            {   
                Product prod = context.Products.Where(x => x.ProductId == orderDetails.ProductId).FirstOrDefault();
                prod.UnitsInStock = (short ?)(prod.UnitsInStock + orderDetails.Quantity); 
                context.Remove(orderDetails);
            }
            context.Remove(order);
            context.SaveChanges();
            answer = "order deleted succesfully";
        }
        catch(DbUpdateConcurrencyException)
        {
            answer = "couldn't delete order due to concurrency exception";
        }
        catch (DbUpdateException) 
        {
            answer = "couldn't delete order due to constraint exception (shouldn't be possible?)";
        }
    }
    else
    {
        answer = "the order was already sent so it can't be deleted";
    }
    return answer;
}
string modifyOrder()
{
    using NorthwindContext context = new NorthwindContext();
    string answer = "";
    Console.Clear();
    Console.WriteLine("What order to modify?\n");
    string temp = Console.ReadLine();
    Order order = context.Orders.Where(x => x.OrderId == Convert.ToInt16(temp)).FirstOrDefault();
    if (order != null)
    {
        Console.Clear();
        Console.WriteLine("what to modify?\n" +
            "0 - employee id\n" +
            "1 - required date\n" +
            "2 - shipped date \n" +
            "3 - ship via \n" +
            "4 - freight \n" +
            "5 - ship name\n" +
            "6 - ship address\n" +
            "7 - ship city\n" +
            "8 - ship region\n" +
            "9 - postal code \n" +
            "10 - country\n" +
            "11 - quantity of products on order");
        temp = Console.ReadLine();
        try
        {
            switch (temp){
                case "0":
                    Console.Clear();
                    Console.WriteLine("enter new employee id\n");
                    order.EmployeeId = Convert.ToInt16(Console.ReadLine());
                    context.SaveChanges();
                    answer = "employee id changed";
                    break;

                case "1":
                    Console.Clear();
                    Console.WriteLine("enter new required date\n");
                    order.RequiredDate = DateOnly.Parse(Console.ReadLine());
                    context.SaveChanges();
                    answer = "required date changed";
                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine("enter new shipped date\n");
                    order.ShippedDate = DateOnly.Parse(Console.ReadLine());
                    context.SaveChanges();
                    answer = "shipped date changed";
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("enter new ship via\n");
                    order.ShipVia = Convert.ToInt16(Console.ReadLine());
                    context.SaveChanges();
                    answer = "ship via changed";
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("enter new freight\n");
                    order.Freight= (float?)Convert.ToDouble(Console.ReadLine());
                    context.SaveChanges();
                    answer = "freight changed";
                    break;
                case "5":
                    Console.Clear();
                    Console.WriteLine("enter new ship name\n");
                    order.ShipName= Console.ReadLine();
                    context.SaveChanges();
                    answer = "ship name changed";
                    break;
                case "6":
                    Console.Clear();
                    Console.WriteLine("enter new ship address\n");
                    order.ShipAddress = Console.ReadLine();
                    context.SaveChanges();
                    answer = "ship address changed";
                    break;
                case "7":
                    Console.Clear();
                    Console.WriteLine("enter new ship city\n");
                    order.ShipCity = Console.ReadLine();
                    context.SaveChanges();
                    answer = "ship city changed";
                    break;
                case "8":
                    Console.Clear();
                    Console.WriteLine("enter new ship region\n");
                    order.ShipRegion = Console.ReadLine();
                    context.SaveChanges();
                    answer = "ship region changed";
                    break;
                case "9":
                    Console.Clear();
                    Console.WriteLine("enter new postal code\n");
                    order.ShipPostalCode = Console.ReadLine();
                    context.SaveChanges();
                    answer = "postal code changed";
                    break;
                case "10":
                    Console.Clear();
                    Console.WriteLine("enter new ship country\n");
                    order.ShipCountry = Console.ReadLine();
                    context.SaveChanges();
                    answer = "ship country changed";
                    break;
                case "11":
                    temp = "";
                    while (temp.Length == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("which product?\n");
                        temp = Console.ReadLine();
                    }
                    OrderDetail orderDetail1 = context.OrderDetails.Where(x => x.OrderId == order.OrderId && x.ProductId == Convert.ToInt16(temp)).FirstOrDefault();
                    if (orderDetail1 != null)
                    {
                        temp = "";
                        foreach (Product product in context.Products)
                        {
                            if (product.ProductId == orderDetail1.ProductId)
                            {
                                Console.Clear();
                                Console.WriteLine("what should the quantity be?\n");
                                temp = Console.ReadLine();
                                product.UnitsInStock = (short?)(product.UnitsInStock + orderDetail1.Quantity - Convert.ToInt16(temp));
                                orderDetail1.Quantity = Convert.ToInt16(temp);
                                orderDetail1.UnitPrice = (float)(product.UnitPrice * orderDetail1.Quantity);
                                answer = "order modified succesfully";
                            }
                        }
                        context.SaveChanges();

                    }
                    else
                    {
                        answer = "Coulnd't find your product in order";
                    }
                    break;
                default:
                    break;

            }
        }
        catch (DbUpdateConcurrencyException)
        {
            answer = "couldn't modify order due to concurrency exception";
        }
        catch (DbUpdateException)
        {
            answer = "couldn't modify order due to constraints exception";
        }
    }
    else
    {
        answer = "can't modify an order that doesn't exist";
    }
    return answer;
}
//=================================================
//main start
writeUI(""); 
while (true)
{
    string text = Console.ReadLine();   
    // whether operation succeded or not and why
    string answer="";
    switch (parseCmd(text))
    {
        case 0:
            continue;
        case 1:
            answer=addOrder();
            break;
        case 2:
            answer = deleteOrder();
            break;
        case 3:
            answer = modifyOrder();
            break;
        case 4:
            answer=addShipper();
            break;
        case 5:
            answer=addProduct();
            break;
        case 6:
            answer=addSupplier();
            break;
        case 7:
            Console.Clear();
            Console.WriteLine("Exiting...");
            return 0;
    }
    Console.Clear();
    writeUI(answer);
}