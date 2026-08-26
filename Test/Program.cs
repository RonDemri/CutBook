using CutBook;
using CutBook.DataAccess;
using CutBook.Models;
namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestAppointment();
            Console.ReadLine();
        }
        static void TestLogin()
        {
            DB_Helper dB_Helper = new DB_Helper();
            ViewModelFactory viewModelFactory = new ViewModelFactory(dB_Helper);
            Console.WriteLine("Enter userID:");
            string userID = Console.ReadLine();
            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();
            string loggedInUserID = viewModelFactory.LoginUser(userID, password);
            if (loggedInUserID != null)
            {
                Console.WriteLine($"Login successful! Welcome, {loggedInUserID}.");
            }
            else
            {
                Console.WriteLine("Login failed. Invalid userID or password.");
            }
        }
        static void TestAppointment() 
        {
            Console.WriteLine("insert user id -> ");
            string userID = Console.ReadLine();
            DB_Helper dB_Helper = new DB_Helper();
            ViewModelFactory viewModelFactory = new ViewModelFactory(dB_Helper);
            Appointment[] appointments = viewModelFactory.GetAllAppointments(userID);
            for (int i = 0;i < appointments.Length; i++)
            {
                Console.WriteLine($@"Appointment {i + 1}: 
                                     Date: {appointments[i].GetAppointmentDate()},
                                     Time: {appointments[i].GetAppointmentTime()},
                                     Is Payed: {appointments[i].GetIsPayed()}");
            }
        }
    }
}