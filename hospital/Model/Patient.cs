using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
    public class Patient : User, INotifyPropertyChanged
    {
        private string firstName;
        private string lastName;
        private string id;
        private string dateOfBirth;
        private string gender;
        private string phoneNumber;
        private bool isGuest;
        private string email;

        private int recordID;

        private List<DateTime> delayOrCancelAppointment;

        //public Appointment[] appointment;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public Patient() { }
        public Patient(string _username, string fName, string lName, string email, string patientId, string phone, string date, string gender, bool blocked)
        {
            Username = _username;
            FirstName = fName;
            LastName = lName;
            Email = email;
            id = patientId;
            phoneNumber = phone;
            dateOfBirth = date;
            isGuest = false;
            Gender = gender;
            IsBlocked = blocked;
        }


        public Patient(string firstName, string surname, string username)
        {
            //constructor for guest account
            FirstName = firstName;
            LastName = surname;
            Username = username;
            isGuest = true;
        }
        public string Id
        {
            get => id;
            set { id = value; OnPropertyChanged(""); }
        }
        public string Gender { get => gender; set { gender = value; OnPropertyChanged(""); } }
        public string Email { get => email; set { email = value; OnPropertyChanged(""); } }
        public string FirstName { get => firstName; set { firstName = value; OnPropertyChanged(""); } }
        public string LastName { get => lastName; set { lastName = value; OnPropertyChanged(""); } }
        public string DateOfBirth { get => dateOfBirth; set { dateOfBirth = value; OnPropertyChanged(""); } }
        public string PhoneNumber { get => phoneNumber; set { phoneNumber = value; OnPropertyChanged(""); } }
        public bool IsGuest { get => isGuest; set { isGuest = value; OnPropertyChanged(""); } }
        public string Username { get => base.Username; set { base.Username = value; OnPropertyChanged(""); } }
        public override string ToString()
        {
            return base.Username;
        }
        public int RecordId { get => recordID; set { recordID = value; OnPropertyChanged(""); } }

        public List<DateTime> DelayOrCancelAppointment
        {
            get
            {
                if (delayOrCancelAppointment != null)
                {
                    return delayOrCancelAppointment;
                }
                else
                {
                    delayOrCancelAppointment = new List<DateTime>();
                    return delayOrCancelAppointment;
                }
            }
            set => delayOrCancelAppointment = value;
        }
    }
}