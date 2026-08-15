using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ExpenseTrackingSystem
{
    class Expense
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string PaymentMethod { get; set; }
        public string Description { get; set; }

        public Expense(int id, string category, decimal amount,
                       DateTime expenseDate, string paymentMethod,
                       string description)
        {
            Id = id;
            Category = category;
            Amount = amount;
            ExpenseDate = expenseDate;
            PaymentMethod = paymentMethod;
            Description = description;
        }
    }

    class ExpenseTracker
    {
        private readonly List<Expense> expenses = new List<Expense>();
        private int nextId = 1;

    
        public void AddExpense()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("========== ADD EXPENSE ==========\n");

                Console.Write("Enter Category: ");
                string category = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(category))
                    throw new ArgumentException("Category cannot be empty.");

                Console.Write("Enter Amount: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
                    throw new FormatException("Please enter a valid amount.");

                if (amount <= 0)
                    throw new ArgumentException(
                        "Amount must be greater than 0.");

                Console.Write("Enter Expense Date (dd-MM-yyyy): ");
                string dateInput = Console.ReadLine();

                if (!DateTime.TryParseExact(
                        dateInput,
                        "dd-MM-yyyy",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out DateTime expenseDate))
                {
                    throw new FormatException(
                        "Invalid date. Use format dd-MM-yyyy.");
                }

                // Future date validation
                if (expenseDate.Date > DateTime.Today)
                {
                    throw new ArgumentException(
                        "Future dates are not allowed.");
                }

                Console.WriteLine("\nPayment Methods:");
                Console.WriteLine("1. Cash");
                Console.WriteLine("2. UPI");
                Console.WriteLine("3. Credit Card");
                Console.WriteLine("4. Debit Card");
                Console.WriteLine("5. Bank Transfer");

                Console.Write("Select Payment Method: ");

                string paymentMethod;

                switch (Console.ReadLine())
                {
                    case "1":
                        paymentMethod = "Cash";
                        break;

                    case "2":
                        paymentMethod = "UPI";
                        break;

                    case "3":
                        paymentMethod = "Credit Card";
                        break;

                    case "4":
                        paymentMethod = "Debit Card";
                        break;

                    case "5":
                        paymentMethod = "Bank Transfer";
                        break;

                    default:
                        throw new ArgumentException(
                            "Invalid payment method.");
                }

                Console.Write("Enter Description: ");
                string description = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(description))
                    description = "No description";

                Expense expense = new Expense(
                    nextId++,
                    category,
                    amount,
                    expenseDate,
                    paymentMethod,
                    description
                );

                expenses.Add(expense);

                Console.WriteLine("\nExpense added successfully!");
            }
            catch (FormatException ex)
            {
                Console.WriteLine("\nInput Error: " + ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("\nValidation Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nUnexpected Error: " + ex.Message);
            }

            Pause();
        }

      
        public void ViewExpenses()
        {
            Console.Clear();
            Console.WriteLine("========== ALL EXPENSES ==========\n");

            if (expenses.Count == 0)
            {
                Console.WriteLine("No expenses available.");
                Pause();
                return;
            }

            Console.WriteLine(
                "ID   Date         Category       Amount       Payment       Description");

            Console.WriteLine(
                "--------------------------------------------------------------------------");

            foreach (var expense in expenses.OrderByDescending(e => e.ExpenseDate))
            {
                Console.WriteLine(
                    $"{expense.Id,-4}" +
                    $"{expense.ExpenseDate:dd-MM-yyyy}   " +
                    $"{expense.Category,-15}" +
                    $"{expense.Amount,-10:N2}" +
                    $"{expense.PaymentMethod,-14}" +
                    $"{expense.Description}");
            }

            Console.WriteLine(
                "\nTotal: " + expenses.Sum(e => e.Amount).ToString("N2"));

            Pause();
        }
        public void UpdateExpense()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("========== UPDATE EXPENSE ==========\n");

                Console.Write("Enter Expense ID: ");

                if (!int.TryParse(Console.ReadLine(), out int id))
                    throw new FormatException("Invalid Expense ID.");

                Expense expense = expenses.FirstOrDefault(e => e.Id == id);

                if (expense == null)
                    throw new KeyNotFoundException(
                        "Expense with this ID was not found.");

                Console.WriteLine("\nCurrent Details:");
                Console.WriteLine($"Category: {expense.Category}");
                Console.WriteLine($"Amount: {expense.Amount:N2}");
                Console.WriteLine(
                    $"Date: {expense.ExpenseDate:dd-MM-yyyy}");
                Console.WriteLine(
                    $"Payment: {expense.PaymentMethod}");
                Console.WriteLine(
                    $"Description: {expense.Description}");

                Console.WriteLine("\nEnter New Details:");

                Console.Write("New Category: ");
                string category = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(category))
                    expense.Category = category;

                Console.Write("New Amount: ");
                string amountInput = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(amountInput))
                {
                    if (!decimal.TryParse(amountInput, out decimal amount))
                        throw new FormatException("Invalid amount.");

                    if (amount <= 0)
                        throw new ArgumentException(
                            "Amount must be greater than 0.");

                    expense.Amount = amount;
                }

                Console.Write("New Date (dd-MM-yyyy): ");
                string dateInput = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(dateInput))
                {
                    if (!DateTime.TryParseExact(
                        dateInput,
                        "dd-MM-yyyy",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out DateTime date))
                    {
                        throw new FormatException("Invalid date.");
                    }

                    if (date.Date > DateTime.Today)
                        throw new ArgumentException(
                            "Future dates are not allowed.");

                    expense.ExpenseDate = date;
                }

                Console.Write("New Description: ");
                string description = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(description))
                    expense.Description = description;

                Console.WriteLine(
                    "\nExpense updated successfully!");
            }
            catch (FormatException ex)
            {
                Console.WriteLine("\nInput Error: " + ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("\nValidation Error: " + ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine("\nError: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nUnexpected Error: " + ex.Message);
            }

            Pause();
        }

        public void DeleteExpense()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("========== DELETE EXPENSE ==========\n");

                Console.Write("Enter Expense ID: ");

                if (!int.TryParse(Console.ReadLine(), out int id))
                    throw new FormatException("Invalid Expense ID.");

                Expense expense = expenses.FirstOrDefault(e => e.Id == id);

                if (expense == null)
                    throw new KeyNotFoundException(
                        "Expense not found.");

                Console.WriteLine(
                    $"\nExpense: {expense.Category} - {expense.Amount:N2}");

                Console.Write("Are you sure you want to delete? (Y/N): ");

                string confirmation = Console.ReadLine();

                if (confirmation?.ToUpper() == "Y")
                {
                    expenses.Remove(expense);
                    Console.WriteLine("\nExpense deleted successfully!");
                }
                else
                {
                    Console.WriteLine("\nDelete operation cancelled.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError: " + ex.Message);
            }

            Pause();
        }

        public void SearchByCategory()
        {
            Console.Clear();
            Console.WriteLine("========== SEARCH EXPENSE ==========\n");

            Console.Write("Enter Category: ");
            string category = Console.ReadLine();

            var results = expenses
                .Where(e => e.Category.Equals(
                    category,
                    StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (results.Count == 0)
            {
                Console.WriteLine("\nNo expenses found.");
            }
            else
            {
                Console.WriteLine("\nSearch Results:\n");

                foreach (var expense in results)
                {
                    Console.WriteLine(
                        $"ID: {expense.Id} | " +
                        $"Date: {expense.ExpenseDate:dd-MM-yyyy} | " +
                        $"Amount: {expense.Amount:N2} | " +
                        $"Payment: {expense.PaymentMethod} | " +
                        $"Description: {expense.Description}");
                }

                Console.WriteLine(
                    $"\nCategory Total: {results.Sum(e => e.Amount):N2}");
            }

            Pause();
        }

        
        public void MonthlySummary()
        {
            Console.Clear();
            Console.WriteLine("========== MONTHLY SUMMARY ==========\n");

            Console.Write("Enter Year: ");

            if (!int.TryParse(Console.ReadLine(), out int year))
            {
                Console.WriteLine("Invalid year.");
                Pause();
                return;
            }

            Console.Write("Enter Month (1-12): ");

            if (!int.TryParse(Console.ReadLine(), out int month) ||
                month < 1 || month > 12)
            {
                Console.WriteLine("Invalid month.");
                Pause();
                return;
            }

            var monthlyExpenses = expenses
                .Where(e =>
                    e.ExpenseDate.Year == year &&
                    e.ExpenseDate.Month == month)
                .ToList();

            Console.WriteLine(
                $"\nExpenses for {month:D2}/{year}");

            Console.WriteLine("-----------------------------");

            if (monthlyExpenses.Count == 0)
            {
                Console.WriteLine("No expenses found.");
            }
            else
            {
                foreach (var expense in monthlyExpenses)
                {
                    Console.WriteLine(
                        $"{expense.ExpenseDate:dd-MM-yyyy} | " +
                        $"{expense.Category,-15} | " +
                        $"{expense.Amount:N2}");
                }

                Console.WriteLine(
                    "\nMonthly Total: " +
                    monthlyExpenses.Sum(e => e.Amount).ToString("N2"));
            }

            Pause();
        }

        private void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    class Program
    {
        static void Main()
        {
            ExpenseTracker tracker = new ExpenseTracker();

            while (true)
            {
                Console.Clear();

                Console.WriteLine("====================================");
                Console.WriteLine("       EXPENSE TRACKING SYSTEM");
                Console.WriteLine("====================================");
                Console.WriteLine("1. Add Expense");
                Console.WriteLine("2. View All Expenses");
                Console.WriteLine("3. Update Expense");
                Console.WriteLine("4. Delete Expense");
                Console.WriteLine("5. Search by Category");
                Console.WriteLine("6. Monthly Summary");
                Console.WriteLine("7. Exit");
                Console.WriteLine("====================================");

                Console.Write("Enter your choice: ");

                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            tracker.AddExpense();
                            break;

                        case 2:
                            tracker.ViewExpenses();
                            break;

                        case 3:
                            tracker.UpdateExpense();
                            break;

                        case 4:
                            tracker.DeleteExpense();
                            break;

                        case 5:
                            tracker.SearchByCategory();
                            break;

                        case 6:
                            tracker.MonthlySummary();
                            break;

                        case 7:
                            Console.WriteLine(
                                "\nThank you for using Expense Tracker!");
                            return;

                        default:
                            Console.WriteLine(
                                "\nInvalid choice. Please select 1-7.");
                            Console.ReadKey();
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine(
                        "\nError: Please enter a valid number.");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        "\nUnexpected Error: " + ex.Message);
                    Console.ReadKey();
                }
            }
        }
    }
}