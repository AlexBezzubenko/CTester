using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using TestApp.Model;
using TestApp.View;

namespace TestApp.Commands.Add
{
    public class AddCommand : ICommand
    {
        private AddWindowView addWindow;

        public AddCommand(AddWindowView addWindow)
        {
            this.addWindow = addWindow;
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
            Question question = new Question();

            CheckBox[] Cboxes = new CheckBox[] { addWindow.checkbox1, addWindow.checkbox2, addWindow.checkbox3,
                                                 addWindow.checkbox4, addWindow.checkbox5 };
            TextBox[] Tboxes = new TextBox[] { addWindow.textbox0, addWindow.textbox1, addWindow.textbox2, addWindow.textbox3, 
                                               addWindow.textbox4, addWindow.textbox5 };
            question.Text = Tboxes[0].Text;
            
            for (int i = 0; i < 5; i++)
            {
                if (Tboxes[i + 1].Text != "")
                {
                    bool Correct = (bool)Cboxes[i].IsChecked;
                    question.Answers.Add(new Answer(Tboxes[i + 1].Text, Correct));
                }
            }
            if (question.Text != String.Empty)
            {            
                addWindow.Test.Add(question);                               
            }
            addWindow.Close();
        }  
    }
}