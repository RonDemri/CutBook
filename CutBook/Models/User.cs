namespace CutBook.Models
{
    public class User
    {
        string userID;
        string userTel;
        string userEmail;
        string userName;
        string userPassword;
        bool isAdmin;
        public User(string userID, string userTel, string userEmail,
            string userName, string userPassword, bool isAdmin)
        {
            this.userID = userID;
            this.userTel = userTel;
            this.userEmail = userEmail;
            this.userName = userName;
            this.userPassword = userPassword;
            this.isAdmin = isAdmin;
        }
        public string GetUserID()
        {
            return this.userID;
        }
        public void SetUserID(string userID)
        {
            this.userID = userID;
        }
        public string GetUserTel()
        {
            return this.userTel;
        }
        public void SetUserTel(string userTel)
        {
            this.userTel = userTel;
        }
        public string GetUserEmail()
        {
            return this.userEmail;
        }
        public void SetUserEmail(string userEmail)
        {
            this.userEmail = userEmail;
        }
        public string GetUserName()
        {
            return this.userName;
        }
        public void SetUserName(string userName)
        {
            this.userName = userName;
        }
        public string GetUserPassword()
        {
            return this.userPassword;
        }
        public void SetUserPassword(string userPassword)
        {
            this.userPassword = userPassword;
        }
        public bool GetIsAdmin()
        {
            return this.isAdmin;
        }
        public void setIsAdmin(bool isAdmin)
        {
            this.isAdmin = isAdmin;
        }
    }
}
