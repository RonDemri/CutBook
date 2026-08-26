namespace CutBook.Models
{
    public class KindOfHairCutAppointment
    {
        int appointmentID;
        int hairCutID;
        public KindOfHairCutAppointment(int appointmentID, int hairCutID)
        {
            this.appointmentID = appointmentID;
            this.hairCutID = hairCutID;
        }
        public int GetAppointmentID()
        {
            return this.appointmentID;
        }
        public void SetAppointmentID(int appointmentID)
        {
            this.appointmentID = appointmentID;
        }
        public int GetHairCutID()
        {
            return this.hairCutID;
        }
        public void SetHairCutID(int hairCutID)
        {
            this.hairCutID = hairCutID;
        }
    }
}
