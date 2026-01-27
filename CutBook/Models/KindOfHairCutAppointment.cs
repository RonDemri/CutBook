namespace CutBook.Models
{
    public class KindOfHairCutAppointment
    {
        int appointmentID;
        int hairCutID1;
        int hairCutID2;
        int hairCutID3;
        public KindOfHairCutAppointment(int appointmentID, int hairCutID1, int hairCutID3, int hairCutID2)
        {
            this.appointmentID = appointmentID;
            this.hairCutID1 = hairCutID1;
            this.hairCutID2 = hairCutID2;
            this.hairCutID3 = hairCutID3;
        }
        public int GetAppointmentID()
        {
            return this.appointmentID;
        }
        public void SetAppointmentID(int appointmentID)
        {
            this.appointmentID = appointmentID;
        }
        public int GetHairCutID1()
        {
            return this.hairCutID1;
        }
        public void SetHairCutID1(int hairCutID1)
        {
            this.hairCutID1 = hairCutID1;
        }
        public int GetHairCutID2()
        {
            return this.hairCutID2;
        }
        public void SetHairCutID2(int hairCutID2)
        {
            this.hairCutID2 = hairCutID2;
        }
        public int GetHairCutID3()
        {
            return this.hairCutID3;
        }
        public void SetHairCutID3(int hairCutID3)
        {
            this.hairCutID3 = hairCutID3;
        }
    }
}
