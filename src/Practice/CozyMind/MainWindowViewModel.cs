using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CozyMind
{
    public class MainWindowViewModel
    {
        public PlotModel myPlotModel { get; private set; }
        public PlotController Controller { get; private set; }

        public MainWindowViewModel()
        {
            this.Controller = new PlotController();
            myPlotModel = new PlotModel { Title = "Example 1" };
            myPlotModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));

            var arrowAnnotation = new ArrowAnnotation
            {
                StartPoint = new DataPoint(0, 0),
                EndPoint = new DataPoint(3, 1)
            };
            myPlotModel.Annotations.Add(arrowAnnotation);
        }
    }
}
