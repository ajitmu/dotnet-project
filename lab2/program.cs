using System;

namespace EmployeePayrollSystem
{
    // Interface
    interface IPayroll
    {
        double CalculateSalary();
    }

    // Base Class
    abstract class Employee : IPayroll
    {
        protected int id;
        protected string name;

        public Employee(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public abstract double CalculateSalary();

        public virtual void Display()
        {
            Console.WriteLine("Employee ID   : " + id);
            Console.WriteLine("Employee Name : " + name);
        }
    }

    // Full Time Employee
    class FullTimeEmployee : Employee
    {
        private double monthlySalary;

        public FullTimeEmployee(int id, string name, double salary)
            : base(id, name)
        {
            monthlySalary = salary;
        }

        public override double CalculateSalary()
        {
            return monthlySalary;
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine("Employee Type : Full Time");
            Console.WriteLine("Salary        : " + CalculateSalary());
            Console.WriteLine();
        }
    }

    // Part Time Employee
    class PartTimeEmployee : Employee
    {
        private int hoursWorked;
        private double hourlyRate;

        public PartTimeEmployee(int id, string name, int hours, double rate)
            : base(id, name)
        {
            hoursWorked = hours;
            hourlyRate = rate;
        }

        public override double CalculateSalary()
        {
            return hoursWorked * hourlyRate;
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine("Employee Type : Part Time");
            Console.WriteLine("Hours Worked  : " + hoursWorked);
            Console.WriteLine("Hourly Rate   : " + hourlyRate);
            Console.WriteLine("Salary        : " + CalculateSalary());
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Employee[] employees = new Employee[10];
            int count = 0;
            int choice;

            do
            {
                Console.WriteLine("===== Employee Payroll System =====");
                Console.WriteLine("1. Add Full Time Employee");
                Console.WriteLine("2. Add Part Time Employee");
                Console.WriteLine("3. Display Employees");
                Console.WriteLine("4. Exit");
                Console.Write("Enter Choice: ");
                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter ID: ");
                        int fid = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter Name: ");
                        string fname = Console.ReadLine();

                        Console.Write("Enter Monthly Salary: ");
                        double salary = Convert.ToDouble(Console.ReadLine());

                        employees[count] = new FullTimeEmployee(fid, fname, salary);
                        count++;

                        Console.WriteLine("Full Time Employee Added Successfully.\n");
                        break;

                    case 2:
                        Console.Write("Enter ID: ");
                        int pid = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter Name: ");
                        string pname = Console.ReadLine();

                        Console.Write("Enter Hours Worked: ");
                        int hours = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter Hourly Rate: ");
                        double rate = Convert.ToDouble(Console.ReadLine());

                        employees[count] = new PartTimeEmployee(pid, pname, hours, rate);
                        count++;

                        Console.WriteLine("Part Time Employee Added Successfully.\n");
                        break;

                    case 3:
                        if (count == 0)
                        {
                            Console.WriteLine("No Employees Found.\n");
                        }
                        else
                        {
                            Console.WriteLine("\nEmployee Details\n");

                            for (int i = 0; i < count; i++)
                            {
                                employees[i].Display();
                            }
                        }
                        break;

                    case 4:
                        Console.WriteLine("Thank You!");
                        Console.ReadLine();
                        break;
                      

                    default:
                        Console.WriteLine("Invalid Choice.\n");
                        break;
                }

            } while (choice != 4);
        }
    }
}
