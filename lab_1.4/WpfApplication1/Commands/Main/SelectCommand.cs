using System;
using System.Windows.Input;
using TestApp.Model;
using TestApp.View;

namespace TestApp.Commands.Main
{
    public class SelectCommand : ICommand
    {     
        public SelectCommand()
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
            /*var answer = (Answer)answerBox.SelectedItem;
            if (answer != null)
            {
                correct_label.Content = answer.Correct.ToString();
                correct_label.Foreground = (answer.Correct) ? Brushes.Green : Brushes.Red;
            }
            else
            {
                correct_label.Content = "";
            }
        }*/
        }  
    }
}