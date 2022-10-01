using hospital.View.Manager;
using System;
using System.Windows.Input;

namespace hospital.VM
{
    public class ReviewPageViewModel
    {
        public ICommand DoctorReviewCommand => new DoctorReviewCommand();
        public ICommand HospitalReviewCommand => new HospitalReviewCommand();
        public ReviewPageViewModel()
        {

        }
    }

    public class DoctorReviewCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            new DoctorReviewWindow().Show();
        }
    }

    public class HospitalReviewCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            new HospitalReviewWindow().Show();
        }
    }
}
