using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyMabi.WpfExe.Command;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows;

namespace CozyMabi.WpfExe.ViewModel.UserControlsViewModel
{
    public class BubbleViewModel : BaseViewModel
    {
        #region Property

        private string _Bubble;
        public string Bubble
        {
            get
            {
                return _Bubble;
            }
            set
            {
                Set(ref _Bubble, value, "Bubble");
            }
        }

        #endregion

        #region Command

        private ICommand _SubmitBubble;
        public ICommand SubmitBubble
        {
            get
            {
                return _SubmitBubble = _SubmitBubble ?? new DelegateCommand(x =>
                {
                    MessageBox.Show(Bubble);
                });
            }
        }

        #endregion

        public BubbleViewModel()
        {
            this.PropertyChanged += PropertyChangedEvent;
        }

        private void PropertyChangedEvent(object sender, PropertyChangedEventArgs e)
        {

        }
    }
}
