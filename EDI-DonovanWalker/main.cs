using System;

public interface IPerson
{
    string FirstName { get; set; }
    string LastName { get; set; }
    int Age { get; set; }
    string Address { get; set; }

    // Method to display relevant information about the person
    void Display();
}
public class Student : IPerson
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Address { get; set; }
    
    // Specific to Student
    public string StudentId { get; set; }
    public string Major { get; set; }

    // Constructor
    public Student(string firstName, string lastName, int age, string address, string studentId, string major)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        Address = address;
        StudentId = studentId;
        Major = major;
    }

    // Implement the Display method
    public void Display()
    {
        Console.WriteLine($"Student: {FirstName} {LastName}");
        Console.WriteLine($"Age: {Age}, Address: {Address}");
        Console.WriteLine($"Student ID: {StudentId}, Major: {Major}");
    }
}
public class Teacher : IPerson
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Address { get; set; }
    
    // Specific to Teacher
    public string EmployeeId { get; set; }
    public string Subject { get; set; }

    // Constructor
    public Teacher(string firstName, string lastName, int age, string address, string employeeId, string subject)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        Address = address;
        EmployeeId = employeeId;
        Subject = subject;
    }

    // Implement the Display method
    public void Display()
    {
        Console.WriteLine($"Teacher: {FirstName} {LastName}");
        Console.WriteLine($"Age: {Age}, Address: {Address}");
        Console.WriteLine($"Employee ID: {EmployeeId}, Subject: {Subject}");
    }
}
public class Employee : IPerson
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Address { get; set; }
    
    // Specific to Employee
    public string EmployeeId { get; set; }
    public string Department { get; set; }

    // Constructor
    public Employee(string firstName, string lastName, int age, string address, string employeeId, string department)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        Address = address;
        EmployeeId = employeeId;
        Department = department;
    }

    // Implement the Display method
    public void Display()
    {
        Console.WriteLine($"Employee: {FirstName} {LastName}");
        Console.WriteLine($"Age: {Age}, Address: {Address}");
        Console.WriteLine($"Employee ID: {EmployeeId}, Department: {Department}");
    }
}
class Program
{
    static void Main()
    {
        // Create instances of each class
        IPerson student = new Student("Donovan", "Walker", 21, "123 College St.", "S12345", "Computer Science");
        IPerson teacher = new Teacher("Jane", "Smith", 40, "456 School Rd.", "T98765", "Mathematics");
        IPerson employee = new Employee("Mark", "Johnson", 35, "789 Work Ave.", "E56789", "HR");

        // Display their information
        student.Display();
        Console.WriteLine();
        teacher.Display();
        Console.WriteLine();
        employee.Display();
    }
}
