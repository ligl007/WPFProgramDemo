using Commons;
using Commons.Helper;
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
    ///     xmlns:MyNamespace="clr-namespace:TipPlugs"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:TipPlugs;assembly=TipPlugs"
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
    ///     <MyNamespace:TextBoxSign/>
    ///
    /// </summary>
    public class TextBoxSign : TextBox
    {
        static TextBoxSign()
        {

            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBoxSign), new FrameworkPropertyMetadata(typeof(TextBoxSign)));
        }

        TextBox NormalTxt;
        TextBox TrimmingTxt;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            NormalTxt = this.Template.FindName("PART_Text", this) as TextBox;
            TrimmingTxt = this.Template.FindName("txbTrimming", this) as TextBox;
            TrimmingTxt.GotFocus += new RoutedEventHandler(TrimmingTxt_GotFocus);
            NormalTxt.LostFocus += new RoutedEventHandler(NormalTxt_LostFocus);
            NormalTxt.GotFocus += new RoutedEventHandler(NormalTxt_GotFocus);
            this.KeyUp += TextBoxSign_KeyUp;
        }

        private void TextBoxSign_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //FocusManager.SetFocusedElement(this, null);
                //Keyboard.Focus(null);
                NormalTxt_LostFocus(null, null);
            }
        }

        void NormalTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            NormalIsFocus = true;
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            bool IsShow = false;
            if (!string.IsNullOrEmpty(this.Text))
            {
                IsShow = true;
            }
            else
            {
                IsShow = false;
            }
            ShowToolTip(IsShow);
            base.OnTextChanged(e);
        }

        /// <summary>
        /// 如何显示Tooltip文本
        /// </summary>
        /// <param name="isShow"></param>
        private void ShowToolTip(bool isShow)
        {
            if (isShow)
            {
                this.ToolTip = this.Text;
            }
            else
            {
                this.ToolTip = null;
            }
        }

        void NormalTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Text))
            {
                double wd = this.ActualWidth;
                ControlTrimming(wd);
            }
            NormalIsFocus = false;
        }

        bool NormalIsFocus = false;
        void TrimmingTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Text))
            {
                TrimmingTxt.Visibility = Visibility.Collapsed;
                NormalTxt.Visibility = Visibility.Visible;

            }
            NormalTxt.Focus();

        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            Size size = base.ArrangeOverride(arrangeBounds);
            if (NormalIsFocus == false)
            {
                ControlTrimming(arrangeBounds.Width);
            }
            return size;
        }
        private void ControlTrimming(double width)
        {
            if (!string.IsNullOrEmpty(this.Text))
            {
                NormalTxt.Visibility = Visibility.Hidden;
                TrimmingTxt.Visibility = Visibility.Visible;
                Typeface face = new Typeface(this.FontFamily, this.FontStyle, this.FontWeight, this.FontStretch);
                bool IsShowTip = false;
                TrimmingTxt.Text = TrimmingHelper.Trim(Text, "...", "", width - 10, face, this.FontSize, ref IsShowTip);
                Console.WriteLine(IsShowTip);
                ShowToolTip(IsShowTip);
            }
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
            DependencyProperty.Register("TooTipText", typeof(string), typeof(TextBoxSign), new UIPropertyMetadata("", null));
    }

    class TextBoxConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
