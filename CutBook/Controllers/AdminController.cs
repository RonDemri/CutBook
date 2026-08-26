using CutBook.DataAccess;
using CutBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CutBook.Controllers
{
    public class AdminController : Controller
    {
        private bool IsAdminLoggedIn()
        {
            return HttpContext.Session.GetString("IsAdmin") == "true";
        }

        // 1. צפייה בכל התורים במערכת
        public IActionResult Appointments()
        {
            if (!IsAdminLoggedIn()) return RedirectToAction("GetLoginForm", "Home");
            DB_Helper dbHelper = new DB_Helper();
            string sql = @"SELECT Appointment.AppointmentID, Appointment.AppointmentDate, Appointment.AppointmentTime, Appointment.IsPayed, Appointment.UserID, Users.userName, Users.userTel, KindOfHaircut.HaircutName 
                   FROM (((Appointment 
                   INNER JOIN KindOfHaircutAppointment ON Appointment.AppointmentID = KindOfHaircutAppointment.AppointmentID) 
                   INNER JOIN KindOfHaircut ON KindOfHaircutAppointment.HaircutID = KindOfHaircut.HaircutID)
                   INNER JOIN Users ON Appointment.UserID = Users.userID)";
            DataTable dt = dbHelper.GetDataTable(sql, "Appointment");
            List<Appointment> list = new List<Appointment>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Appointment(
                    Convert.ToInt32(row["AppointmentID"]),
                    row["AppointmentDate"].ToString(),
                    row["AppointmentTime"].ToString(),
                    Convert.ToBoolean(row["IsPayed"]),
                    row["UserID"].ToString(),
                    row["HaircutName"].ToString(),
                    row["userName"].ToString(),
                    row["userTel"].ToString()
                ));
            }
            var sortedList = list
                .OrderByDescending(a => DateTime.TryParse($"{a.GetAppointmentDate()} {a.GetAppointmentTime()}", out var d) ? d : DateTime.MinValue)
                .ToArray();
            return View(sortedList);
        }
        // 2. שינוי סטטוס תשלום
        [HttpPost]
        public IActionResult TogglePayment(int appointmentId, bool currentStatus)
        {
            if (!IsAdminLoggedIn()) return RedirectToAction("GetLoginForm", "Home");
            DB_Helper dbHelper = new DB_Helper();
            int newStatus = currentStatus ? 0 : 1;
            string sql = $"UPDATE Appointment SET IsPayed = {newStatus} WHERE AppointmentID = {appointmentId}";
            dbHelper.ChangeDb(sql);
            return RedirectToAction("Appointments");
        }

        // 3. מחיקת תור
        [HttpPost]
        public IActionResult DeleteAppointment(int appointmentId)
        {
            if (!IsAdminLoggedIn()) return RedirectToAction("GetLoginForm", "Home");
            DB_Helper dbHelper = new DB_Helper();
            dbHelper.ChangeDb($"DELETE FROM KindOfHaircutAppointment WHERE AppointmentID = {appointmentId}");
            dbHelper.ChangeDb($"DELETE FROM Appointment WHERE AppointmentID = {appointmentId}");
            return RedirectToAction("Appointments");
        }

        // 4. צפייה בהכנסות
        public IActionResult MyIncomes()
        {
            if (!IsAdminLoggedIn()) return RedirectToAction("GetLoginForm", "Home");
            DB_Helper dbHelper = new DB_Helper();
            string sql = @"SELECT Appointment.AppointmentDate, KindOfHaircut.HaircutPrice 
                           FROM ((Appointment 
                           INNER JOIN KindOfHaircutAppointment ON Appointment.AppointmentID = KindOfHaircutAppointment.AppointmentID) 
                           INNER JOIN KindOfHaircut ON KindOfHaircutAppointment.HaircutID = KindOfHaircut.HaircutID) 
                           WHERE Appointment.IsPayed = True";
            DataTable dt = dbHelper.GetDataTable(sql, "Appointment");
            double monthlyIncome = 0;
            double yearlyIncome = 0;
            DateTime now = DateTime.Now;

            foreach (DataRow row in dt.Rows)
            {
                if (DateTime.TryParse(row["AppointmentDate"].ToString(), out DateTime date))
                {
                    double price = Convert.ToDouble(row["HaircutPrice"]);

                    if (date.Year == now.Year)
                    {
                        yearlyIncome += price;
                        if (date.Month == now.Month)
                        {
                            monthlyIncome += price;
                        }
                    }
                }
            }
            ViewBag.MonthlyIncome = monthlyIncome;
            ViewBag.YearlyIncome = yearlyIncome;
            return View();
        }
    }
}