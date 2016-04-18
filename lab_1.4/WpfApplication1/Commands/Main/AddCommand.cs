using System;
using System.Windows.Input;
using TestApp.Model;
using TestApp.View;

namespace TestApp.Commands.Main
{
    public class AddCommand : ICommand
    {     
        public AddCommand()
        {
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

        public void Execute(object parametr)
        {
            var test = parametr as TestModel;
            if (test != null)
            {
                AddWindowView addWindow = new AddWindowView(test);
                addWindow.ShowDialog();
            }                                  
        }  
    }
}