using System;
using System.Windows.Input;
using TestApp.Model;
using TestApp.View;

namespace TestApp.ViewModel
{
    class AddWindowViewModel
    {       
            public AddWindowViewModel(AddWindowView addWindow)
            {              
                AddCommand = new Commands.Add.AddCommand(addWindow);
                ReturnCommand = new Commands.ReturnCommand(addWindow);
            }
    
            public ICommand AddCommand { get; set; }
            public ICommand ReturnCommand { get; set; }                               
    }
}
