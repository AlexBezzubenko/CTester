using System;
using System.Windows.Input;
using TestApp.Model;
using TestApp.View;
using System.Windows.Controls;

namespace TestApp.ViewModel
{
    class TestWindowViewModel
    {
            Question question;
            public TestWindowViewModel(TestWindowView testWindow, TestModel test)
            {              
                AcceptCommand = new Commands.View.AcceptCommand(testWindow, test);
                ReturnCommand = new Commands.ReturnCommand(testWindow);
            }

            public TestWindowViewModel(TestWindowView testWindow, Question question)
            {              
                CheckBox[] boxes;
                
                testWindow.accept_button.Visibility = System.Windows.Visibility.Hidden;
                boxes = new CheckBox[] { testWindow.checkbox1, testWindow.checkbox2, testWindow.checkbox3, 
                                         testWindow.checkbox4, testWindow.checkbox5 };
                testWindow.accept_button.Visibility = System.Windows.Visibility.Hidden;

                this.question = question;
                int i = 0;
                foreach (Answer answer in question)
                {
                    boxes[i].Content = answer;
                    if (answer.Correct)
                    {
                        boxes[i].IsChecked = true;
                        boxes[i].Visibility = System.Windows.Visibility.Visible;
                    }
                    i++;
                }
                for (int j = i; j < 5; j++)
                {
                    boxes[j].Visibility = System.Windows.Visibility.Hidden;
                }
                testWindow.label1.Content = question;   

                ReturnCommand = new Commands.ReturnCommand(testWindow);
            }
    
            public ICommand AcceptCommand { get; set; }
            public ICommand ReturnCommand { get; set; }                               
    }
}
