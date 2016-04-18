using System;
using System.Windows.Input;
using System.Windows.Data;
using System.Windows.Media;
using TestApp.Model;
using TestApp.View;
 
using System.ComponentModel;
using System.Collections.Generic;

namespace TestApp.ViewModel
{
    public class MainWindowViewModel
    {       
            public MainWindowViewModel()
            {
                Test = new TestModel();
                Test.ReadFromFile();
                AddCommand = new Commands.Main.AddCommand();
                SelectCommand = new Commands.Main.SelectCommand();
                ViewCommand = new Commands.Main.ViewCommand();
                RemoveCommand = new Commands.Main.RemoveCommand(Test);
                TestCommand = new Commands.Main.StartTestCommand(Test);
                QueuryCommand = new Commands.Main.QueuryCommand(Test);                
            }

            public TestModel Test { get; set; }

            public ICommand QueuryCommand { get; set; }
            public ICommand AddCommand { get; set; }
            public ICommand SelectCommand { get; set; }
            public ICommand ViewCommand { get; set; }
            public ICommand RemoveCommand { get; set; }
            public ICommand TestCommand { get; set; }                    
    }
}
