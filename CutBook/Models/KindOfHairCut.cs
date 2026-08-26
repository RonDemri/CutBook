namespace CutBook.Models
{
    public class KindOfHaircut
    {
        int hairCutID;
        string hairCutName;
        string hairCutPrice;
        string hairCutTime;
        public KindOfHaircut(int hairCutID, string hairCutName, string hairCutPrice, string hairCutTime)
        {
            this.hairCutID = hairCutID;
            this.hairCutName = hairCutName;
            this.hairCutPrice = hairCutPrice;
            this.hairCutTime = hairCutTime;
        }
        public int GetHairCutID()
        {
            return this.hairCutID;
        }
        public void SetHairCutID(int hairCutID)
        {
            this.hairCutID = hairCutID;
        }
        public string GetHairCutName()
        {
            return this.hairCutName;
        }
        public void SetHairCutName(string hairCutName)
        {
            this.hairCutName = hairCutName;
        }
        public string GetHairCutPrice()
        {
            return this.hairCutPrice;
        }
        public void SetHairCutPrice(string hairCutPrice)
        {
            this.hairCutPrice = hairCutPrice;
        }
        public string GetHairCutTime()
        {
            return this.hairCutTime;
        }
        public void SetHairCutTime(string hairCutTime)
        {
            this.hairCutTime = hairCutTime;
        }
    }
}
