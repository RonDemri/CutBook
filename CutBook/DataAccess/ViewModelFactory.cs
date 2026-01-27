using CutBook.Models;
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
        public Appointment[] GetAllAppointments()
        {
            string sql = "SELECT * FROM Appointments";
            DataTable dt = this.dbHelper.GetDataTable(sql, "Appointment");
            Appointment[] appointments = new Appointment[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                appointments[i] = this.modelFactory.GetAppointment(dt.Rows[i]);
            }
            return appointments;
        }
        public MakeAnAppointmentViewModel GetMakeAnAppointmentView()
        {

        }
    }
}
