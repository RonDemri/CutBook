namespace CutBook.Models
{
    public class MyTime
    {
        public int Hour { get; set; }
        public int Minute { get; set; }

        public MyTime(int hour, int minute)
        {
            this.Hour = hour + (minute / 60);
            this.Minute = minute % 60;
            this.Hour = this.Hour % 24;
        }

        public override string ToString()
        {
            return $"{Hour:D2}:{Minute:D2}";
        }
    }

    public class AppointmentTime : MyTime
    {
        public int Duration { get; set; }

        public AppointmentTime(MyTime time, int duration) : base(time.Hour, time.Minute)
        {
            Duration = duration;
        }
    }

    public class TimeHelper
    {
        public static MyTime[] GetAvailableTimes(MyTime[] workTimes, AppointmentTime[] existingAppointments, int appointmentDuration)
        {
            MyTime[] tempResults = new MyTime[workTimes.Length];
            int count = 0;

            for (int i = 0; i < workTimes.Length; i++)
            {
                MyTime currentWorkTime = workTimes[i];
                int startMin = currentWorkTime.Hour * 60 + currentWorkTime.Minute;
                int endMin = startMin + appointmentDuration;

                bool isBlocked = false;

                for (int j = 0; j < existingAppointments.Length && !isBlocked; j++)
                {
                    AppointmentTime appt = existingAppointments[j];
                    int apptStart = appt.Hour * 60 + appt.Minute;
                    int apptEnd = apptStart + appt.Duration;

                    if (startMin < apptEnd && endMin > apptStart)
                        isBlocked = true;
                }

                if (!isBlocked)
                {
                    bool hasContinuousWorkTime = false;
                    for (int k = 0; k < workTimes.Length; k++)
                    {
                        int workBlock = workTimes[k].Hour * 60 + workTimes[k].Minute;
                        if (workBlock >= endMin - 10)
                        {
                            hasContinuousWorkTime = true;
                            break;
                        }
                    }

                    if (hasContinuousWorkTime)
                    {
                        tempResults[count] = currentWorkTime;
                        count++;
                    }
                }
            }

            MyTime[] finalAvailableTimes = new MyTime[count];
            for (int i = 0; i < count; i++)
            {
                finalAvailableTimes[i] = tempResults[i];
            }

            return finalAvailableTimes;
        }
    }
}
