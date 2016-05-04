using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using CozyFarseer.TopList.Model;
using CozyFarseer.TopList.Network;
using RestSharp;
using System.Windows;

namespace CozyFarseer.TopList.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        ObservableCollection<FarseerNodeViewModel> _farseerNodeList = new ObservableCollection<FarseerNodeViewModel>();
        public ObservableCollection<FarseerNodeViewModel> FarseerNodeList
        {
            get
            {
                return _farseerNodeList;
            }
            set
            {
                Set("FarseerNodeList", ref value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            TopListUpdate.Last(OnContentUpdate);
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

        public string Title { get; } = "CozyFarseer";

        private void OnContentUpdate(IRestResponse<FarseerTopList> resp)
        {
            if(resp.ResponseStatus == ResponseStatus.Completed)
            {
                var result = resp.Data;
                if(result.code == 0)
                {
                    Application.Current.Dispatcher.Invoke(()=> 
                    {
                        foreach (var obj in result.ret.list)
                        {
                            _farseerNodeList.Add(new FarseerNodeViewModel(obj));
                        }
                    });
                }
                else
                {
                    //TODO Failed
                }
            }
            else if(resp.ResponseStatus == ResponseStatus.TimedOut)
            {
                //TODO timeout
            }
        }
    }
}