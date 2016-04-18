using System;
using System.Windows.Input;
using TestApp.Model;
using TestApp.View;
using TestApp.ViewModel;

namespace TestApp.Commands.Main
{
    public class ViewCommand : ICommand
    {     
        public ViewCommand()
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

        public void Execute(object parameter)
        {
            if (parameter == null)
            {
                return;
            }
            Question question = (Question)parameter;
            TestWindowView viewWindow = new TestWindowView(question);          
            viewWindow.DataContext = new TestWindowViewModel(viewWindow, question);         
            viewWindow.ShowDialog();
        }
    }
}