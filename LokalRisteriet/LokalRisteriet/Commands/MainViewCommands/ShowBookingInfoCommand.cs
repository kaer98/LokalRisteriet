using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LokalRisteriet.ViewModels;
using LokalRisteriet.Views;

namespace LokalRisteriet.Commands.MainViewCommands
{
    public class ShowBookingInfoCommand : ICommand
    {
        public bool CanExecute(object? parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }

        public event EventHandler? CanExecuteChanged;
    }

}
