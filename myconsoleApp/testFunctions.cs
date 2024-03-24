using System.Drawing;
using Console = Colorful.Console;
using ConsoleTables;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Spectre.Console;
class testFunction
{

#region  test area - methods

    public static void helloWorld()
    {
        Console.WriteLine("hello World");
    }
    public static void printAnIntegerEnteredByUser()
    {
        int number;
        Console.Write("enter a number :");
        number = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("you entered : {0} ", number);

    }
    public static void addTwoInteger()
    {
        int num1, num2, res;

        Console.Write("input first number: ");
        num1 = Convert.ToInt32(Console.ReadLine());

        Console.Write("input second number: ");
        num2 = Convert.ToInt32(Console.ReadLine());

        res = num1 + num2;

        Console.WriteLine("addition of two numbers: " + res);
    }
    public static void multiplyTwoInteger()
    {
        float num1,num2,res;
        Console.Write("input first number : ");
        num1 = Convert.ToSingle(Console.ReadLine());

        Console.Write("input second number : ");
        num2 = Convert.ToSingle(Console.ReadLine());

        res = num1*num2;

        Console.WriteLine("multiplication of two float inputs are : " + res);
    }
    public static void multiplyTwoInteger1()
    {
        float num1,num2;

        num1 = 4.0f;
        num2 = 2.0f;

        Console.WriteLine("{0} * {1} = {2}" , num1 , num2 , num1*num2);
    }
    public static void mathPower()
    {
        int basenumber,powernumber;

        Console.Write("enter base number:");
        basenumber = Convert.ToInt32(Console.ReadLine());

        Console.Write("enter power number:");
        powernumber = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("result is " + Math.Pow(basenumber,powernumber));
    }
    public static void countnumberofwordsinsentence()
    {
        string sentence;
        Console.Write("enter sentence:");
        sentence = Console.ReadLine();
        string[] words = sentence.Split(' ');
        Console.WriteLine("number of words in this sentence " + words.Length);
    }
    public static void logoAndTables()
    {
        printlogo();
        Console.Title = "test";
        Console.WriteLine("welcome back");

        var table = new ConsoleTable("one","two","three");
        table.AddRow(1, 2, 3);
        table.AddRow("this line should be longer", "yes it is", "oh");

        table.Write();

        Console.ReadKey(true);
    }
    public static void printlogo()
    {
        string logo = @"                     _     _ 
                    | |   | |
 __      _____  _ __| | __| |
 \ \ /\ / / _ \| '__| |/ _` |
  \ V  V / (_) | |  | | (_| |
   \_/\_/ \___/|_|  |_|\__,_|
                             
                             ";
     Console.WriteLine(logo);
    }
    public static void divideByzeroexception()
    {
        int num1,num2,res;
        num1 = 2;
        num2 = 01;
        try
        {
            res = num1/num2;
            Console.WriteLine("result : " + res);
        }
        catch
        {
            Console.WriteLine("error has occured . num2 seems to be zero");
        }
    }
    public static void writeToFile()
    {
        jsontest jsontest = new jsontest{param1=25,param2=56};

        string serializeString = JsonConvert.SerializeObject(jsontest,Formatting.Indented);

        File.WriteAllText("myjson.json",serializeString);
    }
    public static void readFromFile()
    {
        string content = File.ReadAllText("myjson.json");
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        jsontest myjsontest = JsonConvert.DeserializeObject<jsontest>(content);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        Console.WriteLine(myjsontest.param1);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
    }
    public static void todo_LIST()
    {
        List<string> todo_list = new List<string>();

        //create
        todo_list.Add("task1");
        //read
        Console.WriteLine(todo_list[0].ToString());
        //update
        int i = todo_list.IndexOf("task1");
        todo_list.Insert(i,"changed");
        Console.WriteLine(todo_list[0].ToString());
        Console.WriteLine(todo_list.Count + " is the number of tasks in the list");
        //delete
        todo_list.RemoveAt(0);

        Console.WriteLine(todo_list.Count + " is the number of tasks in the list");
    }
    public static void chooseOneMethod()
    {
        List<string> methodNames = new List<string>();
        methodNames.Add("0>helloworld");
        methodNames.Add("1>printAnIntegerEnteredByUser");

        foreach(var i in methodNames)
        {
            Console.WriteLine(i);
        }
        Console.WriteLine("please choose an option to enter...");
        int j = Convert.ToInt32( Console.ReadLine() );
        switch(j)
        {
            case 0:
                Console.WriteLine("...ENTERING HELLO WORLD METHOD...");
                helloWorld();
                Console.WriteLine("...TERMINATING HELLO WORLD METHOD...");
                break;
            case 1:
                Console.WriteLine("...ENTERING PRINT AN INTEGER ENTERED BY USER...");
                printAnIntegerEnteredByUser();
                Console.WriteLine("...TERMINATING PRINT AN INTEGER ENTERED BY USER...");
                break;
            default:
                Console.WriteLine("choose a correct option");
                break;
        }
        Console.ReadKey(true);

    }
    public static void insertIntoDb(string title, string author, float price)
{
    MySqlConnection mysqlConnection;
    string connectionString = "Server=localhost;Database=librarysystemdb;Uid=root;Pwd=root;";

    try
    {
        mysqlConnection = new MySqlConnection(connectionString);
        mysqlConnection.Open();
        Console.WriteLine("Connection successful");

        // Use parameterized query to insert data
        string sql = @"INSERT INTO booktable (title, author, price) VALUES (@title, @author, @price)";
        MySqlCommand command = new MySqlCommand(sql, mysqlConnection);
        
        // Add parameters with values
        command.Parameters.AddWithValue("@title", title);
        command.Parameters.AddWithValue("@author", author);
        command.Parameters.AddWithValue("@price", price);

        int rowsAffected = command.ExecuteNonQuery();
        Console.WriteLine(title + " has been added to the library");
        
        mysqlConnection.Close(); // Close the connection when done
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

    public static void readFromDb()
{
    MySqlConnection mySqlConnection;
    string connectionString = "Server=localhost;Database=mydb;Uid=root;Pwd=root;";

    try
    {
        mySqlConnection = new MySqlConnection(connectionString);
        mySqlConnection.Open();

        string commandText = "SELECT name FROM mytable";
        MySqlCommand mySqlCommand = new MySqlCommand(commandText, mySqlConnection);

        // Execute the command and get a data reader
        MySqlDataReader reader = mySqlCommand.ExecuteReader();

        // Check if the reader has rows
        if (reader.HasRows)
        {
            Console.WriteLine("Data from mytable:");
            while (reader.Read())
            {
                // Access data using column name or index
                string name = reader.GetString("name"); // Assuming 'name' is a column in mytable
                Console.WriteLine(name);
            }
        }
        else
        {
            Console.WriteLine("No data found in mytable.");
        }

        reader.Close(); // Close the reader
        mySqlConnection.Close(); // Close the connection
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }

    Console.ReadLine(); // Keep console window open
}
    public static void updateDb()
    {
        MySqlConnection mySqlConnection;
        string connectionString = "Server=localhost;Database=mydb;Uid=root;Pwd=root;";

        try
        {
            mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();

            string command = "update mytable set name = 'batman' where id = 1;";
            MySqlCommand mySqlCommand = new MySqlCommand(command,mySqlConnection);
            mySqlCommand.ExecuteNonQuery();
            Console.WriteLine("command executed successfully");
            mySqlConnection.Close();
            Console.ReadLine();
        }
        catch
        {
            Console.WriteLine("there was error connecting to the mysql database");
            Console.ReadLine();
        }
    } 
    public static void deleteFromDb()
    {
        //create connection
        MySqlConnection mySqlConnection;
        string connectionString = "Server=localhost;Database=mydb;Uid=root;Pwd=root;";

        try
        {
            mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            Console.WriteLine("connection successful");
            string command = "delete from mytable where id = 2;";
            MySqlCommand mySqlCommand = new MySqlCommand(command,mySqlConnection);
            mySqlCommand.ExecuteNonQuery();
            Console.WriteLine("command executed successfully");
            mySqlConnection.Close();
            Console.ReadLine();
        }
        catch
        {
            Console.WriteLine("there was error connecting to the mysql database");
            Console.ReadLine();
        }
    }
    public static void librarySystem()
    {
        library library = new library();
        Console.WriteLine("---WELCOME TO THE CONSOLE LIBRARY---");
        Console.WriteLine("0>DepositToLibrary");
        int i = Convert.ToInt32(Console.ReadLine());
        switch(i)
        {
            case 0:
                deposit();
                break;
            default:
                Console.WriteLine("please enter a valid input");
                break;
        }
        Console.WriteLine("---THANKS FOR VISITING . COME AGAIN---");
        Console.ReadLine();
    }
    public static void deposit()
    {
        Console.WriteLine("enter title of the book : " );
        string title = Console.ReadLine().ToString();
        Console.ReadLine();

        Console.WriteLine("enter author of the book : " );
        string author = Console.ReadLine().ToString();
        Console.ReadLine();

        Console.WriteLine("enter price of the book : " );
        float price = Convert.ToInt32(Console.ReadLine());
        Console.ReadLine();

        book book = new book(title,author,price);
        library library = new library();
        library.add(book);

        Console.ReadLine();
    }
    public static void spectre()
    {
        AnsiConsole.Markup("[underline red]Hello[/] World!");
    }
#endregion
}

public class jsontest
{
    public int param1;
    public int param2;
}

class library
{       
    public void add(book book)
    {
        string author = book.author;
        string title = book.title;
        float price = book.price;
        testFunction.insertIntoDb(title,author,price);
    }   
    
}

class book
{
    public string title;
    public string author;
    public float price;

    public book(string Title,string Author,float Price)
    {
        title = Title;
        author = Author;
        price = Price;
    }
}