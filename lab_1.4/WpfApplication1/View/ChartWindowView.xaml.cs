using System;
using System.Windows;
using TestApp.ViewModel;
using TestApp.Model;
using System.Collections.Generic;

namespace TestApp.View
{
    /// <summary>
    /// Логика взаимодействия для chartWindow.xaml
    /// </summary>
    public partial class ChartWindowView : Window
    {       
        public ChartWindowView(IEnumerable<Question> test, int mode)
        {
            InitializeComponent();
            DataContext = new ChartWindowViewModel(this, test, mode);   
        }              
    }
}
