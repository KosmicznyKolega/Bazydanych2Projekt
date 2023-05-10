using Microsoft.EntityFrameworkCore;
using repl.Models;
using System.Text.RegularExpressions;

void writeUI(string answer){
    Console.WriteLine(answer);
    Console.WriteLine(" - Northwind - \n" +
        "Choose a command:\n" +
        "-add order\n-add shipper\n-add product\n-add supplier\n-add employee\n-exit");
}
int parseCmd(string text){
    if (text == "add order" || text == "1")
    {
        return 1;
    }
    else if (text=="add shipper" || text == "2")
    {
        return 2;
    }
    else if (text == "add product" || text == "3")
    {
        return 3;
    }
    else if (text == "add supplier" || text == "4")
    {
        return 4;
    }
    else if (text == "add employee" || text == "5")
    {
        return 5;
    }
    else if (text == "exit" || text == "7")
    {
        return 6;
    }

    else return 0;
}

string addOrder()
{
    return "1";
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
string addEmployee()
{
    return "5";
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
            answer=addShipper();
            break;
        case 3:
            answer=addProduct();
            break;
        case 4:
            answer=addSupplier();
            break;
        case 5:
            answer=addEmployee();
            break;
        case 6:
            Console.Clear();
            Console.WriteLine("Exiting...");
            return 0;
    }
    Console.Clear();
    writeUI(answer);
}