namespace CutBook.Models
{
    public class MakeAnAppointmentViewModel
    {
        KindOfHairCut[] kindOfHairCuts;
        string[] dates;
        string[] times;
        int countKindOfHairCuts;
        int countDates;
        int countTimes;
        public MakeAnAppointmentViewModel(int countKindOfHairCuts, int countDates, int countTimes)
        {
            this.countKindOfHairCuts = 0;
            this.countDates = 0;
            this.countTimes = 0;
        }
        public void AddKindOfHairCuts(KindOfHairCut kindOfHairCuts)
        {
            this.kindOfHairCuts[this.countKindOfHairCuts] = kindOfHairCuts;
            this.countKindOfHairCuts++;
        }
        public void AddDates(string dates)
        {
            this.dates[this.countDates] = dates;
            this.countDates++;
        }
        public void AddTimes(string times)
        {
            this.times[this.countTimes] = times;
            this.countTimes++;
        }
        public KindOfHairCut GetKindOfHairCut(int index)
        {
            return this.kindOfHairCuts[index];
        }
        public string GetDates(int index)
        {
            return this.dates[index];
        }
        public string GetTimes(int index)
        {
            return this.times[index];
        }
    }

}
