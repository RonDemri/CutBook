namespace CutBook.Models
{
    public class Appointment
    {
        public int appointmentID;
        public string appointmentDate;
        public string appointmentTime;
        public bool isPayed;
        public string userID;
        public string haircutName;
        string userName;
        string userTel;
        public Appointment(int appointmentID, string appointmentDate, string appointmentTime,
                           bool isPayed, string userID, string haircutName = "", string userName = "", string userTel = "")
        {
            this.appointmentID = appointmentID;
            this.appointmentDate = appointmentDate;
            this.appointmentTime = appointmentTime;
            this.isPayed = isPayed;
            this.userID = userID;
            this.haircutName = haircutName;
            this.userName = userName;
            this.userTel = userTel;
        }

        public int GetAppointmentID()
        {
            return this.appointmentID;
        }
        public void SetAppointmentID(int appointmentID)
        {
            this.appointmentID = appointmentID;
        }

        public string GetAppointmentDate()
        {
            return this.appointmentDate;
        }
        public void SetAppointmentDate(string appointmentDate)
        {
            this.appointmentDate = appointmentDate;
        }

        public string GetAppointmentTime()
        {
            return this.appointmentTime;
        }
        public void SetAppointmentTime(string appointmentTime)
        {
            this.appointmentTime = appointmentTime;
        }

        public bool GetIsPayed()
        {
            return this.isPayed;
        }
        public void SetIsPayed(bool isPayed)
        {
            this.isPayed = isPayed;
        }

        public string GetUserID()
        {
            return this.userID;
        }
        public void SetUserID(string userID)
        {
            this.userID = userID;
        }

        public string GetHaircutName()
        {
            return this.haircutName;
        }
        public void SetHaircutName(string haircutName)
        {
            this.haircutName = haircutName;
        }
        public string GetUserName()
        {
            return this.userName;
        }
        public void SetUserName(string userName)
        {
            this.userName = userName;
        }
        public string GetUserTel()
        {
            return this.userTel;
        }
        public void SetUserTel(string userTel)
        {
            this.userTel = userTel;
        }
    }
}