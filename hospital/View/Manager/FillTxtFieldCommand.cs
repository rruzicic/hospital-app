using System.Windows.Controls;

namespace hospital.View.Manager
{
    public class FillTxtFieldCommand : IDemoCommand
    {
        private readonly TextBox box;
        private readonly string text;
        public FillTxtFieldCommand(TextBox box, string text)
        {
            this.box = box;
            this.text = text;
        }
        public void execute()
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                box.Text = text;
            });
        }
    }
}
