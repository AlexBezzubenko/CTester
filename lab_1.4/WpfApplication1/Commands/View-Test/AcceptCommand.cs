using System;
using System.Windows;
using System.Windows.Input;
using TestApp.View;
using System.Windows.Controls;
using TestApp.Model;

namespace TestApp.Commands.View
{
    public class AcceptCommand : ICommand
    {
        private TestWindowView testWindow;
        private Question question;      
        private CheckBox[] boxes;
        private int current_question = 0;
        private int test_result;
        private TestModel test;

        public AcceptCommand(TestWindowView testWindow, TestModel test)
        {
            this.testWindow = testWindow;
            boxes = new CheckBox[] { testWindow.checkbox1, testWindow.checkbox2, testWindow.checkbox3,
                                     testWindow.checkbox4, testWindow.checkbox5 };
            this.test = test;
            update_elements();
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
            int i = 0;
            int wrong_count = 0;
            if (question != null)
            {
                foreach (Answer answer in question)
                {                 
                    if ((answer.Correct && boxes[i].IsChecked == false)
                        || (!answer.Correct && boxes[i].IsChecked == true))
                    {
                        wrong_count++;
                    }
                    i++;
                }
                MessageBox.Show("Fail = " + question.FailStats.ToString() + "\n"
                       + "Success = " + question.SuccessStats.ToString());
                if (wrong_count == 0)
                {
                    test_result++;
                    ++question.SuccessStats;
                }
                else
                {
                    ++question.FailStats;
                }
                if (test.Count == current_question + 1)
                {
                    MessageBox.Show(test_result.ToString() + " of "
                                    + test.Count.ToString() +
                                    " correct answers");
                    testWindow.Close();
                    return;
                }
                MessageBox.Show("Fail = " + question.FailStats.ToString() + "\n"
                        + "Success = " + question.SuccessStats.ToString());
                current_question++;
                update_elements();
            }               
        }
        private void update_elements()
        {

            this.question = test[current_question];

            if (question != null)
            {
                int i = 0;
                foreach (Answer answer in question)
                {
                    boxes[i].Content = answer;
                    boxes[i].Visibility = System.Windows.Visibility.Visible;
                    boxes[i].IsChecked = false;
                    i++;
                }
                for (int j = i; j < 5; j++)
                {
                    boxes[j].Visibility = System.Windows.Visibility.Hidden;
                }
                testWindow.label1.Content = question;
            }
        }
    }
}