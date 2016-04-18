using System;
using System.Windows.Input;
using TestApp.Model;
using TestApp.View;
using System.Windows;
using System.Linq;


namespace TestApp.Commands.Main
{
    public class QueuryCommand : ICommand
    {
        TestModel test;
        QueuryMethods queurymethods;
        

        public QueuryCommand(TestModel test)
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
            var values = (object[])parametr;
            var index = (int)values[0];
            var string1 = (string)values[1];

            queurymethods = new QueuryMethods(test, string1); 

            if (index == -1)
            {
                MessageBox.Show("Nothing is Selected");
                return;
            }

            if (queurymethods.foos.Length - 1 >= index)
            {
                queurymethods.foos[index](/*this.test*/);
            }
        }

        
    }
}