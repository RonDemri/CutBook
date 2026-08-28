using CutBook.DataAccess;
using CutBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CutBook.Controllers
{
    public class UserController : Controller
    {
        public IActionResult ViewAppointments()
        {
            if (HttpContext.Session.GetString("userID") == null)
            {
                return RedirectToAction("GetLoginForm", "Home");
            }
            string userID = HttpContext.Session.GetString("userID");
            DB_Helper dbHelper = new DB_Helper();
            ViewModelFactory viewModelFactory = new ViewModelFactory(dbHelper);
            Appointment[] appointments = viewModelFactory.GetAllAppointments(userID);
            return View(appointments);
        }

        [HttpGet]
        public IActionResult MakeAnAppointment()
        {
            if (HttpContext.Session.GetString("userID") == null)
            {
                return RedirectToAction("GetLoginForm", "Home");
            }
            DB_Helper dbHelper = new DB_Helper();
            ViewModelFactory factory = new ViewModelFactory(dbHelper);
            ViewBag.Haircuts = factory.GetAllHaircutTypes();
            return View();
        }

        [HttpGet]
        public IActionResult GetAvailableHours(string selectedDate, int haircutId)
        {
            if (!DateTime.TryParse(selectedDate, out DateTime parsedSelectedDate))
            {
                return Json(new List<string>());
            }
            DateTime now = DateTime.Now;
            if (parsedSelectedDate.Date < now.Date)
            {
                return Json(new List<string>());
            }

            DB_Helper dbHelper = new DB_Helper();
            ViewModelFactory factory = new ViewModelFactory(dbHelper);
            var haircuts = factory.GetAllHaircutTypes();
            var selectedHaircut = haircuts.FirstOrDefault(h => h.GetHairCutID() == haircutId);

            int duration = 20;
            if (selectedHaircut != null)
            {
                int.TryParse(selectedHaircut.GetHairCutTime(), out duration);
            }

            AppointmentTime[] existingAppointments = factory.GetAppointmentsForDate(selectedDate);

            int totalSlots = (19 - 9) * 6;
            List<MyTime> availableTimes = new List<MyTime>();
            bool isToday = (parsedSelectedDate.Date == now.Date);
            int currentMinutesNow = now.Hour * 60 + now.Minute; 
            for (int i = 0; i < totalSlots; i++)
            {
                MyTime slot = new MyTime(9 + (i / 6), (i % 6) * 10);

                int slotStart = slot.Hour * 60 + slot.Minute;
                int slotEnd = slotStart + duration;
                if (isToday && slotStart <= currentMinutesNow)
                {
                    continue; 
                }
                bool isOverlap = false;
                if (existingAppointments != null)
                {
                    foreach (var app in existingAppointments)
                    {
                        int existingStart = app.Hour * 60 + app.Minute;
                        int existingEnd = existingStart + app.Duration;

                        if (slotStart < existingEnd && slotEnd > existingStart)
                        {
                            isOverlap = true;
                            break;
                        }
                    }
                }
                if (!isOverlap)
                {
                    availableTimes.Add(slot);
                }
            }
            return Json(availableTimes.Select(t => t.ToString()).ToList());
        }

        [HttpPost]
        public IActionResult SubmitAppointment(string appointmentDate, string appointmentTime, int haircutId)
        {
            string userID = HttpContext.Session.GetString("userID");
            if (userID == null)
            {
                return RedirectToAction("GetLoginForm", "Home");
            }
            DB_Helper dbHelper = new DB_Helper();
            ViewModelFactory factory = new ViewModelFactory(dbHelper);
            Appointment newAppointment = new Appointment(0, appointmentDate, appointmentTime, false, userID);
            bool isSaved = factory.AddNewAppointment(newAppointment);
            if (isSaved)
            {
                int newAppointmentID = factory.GetLastAppointmentID(userID);
                factory.AddHaircutToAppointment(newAppointmentID, haircutId);
                return RedirectToAction("ViewAppointments");
            }
            ViewBag.Error = "הזמנת התור נכשלה, נסה שנית.";
            ViewBag.Haircuts = factory.GetAllHaircutTypes();
            return View("MakeAnAppointment");
        }

        [HttpPost]
        public IActionResult CancelAppointment(int appointmentId)
        {
            DB_Helper dbHelper = new DB_Helper();
            ViewModelFactory factory = new ViewModelFactory(dbHelper);
            factory.DeleteAppointment(appointmentId);
            return RedirectToAction("ViewAppointments");
        }
    }
}