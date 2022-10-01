using hospital.View.Manager;
using System;

namespace hospital.View
{
    public class ActionExecuteCommand : IDemoCommand
    {
        private readonly Action function;
        public ActionExecuteCommand(Action function)
        {
            this.function = function;
        }
        public void execute()
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                function();
            });
        }
    }
}
