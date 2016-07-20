using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Commons
{
    /// <summary>
    /// WindowButtons.xaml 的交互逻辑
    /// </summary>
    /// <summary>
    /// 封装了最小化、最大化及关闭三个窗口按钮。
    /// </summary>
    public partial class WindowButtons : UserControl, INotifyPropertyChanged
    {
        #region 依赖属性

        /// <summary>
        /// 获取或设置是否显示最小化按钮。
        /// </summary>
        public bool IsMinimizeEnable
        {
            get { return (bool)GetValue(IsMinimizeEnableProperty); }
            set { SetValue(IsMinimizeEnableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsMinimizeEnable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsMinimizeEnableProperty =
            DependencyProperty.Register("IsMinimizeEnable", typeof(bool), typeof(WindowButtons),
            new UIPropertyMetadata(true, (s, e) =>
            {
                WindowButtons wb = s as WindowButtons;
                wb.btnMinimize.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Collapsed;
            }));

        /// <summary>
        /// 获取或设置是否显示最大化按钮。
        /// </summary>
        public bool IsNormalMaxEnable
        {
            get { return (bool)GetValue(IsNormalMaxEnableProperty); }
            set { SetValue(IsNormalMaxEnableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsNormalMaxEnable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsNormalMaxEnableProperty =
            DependencyProperty.Register("IsNormalMaxEnable", typeof(bool), typeof(WindowButtons),
            new UIPropertyMetadata(true, (s, e) =>
            {
                WindowButtons wb = s as WindowButtons;
                wb.tbtnNormalMax.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Collapsed;
            }));


        /// <summary>
        /// 获取或设置窗口状态。
        /// </summary>
        public WindowState State
        {
            get { return (WindowState)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(WindowState), typeof(WindowButtons),
            new UIPropertyMetadata(WindowState.Normal, (s, e) =>
            {
                WindowButtons wb = s as WindowButtons;
                if ((WindowState)e.NewValue == WindowState.Normal)
                {
                    wb.tbtnNormalMax.IsChecked = false;
                }
                else if ((WindowState)e.NewValue == WindowState.Maximized)
                {
                    wb.tbtnNormalMax.IsChecked = true;
                }
            }));

        public IEnumerable PlugButtons
        {
            get { return (IEnumerable)GetValue(PlugButtonsProperty); }
            set { SetValue(PlugButtonsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlugButtons.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlugButtonsProperty =
            DependencyProperty.Register("PlugButtons", typeof(IEnumerable), typeof(WindowButtons),
            new UIPropertyMetadata(null, (s, e) =>
            {
                if (e.NewValue != null)
                {
                    WindowButtons wb = s as WindowButtons;
                    wb.ictlButtonHost.ItemsSource = e.NewValue as IEnumerable;
                }
            }));


        #endregion

        /// <summary>
        /// 获取或设置是否显示关闭按钮。
        /// </summary>
        public bool IsCloseEnable
        {
            get { return this.btnClose.Visibility == Visibility.Visible; }
            set { this.btnClose.Visibility = value ? Visibility.Visible : Visibility.Collapsed; }
        }

        /// <summary>
        /// 在单击最小化按钮时发生。
        /// </summary>
        public event RoutedEventHandler Minimize
        {
            add { this.btnMinimize.Click += value; }
            remove { this.btnMinimize.Click -= value; }
        }

        /// <summary>
        /// 在单击关闭按钮时发生。
        /// </summary>
        public event RoutedEventHandler Close
        {
            add { this.btnClose.Click += value; }
            remove { this.btnClose.Click -= value; }
        }

        /// <summary>
        /// 在单击最大化按钮退出最大化时发生。
        /// </summary>
        public event RoutedEventHandler Normalize
        {
            add { this.tbtnNormalMax.Unchecked += value; }
            remove { this.tbtnNormalMax.Unchecked -= value; }
        }

        /// <summary>
        /// 在单击最大化按钮时发生。
        /// </summary>
        public event RoutedEventHandler Maximize
        {
            add { this.tbtnNormalMax.Checked += value; }
            remove { this.tbtnNormalMax.Checked -= value; }
        }

        /// <summary>
        /// 属性更改事件，实现INotifyPropertyChanged接口。
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        Window HostWindow;

        /// <summary>
        /// 实例化一个WindowButtons类。
        /// </summary>
        public WindowButtons()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(WindowButtons_Loaded);
            this.btnMinimize.Click += (s, e) => this.State = WindowState.Minimized;
            // this.tbtnNormalMax.Checked += (s, e) => this.State = WindowState.Maximized;
            // this.tbtnNormalMax.Unchecked += (s, e) => this.State = WindowState.Normal;
            this.tbtnNormalMax.Checked += (s, e) =>
            {

                ChangeSizeToContent = SizeToContent.Manual;
                this.State = WindowState.Maximized;
            };
            this.tbtnNormalMax.Unchecked += (s, e) =>
            {
                this.State = WindowState.Normal;
                ChangeSizeToContent = SizeToContent;
            };
            this.btnClose.Click += (s, e) =>
            {
                if (HostWindow != null)
                {
                    HostWindow.Close();
                }
            };
        }

        void WindowButtons_Loaded(object sender, RoutedEventArgs e)
        {
            HostWindow = Window.GetWindow(this);
            if (HostWindow != null)
            {
                SizeToContent = HostWindow.SizeToContent;
            }
        }

        /// <summary>
        /// 触发指定属性的更改事件。
        /// </summary>
        /// <param name="propertyName">属性名。</param>
        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// 获取设置的内容适应的方式
        /// </summary>
        private SizeToContent SizeToContent = SizeToContent.Manual;

        /// <summary>
        /// 自定义属性，用于标记该窗体是否允许按内容适应，设此属性是为了解决最大化按钮当SizeToContent属性为WidthAndHeight时不能最大化，
        /// 从而最大、最小化必须变更SizeToContent的值的问题
        /// </summary>
        public SizeToContent ChangeSizeToContent
        {
            set
            {
                if (HostWindow != null)
                {
                    HostWindow.SizeToContent = value;
                }
            }
        }
    }
}
