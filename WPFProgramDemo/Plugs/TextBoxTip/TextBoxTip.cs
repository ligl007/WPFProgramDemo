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

namespace TipPlugs
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:TextBoxTip"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:TextBoxTip;assembly=TextBoxTip"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误: 
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:TextBoxTip/>
    ///
    /// </summary>
    public class TextBoxTip : TextBox
    {
        static TextBoxTip()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBoxTip), new FrameworkPropertyMetadata(typeof(TextBoxTip)));
            InitializeCommands();
        }



        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            string text = this.Text;
            if (!string.IsNullOrEmpty(text) && IsShowXButton)
            {
                XButtonVisibility = Visibility.Visible;
            }
            //else if (string.IsNullOrEmpty(text))
            //{
            //    XButtonVisibility = Visibility.Collapsed;
            //}
            base.OnTextChanged(e);
        }

        /// <summary>
        /// 提示文本，如：请输入用户名
        /// </summary>
        public string TooTipText
        {
            get { return (string)GetValue(TooTipTextProperty); }
            set { SetValue(TooTipTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TootipText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TooTipTextProperty =
            DependencyProperty.Register("TooTipText", typeof(string), typeof(TextBoxTip), new UIPropertyMetadata("", null));


        /// <summary>
        /// 三个像素描边颜色
        /// </summary>
        public Brush TopBrush
        {
            get { return (Brush)GetValue(TopBrushProperty); }
            set { SetValue(TopBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TopBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopBrushProperty =
            DependencyProperty.Register("TopBrush", typeof(Brush), typeof(TextBoxTip), new UIPropertyMetadata(null));


        /// <summary>
        /// 控制显示X按钮
        /// </summary>
        public Visibility XButtonVisibility
        {
            get { return (Visibility)GetValue(XButtonVisibilityProperty); }
            set { SetValue(XButtonVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for XButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XButtonVisibilityProperty =
            DependencyProperty.Register("XButtonVisibility", typeof(Visibility), typeof(TextBoxTip),
            new UIPropertyMetadata(Visibility.Collapsed));


        /// <summary>
        ///  是否显示X按钮
        /// </summary>
        public bool IsShowXButton
        {
            get { return (bool)GetValue(IsShowXButtonProperty); }
            set { SetValue(IsShowXButtonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsShowXButton.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowXButtonProperty =
            DependencyProperty.Register("IsShowXButton", typeof(bool), typeof(TextBoxTip), new UIPropertyMetadata(false));


        /// <summary>
        /// Since we're using RoutedCommands for the increase/decrease commands, we need to
        /// register them with the command manager so we can tie the events
        /// to callbacks in the control.
        /// </summary>
        private static void InitializeCommands()
        {
            XButtonCommand = new RoutedCommand("XButtonCommand", typeof(TextBoxTip));
            CommandManager.RegisterClassCommandBinding(typeof(TextBoxTip),
                  new CommandBinding(XButtonCommand, CloseButtonCommand));
        }

        public static void CloseButtonCommand(Object sender, ExecutedRoutedEventArgs e)
        {
            TextBoxTip control = sender as TextBoxTip;
            if (control != null)
            {
                control.Text = "";
            }
        }

        public static RoutedCommand XButtonCommand { set; get; }
    }
}
