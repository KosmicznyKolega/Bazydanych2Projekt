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
    else if (text=="show table" || text == "6")
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
    return "1";
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
    return "3";
}
string addSupplier()
{
    return "4";
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