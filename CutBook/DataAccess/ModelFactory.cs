using CutBook.Models;
using System.Data;

namespace CutBook.DataAccess
{
    public class ModelFactory
    {
        public Appointment GetAppointment(DataRow dataRow)
        {
            int appointmentID = int.Parse(dataRow["appointmentID"].ToString()); 
            string appointmentDate = (dataRow["appointmentDate"].ToString());
            string appointmentTime = (dataRow["appointmentTime"].ToString());
            bool isPayed = bool.Parse(dataRow["isPayed"].ToString());
            string userID = (dataRow["userID"].ToString());
            return new Appointment(appointmentID, appointmentDate, appointmentTime,
                isPayed, userID);
        }
        public HairCutImage GetHirCutImage(DataRow dataRow)
        {
            int imageID = int.Parse(dataRow["imageID"].ToString());
            string imageName = (dataRow["imageName"].ToString());
            int hairCutID = int.Parse(dataRow["hairCutID"].ToString());
            return new HairCutImage(imageID, imageName, hairCutID);
        }
        public KindOfHaircut GetKindOfHairCut(DataRow dataRow)
        {
            int hairCutID = int.Parse(dataRow["hairCutID"].ToString());
            string hairCutName = (dataRow["hairCutName"].ToString());
            string hairCutPrice = (dataRow["hairCutPrice"].ToString());
            string hairCutTime = (dataRow["hairCutTime"].ToString());
            return new KindOfHaircut(hairCutID, hairCutName, hairCutPrice, hairCutTime);
        }
        public KindOfHairCutAppointment GetKindOfHairCutAppointment(DataRow dataRow)
        {
            int appointmentID = int.Parse(dataRow["appointmentID"].ToString());
            int hairCutID = int.Parse(dataRow["hairCutID"].ToString());
            return new KindOfHairCutAppointment(appointmentID, hairCutID);
        }
        public User GetUser(DataRow dataRow)
        {
            string userID = dataRow["userID"].ToString();
            string userTel = dataRow["userTel"].ToString();
            string userEmail = dataRow["userEmail"].ToString();
            string userName = dataRow["userName"].ToString();
            string userPassword = dataRow["userPassword"].ToString();
            bool isAdmin = dataRow["IsAdmin"] != DBNull.Value && Convert.ToBoolean(dataRow["IsAdmin"]);
            return new User(userID, userTel, userEmail, userName, userPassword, isAdmin);
        }
    }
}
