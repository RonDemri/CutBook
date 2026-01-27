namespace CutBook.Models
{
    public class HairCutImage
    {
        int imageID;
        string imageName;
        int hairCutID;
        public HairCutImage(int imageID, string imageName, int hairCutID)
        {
            this.imageID = imageID;
            this.imageName = imageName;
            this.hairCutID = hairCutID;
        }
        public int GetImageID()
        {
            return this.imageID;
        }
        public void SetImageID(int imageID)
        {
            this.imageID = imageID;
        }
        public string GetImageName()
        {
            return this.imageName;
        }
        public void SetImageName(string imageName)
        {
            this.imageName = imageName;
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
