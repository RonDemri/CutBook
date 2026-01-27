namespace CutBook.Models
{
    public class Appointment
    {
        int appointmentID;
        string appointmentDate;
        string appointmentTime;
        bool isPayed;
        string userID;
        public Appointment(int appointmentID, string appointmentDate, string appointmentTime,
            bool isPayed, string userID)
        {
            this.appointmentID = appointmentID;
            this.appointmentDate = appointmentDate;
            this.appointmentTime = appointmentTime;
            this.isPayed = isPayed;
            this.userID = userID; 
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
    }
}
