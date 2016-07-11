using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace CoordinateXY
{
    /// <summary>
    /// UserControlShow.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlShow : UserControl
    {
        ViewMode vModel = new ViewMode();
        public UserControlShow()
        {
            InitializeComponent();
            this.DataContext = vModel;
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            //uControlXY.Width = uControlXY.Width * 1.1;
            //uControlXY.Height = uControlXY.Height * 1.1;
            var txt = txtboxWH.Text.Trim();
            string[] whs = txt.Split(',');
            if (whs.Length != 2)
            {
                return;
            }
            double w;
            double h;
            double.TryParse(whs[0], out w);
            double.TryParse(whs[1], out h);
            if (w != 0 && h != 0)
            {
                this.uControlXY.XWidth = w;
                this.uControlXY.YHeight = h;
                this.uControlXY.Refresh(vModel.XyList);

            }
        }

        private void BtnArc_Click(object sender, RoutedEventArgs e)
        {
            var spoints = txtboxArcSpoint.Text.Trim().Split(',');
            if (spoints.Length != 2)
            {
                return;
            }
            Point startPoint = new Point();
            double sX;
            double sY;
            double.TryParse(spoints[0], out sX);
            double.TryParse(spoints[1], out sY);
            startPoint.X = sX;
            startPoint.Y = sY;

            var epoints = txtboxArcEpoint.Text.Trim().Split(',');
            if (epoints.Length != 2)
            {
                return;
            }

            Point endPoint = new Point();
            double eX;
            double eY;
            double.TryParse(epoints[0], out eX);
            double.TryParse(epoints[1], out eY);
            endPoint.X = eX;
            endPoint.Y = eY;

            var angletxt = txtboxAngle.Text.Trim();
            double angle;
            double.TryParse(angletxt, out angle);
            vModel.ArcData = new Tuple<Point, Point, double>(startPoint, endPoint, angle);

        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            uControlXY.ClearLine();
        }
    }

    public class ViewMode : INotifyPropertyChanged
    {
        public ViewMode()
        {
            _xyList = new List<Point>();
            XyList.Add(new Point(10, 10));
            XyList.Add(new Point(40, 50));
            XyList.Add(new Point(30, 40));
            XyList.Add(new Point(90, 10));
            XyList.Add(new Point(20, 90));
            XyList.Add(new Point(45.5, 73.2));
            XyList.Add(new Point(140, 235));
            _arcData = new Tuple<Point, Point, double>(new Point(50, 50), new Point(80, 90), 60);

        }

        private Tuple<Point, Point, double> _arcData;


        private List<Point> _xyList;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        public List<Point> XyList
        {
            get
            {
                return _xyList;
            }

            set
            {
                _xyList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("XyList"));
            }
        }

        /// <summary>
        /// 弧线构成数据
        /// </summary>
        public Tuple<Point, Point, double> ArcData
        {
            get
            {
                return _arcData;
            }

            set
            {
                _arcData = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ArcData"));
            }
        }
    }
}
