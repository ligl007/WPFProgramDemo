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

namespace CoordinateXY
{
    /// <summary>
    /// UserControlXY.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlXY : UserControl
    {
        public UserControlXY()
        {
            InitializeComponent();

            this.Loaded += UserControlXY_Loaded;

        }

        private void UserControlXY_Loaded(object sender, RoutedEventArgs e)
        {
            InitXRuler();
            InitYRuler();
        }

        #region 变量
        /// <summary>
        /// 放大倍数 防止坐标尺子重叠
        /// </summary>
        private static double scaleNumX = 0;

        /// <summary>
        /// 放大倍数 防止坐标尺子重叠
        /// </summary>
        private static double scaleNumY = 0;

        /// <summary>
        /// 按照宽度和高度计算放大倍数
        /// </summary>
        private double scaleStandard = 50;

        /// <summary>
        /// x坐标尺度
        /// </summary>
        private double xTotal = 150;

        /// <summary>
        /// Y坐标尺度
        /// </summary>
        private double yTotal = 300;

        /// <summary>
        ///  刻度间隔 10刻度显示一个网格线
        /// </summary>
        private double scaleInterval = 10;

        /// <summary>
        /// 网格刻度线延长出来的长度值
        /// 修改此长度看效果图
        /// </summary>
        private int xyLine = 0;

        /// <summary>
        /// xy坐标线长比网格绘制长度长多少
        /// </summary>
        private int xyShorten = 50;

        /// <summary>
        /// 文本距离xy坐标线的位置
        /// </summary>
        private int txtDis = 20;

        /// <summary>
        /// 宽度
        /// </summary>
        private double _xWidth;

        /// <summary>
        /// 高度
        /// </summary>
        private double _yHeight;

        /// <summary>
        /// 高度
        /// </summary>
        public double YHeight
        {
            get
            {
                return _yHeight;
            }

            set
            {
                _yHeight = value;
                this.Height = value;
                //预留100的line长度  
                scaleNumY = (value - xyShorten) / scaleStandard / (yTotal / scaleStandard);
            }
        }

        /// <summary>
        /// 宽度
        /// </summary>
        public double XWidth
        {
            get
            {
                return _xWidth;
            }

            set
            {
                _xWidth = value;
                this.Width = value;
                //预留100的line长度  
                scaleNumX = (value - xyShorten) / scaleStandard / (xTotal / scaleStandard);
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 初始化X坐标尺
        /// </summary>
        private void InitXRuler()
        {
            canvasXRuler.Children.Clear();
            var xtotal = xTotal + 1;
            for (int i = 1; i < xtotal; i++)
            {
                if (i % scaleInterval != 0 && i + 1 != xtotal)
                {
                    continue;
                }

                Line xLine = new Line();
                xLine.X1 = 1;
                xLine.X2 = 0;
                xLine.Y1 = 0;
                xLine.Y2 = this.Height - xyShorten + xyLine;//柱状线图形高度;
                xLine.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 255));//蓝色
                xLine.StrokeThickness = 1;
                xLine.IsHitTestVisible = false;
                Canvas.SetLeft(xLine, i * scaleNumX);
                Canvas.SetBottom(xLine, -xyLine);//延迟8长度刻度
                TextBlock txtBlock = new TextBlock();
                txtBlock.Text = (i).ToString();//文本内容
                Canvas.SetLeft(txtBlock, i * scaleNumX - 8);//两位数的文本平移8 让文本居中显示
                Canvas.SetBottom(txtBlock, -txtDis);//刻度下方文本
                canvasXRuler.Children.Add(xLine);
                canvasXRuler.Children.Add(txtBlock);
            }

        }

        /// <summary>
        /// 初始化Y坐标尺
        /// </summary>
        private void InitYRuler()
        {
            canvasYRuler.Children.Clear();
            var ytotal = yTotal + 1;
            for (int i = 1; i < ytotal; i++)
            {
                if (i % scaleInterval != 0 && i + 1 != ytotal)
                {
                    continue;
                }
                Line yLine = new Line();
                yLine.X1 = 1;
                yLine.X2 = this.Width - xyShorten + xyLine;//柱状线图形长度;
                yLine.Y1 = 0;
                yLine.Y2 = 0;
                yLine.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 255));//蓝色
                yLine.StrokeThickness = 1;
                yLine.IsHitTestVisible = false;
                Canvas.SetLeft(yLine, -xyLine);//刻度值
                Canvas.SetBottom(yLine, i * scaleNumY);
                TextBlock txtBlock = new TextBlock();
                txtBlock.Text = (i).ToString();//文本内容
                Canvas.SetLeft(txtBlock, -txtDis - 2);
                Canvas.SetBottom(txtBlock, i * scaleNumY - 8);//两位数的文本平移8 让文本居中显示
                canvasXRuler.Children.Add(yLine);
                canvasXRuler.Children.Add(txtBlock);
            }
        }

        private static UserControlXY uControlXY;

        /// <summary>
        /// 创建点的位置
        /// </summary>
        /// <param name="point"></param>
        static void InCanvasPoint(Point point)
        {
            var temp = CreatePointEllipse();
            //temp.ToolTip = point.X / scaleNumX + "," + point.Y / scaleNumY;
            temp.ToolTip = point.Y / scaleNumX + "," + point.X / scaleNumY + "  " + "(" + point.Y + "," + point.X + ")";
            uControlXY.canvasLinePoint.Children.Add(temp);
            Panel.SetZIndex(temp, 100);
            Canvas.SetLeft(temp, point.X - temp.Height / 2);
            Canvas.SetTop(temp, point.Y - temp.Width / 2);
        }

        /// <summary>
        /// 创建Point
        /// </summary>
        static void CreatePoint(List<Point> itemList)
        {
            if (itemList != null && itemList.Count > 0)
            {
                for (int i = 0; i < itemList.Count; i++)
                {
                    var startPoint = itemList[i];
                    var tmpPoint = ConvertPoint(startPoint);
                    InCanvasPoint(tmpPoint);
                    if (i + 1 == itemList.Count)
                    {
                        break;
                    }
                    var endPoint = itemList[i + 1];
                    var tmpEndPoint = ConvertPoint(endPoint);
                    CreateLine(tmpPoint, tmpEndPoint);
                }
            }
        }

        /// <summary>
        /// 创建连接的直线
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        static void CreateLine(Point startPoint, Point endPoint)
        {
            PathGeometry pg = new PathGeometry();//组合绘制的线段 
            Path pa = new Path();//绘制轨迹曲线的容器，用于显示 
            pa.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            pa.StrokeThickness = 1;
            PathFigure pf = new PathFigure();
            pf.StartPoint = startPoint;
            LineSegment line = new LineSegment();
            line.Point = endPoint;
            pf.Segments.Add(line);
            pg.Figures.Add(pf);
            pa.Data = pg;
            uControlXY.canvasLinePoint.Children.Add(pa);
        }

        /// <summary>
        /// 创建弧线
        /// </summary>
        static void CreateArcLine(Tuple<Point, Point, double> data)
        {
            if (data == null)
            {
                return;
            }
         
            Point startPoint = ConvertPoint(data.Item1);
            Point endPoint = ConvertPoint(data.Item2);
            CreateLine(startPoint, endPoint);
            PathGeometry pg = new PathGeometry();//组合绘制的线段 
            Path pa = new Path();//绘制轨迹曲线的容器，用于显示 
            pa.ToolTip = data.Item1 + "  " + data.Item2;
            //pa.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            pa.Stroke = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            pa.StrokeThickness = 1;
            PathFigure pf = new PathFigure();
            pf.StartPoint = startPoint;
            ArcSegment line = new ArcSegment();
            line.SweepDirection = SweepDirection.Clockwise;//顺时针弧
            line.Point = endPoint;

            //半径 正弦定理a/sinA=2r r=a/2sinA 其中a指的是两个城市点之间的距离 角A指a边的对角
            double sinA = Math.Sin(Math.PI * data.Item3 / 180.0);
            //计算距离 勾股定理
            double x = startPoint.X - endPoint.X;
            double y = startPoint.Y - endPoint.Y;
            double aa = x * x + y * y;
            double l = Math.Sqrt(aa);
            double r = l / (sinA * 2);
            line.Size = new Size(r, r);
            pf.Segments.Add(line);
            pg.Figures.Add(pf);
            pa.Data = pg;
            uControlXY.canvasLinePoint.Children.Add(pa);
        }

        /// <summary>
        /// 把坐标转换为绘画坐标
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        static Point ConvertPoint(Point point)
        {
            var tmpPoint = new Point();
            tmpPoint.X = point.Y * scaleNumY;
            tmpPoint.Y = point.X * scaleNumX;
            return tmpPoint;
        }

        /// <summary>
        /// 创建圆点
        /// </summary>
        /// <returns></returns>
        static Ellipse CreatePointEllipse()
        {
            Ellipse ell = new Ellipse();
            ell.Stroke = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            ell.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            ell.Height = 8;
            ell.Width = 8;
            return ell;
        }

        public void Refresh(List<Point> _itemsSource)
        {
            canvasLinePoint.Children.Clear();
            CreatePoint(_itemsSource);
            InitXRuler();
            InitYRuler();

        }

        public void ClearLine()
        {
            this.canvasLinePoint.Children.Clear();
        }
        #endregion

        #region Customer DependencyObject

        /// <summary>
        /// 求两点之间的弧线
        /// item1 开始坐标 item2 结束坐标 item3 弧度值
        /// </summary>
        public Tuple<Point, Point, double> PointArc
        {
            get { return (Tuple<Point, Point, double>)GetValue(PointArcProperty); }
            set { SetValue(PointArcProperty, value); }
        }
        public List<Point> ItemsSource
        {
            get { return (List<Point>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(List<Point>), typeof(UserControlXY), new PropertyMetadata(null, new PropertyChangedCallback(OnItemsSourceChangedCallback)));
        public static void OnItemsSourceChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                uControlXY = d as UserControlXY;
                CreatePoint(e.NewValue as List<Point>);
            }
        }


        // Using a DependencyProperty as the backing store for PointArc.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointArcProperty =
            DependencyProperty.Register("PointArc", typeof(Tuple<Point, Point, double>), typeof(UserControlXY), new PropertyMetadata(null, new PropertyChangedCallback(OnPointArcChangedCallback)));


        public static void OnPointArcChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                uControlXY = d as UserControlXY;
                CreateArcLine(e.NewValue as Tuple<Point, Point, double>);
            }
        }
        #endregion
    }
}
