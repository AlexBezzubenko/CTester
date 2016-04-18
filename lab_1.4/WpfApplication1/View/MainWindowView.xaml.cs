using System;
using System.Windows;
using TestApp.ViewModel;
 
namespace TestApp.View
{     
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {        
            InitializeComponent();
            
        }

        protected override void OnClosed(EventArgs e)
        {
            var viewModel = DataContext as MainWindowViewModel;
            if (viewModel != null)
            {
                viewModel.Test.SaveToFile();
            }
            base.OnClosed(e);
        }          
    }
}
