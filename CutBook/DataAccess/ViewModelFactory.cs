using CutBook.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace CutBook.DataAccess
{
    public class ViewModelFactory
    {
        ModelFactory modelFactory;
        DB_Helper dbHelper;
        private object userID;

        public ViewModelFactory(DB_Helper DBHelper)
        {
            this.modelFactory = new ModelFactory();
            this.dbHelper = DBHelper;
        }

        public Appointment[] GetAllAppointments(string userID)
        {
            string sql = $@"SELECT Appointment.AppointmentID, Appointment.AppointmentDate, Appointment.AppointmentTime, Appointment.IsPayed, Appointment.UserID, KindOfHaircut.HaircutName 
                    FROM ((Appointment 
                    INNER JOIN KindOfHaircutAppointment ON Appointment.AppointmentID = KindOfHaircutAppointment.AppointmentID) 
                    INNER JOIN KindOfHaircut ON KindOfHaircutAppointment.HaircutID = KindOfHaircut.HaircutID) 
                    WHERE Appointment.UserID = '{userID}'";
            DataTable dt = this.dbHelper.GetDataTable(sql, "Appointment");
            List<Appointment> list = new List<Appointment>();
            foreach (DataRow row in dt.Rows)
            {
                int id = Convert.ToInt32(row["AppointmentID"]);
                string date = row["AppointmentDate"].ToString();
                string time = row["AppointmentTime"].ToString();
                bool isPayed = Convert.ToBoolean(row["IsPayed"]);
                string uId = row["UserID"].ToString();
                string haircutName = row["HaircutName"].ToString();
                Appointment app = new Appointment(id, date, time, isPayed, uId, haircutName);
                list.Add(app);
            }
            return list.ToArray();
        }

        public MakeAnAppointmentViewModel GetTimes()
        {
            return null;
        }

        public bool AddNewAppointment(Appointment appointment)
        {
            string sql = $"INSERT INTO Appointment (AppointmentDate, AppointmentTime, IsPayed, UserID) " +
                $"VALUES ('{appointment.GetAppointmentDate()}', '{appointment.GetAppointmentTime()}', {appointment.GetIsPayed()}, '{appointment.GetUserID()}')";
            int numOfRows = this.dbHelper.ChangeDb(sql);
            return numOfRows > 0;
        }

        public bool UpdateAppointment(Appointment appointment)
        {
            string sql = @$"UPDATE Appointment SET AppointmentDate='{appointment.GetAppointmentDate()}',
                            AppointmentTime='{appointment.GetAppointmentTime()}', 
                            IsPayed={appointment.GetIsPayed()}, 
                            UserID='{appointment.GetUserID()}' 
                            WHERE AppointmentID={appointment.GetAppointmentID()}";
            int numOfRows = this.dbHelper.ChangeDb(sql);
            return numOfRows > 0;
        }

        public string LoginUser(string userID, string password)
        {
            string sql = $@"SELECT userID FROM Users WHERE userID='{userID}' AND userPassword='{password}'";
            DataTable dt = this.dbHelper.GetDataTable(sql, "User");
            if (dt.Rows.Count == 0)
            {
                return null;

            }
            return dt.Rows[0]["userID"].ToString();
        }

        public string GetUserName(string userID)
        {
            string sql = $"SELECT userName FROM Users WHERE userID='{userID}'";
            DataTable dt = this.dbHelper.GetDataTable(sql, "User");

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["userName"].ToString();
            }

            return null;
        }

        public string AddNewUser(User user)
        {
            string sql = $@"INSERT INTO Users (userID, userTel, userEmail, userName, userPassword) 
                    VALUES ('{user.GetUserID()}', '{user.GetUserTel()}', '{user.GetUserEmail()}', '{user.GetUserName()}', '{user.GetUserPassword()}')";
            int rows = this.dbHelper.ChangeDb(sql);

            if (rows > 0)
            {
                return user.GetUserID();
            }
            return null;
        }

        public bool IsUserExist(string userID, string userEmail)
        {
            string sql = $@"SELECT * FROM Users WHERE UserID = '{userID}' OR UserEmail = '{userEmail}'";
            DataTable dt = this.dbHelper.GetDataTable(sql, "Users");
            return dt != null && dt.Rows.Count > 0;
        }

        // ==========================================
        // מתודות חדשות עבור מנגנון התורים והזמנים
        // ==========================================

        public List<KindOfHaircut> GetAllHaircutTypes()
        {
            List<KindOfHaircut> list = new List<KindOfHaircut>();
            string sql = "SELECT * FROM KindOfHaircut";
            DataTable dt = this.dbHelper.GetDataTable(sql, "KindOfHaircut");

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new KindOfHaircut(
                    Convert.ToInt32(row["HaircutID"]),
                    row["HaircutName"].ToString(),
                    row["HaircutPrice"].ToString(),
                    row["HaircutTime"].ToString()
                ));
            }
            return list;
        }

        public AppointmentTime[] GetAppointmentsForDate(string selectedDate)
        {
            List<AppointmentTime> list = new List<AppointmentTime>();
            string sql = $@"SELECT Appointment.AppointmentTime, KindOfHaircut.HaircutTime
                    FROM (Appointment
                    INNER JOIN KindOfHaircutAppointment ON Appointment.AppointmentID = KindOfHaircutAppointment.AppointmentID)
                    INNER JOIN KindOfHaircut ON KindOfHaircutAppointment.HaircutID = KindOfHaircut.HaircutID
                    WHERE Appointment.AppointmentDate = '{selectedDate}'";

            DataTable dt = this.dbHelper.GetDataTable(sql, "Appointment");
            foreach (DataRow row in dt.Rows)
            {
                string timeStr = row["AppointmentTime"].ToString().Trim();
                string[] timeParts = timeStr.Split(':');

                int hour = int.Parse(timeParts[0]);
                int minute = int.Parse(timeParts[1]);
                int duration = Convert.ToInt32(row["HaircutTime"]);

                list.Add(new AppointmentTime(new MyTime(hour, minute), duration));
            }
            return list.ToArray();
        }

        public int GetLastAppointmentID(string userId)
        {
            string sql = $"SELECT MAX(AppointmentID) AS LastID FROM Appointment WHERE UserID='{userId}'";
            DataTable dt = this.dbHelper.GetDataTable(sql, "Appointment");
            if (dt.Rows.Count > 0 && dt.Rows[0]["LastID"] != DBNull.Value)
            {
                return Convert.ToInt32(dt.Rows[0]["LastID"]);
            }
            return 0;
        }

        public bool AddHaircutToAppointment(int appointmentId, int haircutId)
        {
            string sql = $"INSERT INTO KindOfHaircutAppointment (AppointmentID, HaircutID) VALUES ({appointmentId}, {haircutId})";
            int numOfRows = this.dbHelper.ChangeDb(sql);
            return numOfRows > 0;
        }

        public bool DeleteAppointment(int appointmentId)
        {
            // מחיקת הקשר מטבלת המפתח הזר ומחיקת התור עצמו
            string sqlLink = $"DELETE FROM KindOfHaircutAppointment WHERE AppointmentID = {appointmentId}";
            string sqlApp = $"DELETE FROM Appointment WHERE AppointmentID = {appointmentId}";
            this.dbHelper.ChangeDb(sqlLink);
            int rowsAffected = this.dbHelper.ChangeDb(sqlApp);
            return rowsAffected > 0;
        }
        public bool IsUserAdmin(string userID)
        {
            string sql = $"SELECT IsAdmin FROM Users WHERE UserID = '{userID}'";
            DataTable dt = this.dbHelper.GetDataTable(sql, "Users");
            if (dt.Rows.Count > 0 && dt.Rows[0]["IsAdmin"] != DBNull.Value)
            {
                return Convert.ToBoolean(dt.Rows[0]["IsAdmin"]);
            }
            return false;
        }
    }
}