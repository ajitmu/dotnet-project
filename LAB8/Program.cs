using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace InventoryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            InventoryManager manager = new InventoryManager();
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== INVENTORY MANAGEMENT SYSTEM (ADO.NET) ===");
                Console.WriteLine("1. Add New Item (CREATE)");
                Console.WriteLine("2. View All Items (READ)");
                Console.WriteLine("3. Update Item (UPDATE)");
                Console.WriteLine("4. Delete Item (DELETE)");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice (1-5): ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter Item Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter Category: ");
                        string category = Console.ReadLine();
                        Console.Write("Enter Quantity: ");
                        int qty = int.Parse(Console.ReadLine());
                        Console.Write("Enter Price: ");
                        decimal price = decimal.Parse(Console.ReadLine());

                        manager.AddItem(name, category, qty, price);
                        break;

                    case "2":
                        manager.GetAllItems();
                        break;

                    case "3":
                        manager.GetAllItems();
                        Console.Write("\nEnter Item ID to Update: ");
                        int updateId = int.Parse(Console.ReadLine());
                        Console.Write("Enter New Quantity: ");
                        int newQty = int.Parse(Console.ReadLine());
                        Console.Write("Enter New Price: ");
                        decimal newPrice = decimal.Parse(Console.ReadLine());

                        manager.UpdateItem(updateId, newQty, newPrice);
                        break;

                    case "4":
                        manager.GetAllItems();
                        Console.Write("\nEnter Item ID to Delete: ");
                        int deleteId = int.Parse(Console.ReadLine());
                        manager.DeleteItem(deleteId);
                        break;

                    case "5":
                        exit = true;
                        Console.WriteLine("Exiting application. Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice! Please try again.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }
    }

    public class InventoryManager
    {
        // Agar SQL Server ka naam alag ho toh yahan change karein (jaise server=YourServerName)
        private readonly string connectionString = "Server=localhost\\SQLEXPRESS;Database=InventoryDB;Trusted_Connection=True;TrustServerCertificate=True;";

        // 1. CREATE: Add item
        public void AddItem(string itemName, string category, int quantity, decimal price)
        {
            string query = "INSERT INTO InventoryItems (ItemName, Category, Quantity, Price, LastUpdated) " +
                           "VALUES (@ItemName, @Category, @Quantity, @Price, GETDATE())";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ItemName", itemName);
                    cmd.Parameters.AddWithValue("@Category", category);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@Price", price);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        Console.WriteLine($"\nSuccess: {rowsAffected} item added successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nError: {ex.Message}");
                    }
                }
            }
        }

        // 2. READ: Get all items
        public void GetAllItems()
        {
            string query = "SELECT ItemID, ItemName, Category, Quantity, Price, LastUpdated FROM InventoryItems";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Console.WriteLine("\n--- INVENTORY LIST ---");
                            Console.WriteLine($"{"ID",-5} | {"Name",-15} | {"Category",-12} | {"Qty",-5} | {"Price",-10} | {"Last Updated",-20}");
                            Console.WriteLine(new string('-', 75));

                            bool hasRows = false;
                            while (reader.Read())
                            {
                                hasRows = true;
                                Console.WriteLine($"{reader["ItemID"],-5} | " +
                                                  $"{reader["ItemName"],-15} | " +
                                                  $"{reader["Category"],-12} | " +
                                                  $"{reader["Quantity"],-5} | " +
                                                  $"{reader["Price"],-10:C} | " +
                                                  $"{reader["LastUpdated"],-20}");
                            }

                            if (!hasRows)
                            {
                                Console.WriteLine("No items found in inventory.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nError: {ex.Message}");
                    }
                }
            }
        }

        // 3. UPDATE: Modify item
        public void UpdateItem(int itemId, int newQuantity, decimal newPrice)
        {
            string query = "UPDATE InventoryItems SET Quantity = @Quantity, Price = @Price, LastUpdated = GETDATE() WHERE ItemID = @ItemID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ItemID", itemId);
                    cmd.Parameters.AddWithValue("@Quantity", newQuantity);
                    cmd.Parameters.AddWithValue("@Price", newPrice);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            Console.WriteLine($"\nSuccess: Item ID {itemId} updated successfully.");
                        else
                            Console.WriteLine($"\nError: Item ID {itemId} not found.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nError: {ex.Message}");
                    }
                }
            }
        }

        // 4. DELETE: Remove item
        public void DeleteItem(int itemId)
        {
            string query = "DELETE FROM InventoryItems WHERE ItemID = @ItemID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ItemID", itemId);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            Console.WriteLine($"\nSuccess: Item ID {itemId} deleted successfully.");
                        else
                            Console.WriteLine($"\nError: Item ID {itemId} not found.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nError: {ex.Message}");
                    }
                }
            }
        }
    }
}