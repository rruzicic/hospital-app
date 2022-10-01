using hospital.Model;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace Model
{
    public class User : INotifyPropertyChanged
    {
        private string username;
        private string password;
        private bool isBlocked;
        private Role role;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public User() { }
        public User(string _username, string _password, Role role, bool blocked)
        {
            username = _username;
            password = ComputeSha256Hash(_password);
            isBlocked = blocked;
            this.role = role;
        }
        public User(string _username, Role role, bool blocked)
        {
            username = _username;
            isBlocked = blocked;
            this.role = role;
        }

        private static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public string Username
        {
            get => username;
            set { username = value; OnPropertyChanged(""); }
        }
        public string Password
        {
            get => password;
            set { password = value; OnPropertyChanged(""); }
        }
        public bool IsBlocked
        {
            get => isBlocked;
            set { isBlocked = value; OnPropertyChanged(""); }
        }

        public Role Role
        {
            get => role;
            set { role = value; OnPropertyChanged(""); }
        }

    }
}