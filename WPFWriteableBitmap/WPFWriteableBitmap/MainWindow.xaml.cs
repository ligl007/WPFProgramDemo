using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFWriteableBitmap
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        System.Windows.Threading.DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);   //间隔1秒
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            this.DataContext = this;
            this.Loaded += MainWindow_Loaded;
            //timer_Tick(null, null);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //XYScale xy = new XYScale();
            //xy.Width = 1024;
            //xy.Height = 768;
            //gvContent.Children.Add(xy);
        }

        MinuteQuoteViewModel _viewModel = new MinuteQuoteViewModel();
        public MinuteQuoteViewModel ViewModel
        {
            get
            {
                return _viewModel;
            }

            set
            {
                _viewModel = value;
                OnPropertyChanged("ViewModel");
            }
        }
        int prices = 1024;
        Random r = new Random();
        int x = 1;
        void timer_Tick(object sender, EventArgs e)
        {

            Dispatcher.Invoke(new Action(() =>
            {
                MinuteQuoteViewModel tmp = new MinuteQuoteViewModel();
                //tmp.LastPx = r.Next(1, 500);
                //tmp.Ordinal = r.Next(1,prices+1);
                tmp.LastPx = x;
                tmp.Ordinal = x;
                x++;
                ViewModel = tmp;
            }));
            if (this.prices < x)
            {
                x = 1;
            }
            //timer.Stop();
        }

        private void btn_printScreen_Click(object sender, RoutedEventArgs e)
        {


        }

        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    public class XYScale : FrameworkElement
    {
        protected override void OnRender(DrawingContext drawingContext)
        {
            //设置坐标系方向成底部为X Y轴的原点
            drawingContext.PushTransform(new ScaleTransform(1, -1, 0, RenderSize.Height / 2));
            System.Windows.Media.Pen pen = new System.Windows.Media.Pen();
            pen.Thickness = 1;
            pen.Brush = new SolidColorBrush(Colors.White);
            drawingContext.DrawLine(pen, new Point(0, 500), new Point(500, 500));
        }
    }
}
