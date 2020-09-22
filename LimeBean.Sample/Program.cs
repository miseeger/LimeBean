using System;
using System.Data.SQLite;

namespace LimeBean.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Make an ADO.Net connection and create a BeanAPI from it.
            var connection = new SQLiteConnection("data source=:memory:");
            connection.Open();
            var api = new BeanApi(connection);
            
            // Only for demo or rapid prototyping - not recommended for production !!!
            api.EnterFluidMode();

            // Add a new row to the database
            var newRow = api.Dispense("books");
            newRow
                .Put("title", "Cloud Atlas")
                .Put("author", "David Mitchell");
            var newBookId = api.Store(newRow);
            Console.WriteLine("New book ID: " + newBookId.ToString());

            // Get the new row
            var row = api.Load("books", newBookId);
            var bookTitle = row.Get<string>("title");
            var bookAuthor = row.Get<string>("author");
            Console.WriteLine($"{bookTitle} by {bookAuthor}");

            Console.ReadKey();
        }
    }
}
