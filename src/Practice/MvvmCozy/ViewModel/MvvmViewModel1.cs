using GalaSoft.MvvmLight;
using System.Windows;

namespace MvvmCozy.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MvvmViewModel1 : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MvvmViewModel1 class.
        /// </summary>
        public MvvmViewModel1()
        {
        }

        public string Title
        {
            get
            {
                return "MVVM";
            }
        }

        public string SubTitle
        {
            get
            {
                return "原来有一丝心痛叫做无奈";
            }
        }
    }
}