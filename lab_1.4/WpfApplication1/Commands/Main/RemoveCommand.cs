using System;
using System.Windows.Input;
using TestApp.Model;

namespace TestApp.Commands.Main
{
    public class RemoveCommand : ICommand
    {     
        private TestModel test;
        public RemoveCommand(TestModel test)
        {
            this.test = test;
        }

        public bool CanExecute(object parameter)
        {         
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            if (parameter == null)
            {
                return;
            }
            Question q = (Question)parameter;

            test.Remove(q);      
        }  
    }
}