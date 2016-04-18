using System;
using System.Windows;
using TestApp.ViewModel;
using TestApp.Model;

namespace TestApp.View
{
    /// <summary>
    /// Логика взаимодействия для test_window.xaml
    /// </summary>
    public partial class TestWindowView : Window
    {       
        public TestWindowView(TestModel test)
        {
            InitializeComponent();                   
        }
        public TestWindowView(Question question)
        {
            InitializeComponent();                          
        }          
    }
}
