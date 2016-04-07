using Common.Logger;
using MMS.UI.Common;
using MMS.UI.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CozyThunder.Client
{
    public class DownloadViewModel : BaseINotifyPropertyChanged
    {
        private static DownloadViewModel mDownload;
        private static readonly Logger mLog = Logger.GetInstance(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly object syncRoot = new object();

        private DownloadViewModel()
        {
            this.InitializePoint();
            this.DownloadButton = new DownloadButton();
        }

        public static DownloadViewModel GetInstance()
        {
            if (mDownload == null)
            {
                lock (syncRoot)
                {
                    if (mDownload == null)
                    {
                        mDownload = new DownloadViewModel();
                    }
                }
            }
            return mDownload;
        }

        private List<Point> mPoints = new List<Point>();
        public List<Point> Points { get { return this.mPoints; } set { this.mPoints = value; OnPropertyChanged("Points"); } }

        private string mAddress = String.Empty;
        public string Address { get { return this.mAddress; } set { this.mAddress = value; OnPropertyChanged("Address"); } }

        private bool mIsReadOnly = false;
        public bool IsReadOnly { get { return this.mIsReadOnly; } set { this.mIsReadOnly = value; OnPropertyChanged("IsReadOnly"); } }

        public DownloadButton DownloadButton { get; set; }

        public int PointCount = 356;

        public void InitializePoint()
        {
            try
            {
                mLog.Info("Start initialize point.point count:{0}", this.PointCount.ToString());
                List<Point> temp = new List<Point>();
                for (int i = 0; i <= this.PointCount; i++)
                {
                    Point p = new Point()
                    {
                        BackgroundColor = "#245678",
                        Height = 20,
                        Width = 20
                    };
                    temp.Add(p);
                }
                this.Points = temp;
            }
            catch (Exception e)
            {
                mLog.Error("An error has occurred in the initialize point,error:{0}", e.ToString());
                throw;
            }
        }

        public void UpdateAllPoint(List<Point> poins)
        {
            this.Points = poins;
        }

        public void UpdatePointByIndex(int index, string backgroundColor = "#245678", int width = 20, int height = 20)
        {
            if (index < this.Points.Count)
            {
                Point point = this.Points[index];
                point.Width = width;
                point.Height = height;
                point.BackgroundColor = backgroundColor;
            }
            else
            {
                mLog.Warn("Start warn has occurred in the update point by index,error:index out of bounds.");
            }
        }
    }
}
