using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bangazon
{
    class Program
    {
        static void Main(string[] args)
        {
            bool keepGoing = true;
            StringBuilder products = new StringBuilder();
            List<string> itemsToOrder = new List<string>();
            Dictionary<int, double> cartPrices = new Dictionary<int, double>();
            double cartTotal = 0.00;

            StringBuilder MainMenu = new StringBuilder();
            MainMenu.AppendLine("********************************************************");
            MainMenu.AppendLine("**Welcome to Bangazon Command Line Ordering System  **");
            MainMenu.AppendLine("********************************************************");
            MainMenu.AppendLine("1.Create an account");
            MainMenu.AppendLine("2.Create a payment option");
            MainMenu.AppendLine("3.Order a product");
            MainMenu.AppendLine("4.Complete an order");
            MainMenu.AppendLine("5.See porduct popularity");
            MainMenu.AppendLine("6.Exit Program");

            String MainMenuString = MainMenu.ToString();

            SqlConnection sqlConnection1 = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Caitlin\\Documents\\Invoices.mdf\"");


            while (keepGoing)
            {
                Console.WriteLine(MainMenuString);

                String userOption = Console.ReadLine();
                //Put validation on userOption Here!

                switch (userOption)
                {
                    case "1": //Create an account
                        Console.WriteLine("Enter customer first name");
                        string firstName = Console.ReadLine();

                        Console.WriteLine("Enter customer last name");
                        string lastName = Console.ReadLine();

                        Console.WriteLine("Enter street address");
                        string streetAddress = Console.ReadLine();

                        Console.WriteLine("Enter city");
                        string city = Console.ReadLine();

                         Console.WriteLine("Enter state");
                        string state = Console.ReadLine();

                        Console.WriteLine("Enter postal code");
                        string PostalCode = Console.ReadLine();

                        Console.WriteLine("Enter phone number");
                        string phoneNumber = Console.ReadLine();

                        

                        StringBuilder addCustomer = new StringBuilder();
                        addCustomer.Append("INSERT INTO Customer (FirstName, LastName, StreetAddress, City, State, PostalCode, PhoneNumber) VALUES (");

                        addCustomer.Append("'" + firstName + "', ");
                        addCustomer.Append("'" + lastName + "', ");
                        addCustomer.Append("'" + streetAddress + "', ");
                        addCustomer.Append("'" + city + "', ");
                        addCustomer.Append("'" + state + "', ");
                        addCustomer.Append("'" + PostalCode + "', ");
                        addCustomer.Append("'" + phoneNumber + "')");

                        string command = addCustomer.ToString();

              SqlCommand cmd = new SqlCommand();
              cmd.CommandType = System.Data.CommandType.Text;
              cmd.CommandText = command;
              cmd.Connection = sqlConnection1;
              sqlConnection1.Open();
              cmd.ExecuteNonQuery();
                        
                        Console.WriteLine("You've been added!");
                        continue;
                    case "2": //Create a payment option
                        Console.WriteLine("Which customer ?");

                        string customerQuery = "SELECT FirstName + ' ' + LastName as FullName from Customer";

                        using (SqlCommand cmd2 = new SqlCommand(customerQuery, sqlConnection1))
                        {
                            sqlConnection1.Open();
                            using (SqlDataReader reader = cmd2.ExecuteReader())
                            {
                                // Check if the reader has any rows at all before starting to read.
                                if (reader.HasRows)
                                {
                                    int count = 0;
                                    // Read advances to the next row.
                                    while (reader.Read())
                                    {
                                        count++;
                                        Console.WriteLine("{0}. {1}", count, reader[0]);
                                    }

                                }
                            }
                        }

                        
                        string customerNumber = Console.ReadLine();

                        Console.WriteLine("Enter payment type (e.g.AmEx, Visa, Checking)");
                        string paymentType = Console.ReadLine();

                        Console.WriteLine("Enter account number");
                        string accountNumber = Console.ReadLine();

                        StringBuilder addPaymentOption = new StringBuilder();
                        addPaymentOption.Append("INSERT INTO PaymentOptions (IdCustomer, Name, AccountNumber) VALUES (");

                        addPaymentOption.Append("'" + customerNumber + "', ");
                        addPaymentOption.Append("'" + paymentType + "', ");
                        addPaymentOption.Append("'" + accountNumber + "')");

                        string paymentCommand = addPaymentOption.ToString();

                        SqlCommand pymtCmd = new SqlCommand();
                        pymtCmd.CommandType = System.Data.CommandType.Text;
                        pymtCmd.CommandText = paymentCommand;
                        pymtCmd.Connection = sqlConnection1;
                        //sqlConnection1.Open(); <-- when to open and close connection????
                        pymtCmd.ExecuteNonQuery();

                        Console.WriteLine("Your payment option has been added");
                        continue;

                    case "3": //Order a Product
                        
                        bool addMoreToOrder = true;

                        //Clear out any previous orders
                        itemsToOrder.Clear();

                        string productQuery = "SELECT Name, Price FROM Product";
                        // Display products available to order
                        using (SqlCommand cmd3 = new SqlCommand(productQuery, sqlConnection1))
                        {
                            sqlConnection1.Open();
                            using (SqlDataReader reader = cmd3.ExecuteReader())
                            {

                                if (reader.HasRows)
                                {
                                    int count = 0;

                                    while (reader.Read())
                                    {
                                        count++;
                                        string menuLine = string.Format("{0}. {1}", count, reader[0]);
                                        double price = Convert.ToDouble(reader[1]);
                                        cartPrices.Add(count, price);
                                        
                                        products.AppendLine(menuLine);

                                    }
                                    while (addMoreToOrder)
                                    {
                                        //v2- When it isn't the first time through, say what ELSE do you want to order, and give a list of items already ordered
                                        string availableProducts = products.ToString();
                                        Console.WriteLine("What would you like to order?");
                                        Console.WriteLine(availableProducts);
                                        Console.WriteLine("{0}. Back to main menu- I've added all items I want", count + 1);
                                        string itemNumber = Console.ReadLine();
                                        int i = Int32.Parse(itemNumber);
                                        itemsToOrder.Add(itemNumber);
                                        if (i == count + 1)
                                        {
                                            addMoreToOrder = false;
                                        }
                                    }
                                }
                            }

                        }


                        //StringBuilder addOrderProduct = new StringBuilder();
                        //addOrderProduct.Append("INSERT INTO OrderProduct (IdProduct, IdOrder) VALUES (");

                        //addOrderProduct.Append("'" + firstName + "', ");
                        //addOrderProduct.Append("'" + lastName + "', ");
                        //addOrderProduct.Append("'" + streetAddress + "', ");
                        //addOrderProduct.Append("'" + city + "', ");
                        //addOrderProduct.Append("'" + state + "', ");
                        //addOrderProduct.Append("'" + PostalCode + "', ");
                        //addOrderProduct.Append("'" + phoneNumber + "')");

                        //string command = addOrderProduct.ToString();

                        //SqlCommand cmd = new SqlCommand();
                        //cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.CommandText = command;
                        //cmd.Connection = sqlConnection1;
                        //sqlConnection1.Open();
                        //cmd.ExecuteNonQuery();

                        Console.WriteLine("You've selected items to order!");

                        //
                        continue;
                    case "4": //Complete an order
                              //Check to see if items have been selected yet
                        if (itemsToOrder.Count == 0)
                            {
                            Console.WriteLine("Please add some products to your order first.Press any key to return to main menu.");
                            Console.ReadKey();
                            continue;
                            }

                        int numOfITemsToOrder = itemsToOrder.Count();

                        foreach (string item in itemsToOrder)
                        {
                            double itemPrice;
                            if(cartPrices.TryGetValue(int.Parse(item), out itemPrice))
                            {
                                cartTotal += itemPrice;
                            }
                        }

                        Console.WriteLine("Your order total is {0}. Ready to purchase?", cartTotal);
                        Console.WriteLine("Y/N");

                        string answer = Console.ReadLine();

                            // +# If user entered Y
                            // +Which customer is placing the order?
                            //+1.John Q.Public
                            //+ 2.Svetlana Z.Herevazena
                            //+Choose a payment option
                            //+ 1.Amex
                            //+ 2.Visa

                            //+Your order is complete!Press any key to return to main menu.

                            //+# If user entered N, display the main menu again

                        //How would you like your items shipped?

                        continue;
                    case "5": //See product popularity
                        continue;

                    case "6": //Exit
                        keepGoing = false;
                        continue;
                }
            }

            Console.Write("Hit any key to exit");
            Console.ReadKey();

        }
    }
}
