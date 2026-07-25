using System;
namespace UniversityStudentAdmission
{
class StudentAdmission
{
private string name, course;
private double percentage, courseFee, scholarship;
public StudentAdmission(string name,double percentage)
{
this.name=name;
this.percentage=percentage;
}
public bool VerifyEligibility()
{
return percentage>=50;
}
public void ChooseDepartment()
{
Console.Write("--------Available Department--------\n");
Console.WriteLine(" 1. CSE - Rs. 60000\n 2. AI - Rs. 65000\n 3. Cyber
Security - Rs. 70000");
Console.Write("Select the Department(1-3):");
int ch=int.Parse(Console.ReadLine());
switch(ch){
case 1: course="CSE"; courseFee=60000; break;
case 2: course="AI"; courseFee=65000; break;
default: course="Cyber Security"; courseFee=62000; break;
}
}
public void CalculateScholarship()
{
if(percentage>=95) scholarship=courseFee;else if(percentage>=85) scholarship=courseFee*0.5;
else if(percentage>=75) scholarship=courseFee*0.25;
else scholarship=0;
}
public void DisplayReceipt()
{
Console.WriteLine("\n===== UNIVERSITY ADMISSION RECEIPT =====");
Console.WriteLine("Student : "+name);
Console.WriteLine("Course : "+course);
Console.WriteLine("Fee : "+courseFee);
Console.WriteLine("Scholar : "+scholarship);
Console.WriteLine("Payable : "+(courseFee-scholarship));
}
}
class Program
{
static void Main()
{
Console.Write("Enter Name: ");
string name=Console.ReadLine();
Console.Write("Enter 12th Percentage: ");
double per=double.Parse(Console.ReadLine());
StudentAdmission s=new StudentAdmission(name,per);
if(s.VerifyEligibility())
{
Console.WriteLine("Congratulations You Are Eligible for Admission");
s.ChooseDepartment();
s.CalculateScholarship();
s.DisplayReceipt();
}
else
{
Console.WriteLine("Not Eligible for Admission");
}
}
}
}