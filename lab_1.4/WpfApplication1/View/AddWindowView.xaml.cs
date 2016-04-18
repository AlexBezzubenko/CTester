using System;
using System.Windows;
using TestApp.ViewModel;
using TestApp.Model;

namespace TestApp.View
{
    /// <summary>
    /// Логика взаимодействия для add_window.xaml
    /// </summary>
    public partial class AddWindowView : Window
    {
        private TestModel test;
        public TestModel Test
        {
            get { return test;}
            set { test = value; }
        }
        public AddWindowView(TestModel test)
        {
            InitializeComponent();
            DataContext = new AddWindowViewModel(this);
            this.test = test;
        }   
    }
}
