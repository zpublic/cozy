using CozySql.Exe.Commands;
using CozySql.Exe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CozySql.Exe.Models
{
    public class UIControlInfo
    {
        public string Title { get; set; }
        public UserControl Content { get; set; }
        public MainFrameViewModel Parent { get; set; }
        private ICommand closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                return closeCommand = closeCommand ?? new DelegateCommand(x =>
                {
                    Parent.CloseTab(this);
                });
            }
        }
    }
}
