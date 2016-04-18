using System;
using System.Windows.Input;
using TestApp.Model;
using TestApp.View;
using TestApp.ViewModel;

namespace TestApp.Commands.Main
{
    public class StartTestCommand : ICommand
    {
        private TestModel test;
        public StartTestCommand(TestModel test)
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

        public void Execute(object parametr)
        {
            TestWindowView testWindow = new TestWindowView(test);
            testWindow.DataContext = new TestWindowViewModel(testWindow, test); 
            testWindow.ShowDialog();                       
        }  
    }
}