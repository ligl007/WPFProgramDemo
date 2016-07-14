/***********************************************************************   
* Copyright(c) 2016-2050 ligl
* CLR 版本: 4.0.30319.42000   
* 文 件 名：WindowBorder   
* 创 建 人：ligl   
* 创建日期：2016/7/14 22:47:10   
* 修 改 人：ligl   
* 修改日期：   
* 备注描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Commons
{
    /// <summary>
    /// 窗口边框类，提供设置窗口边框样式的属性及调整窗口大小的方法。
    /// </summary>
    public class WindowBorder : Control
    {
        private Window _Owner;      // 所属窗口。

        /// <summary>
        /// 获取该边框所属的窗口。
        /// </summary>
        private Window Owner
        {
            get
            {
                if (_Owner == null)
                {
                    _Owner = Window.GetWindow(this);
                }
                return _Owner;
            }
        }

        /// <summary>
        /// 静态构造，启用默认样式。
        /// </summary>
        static WindowBorder()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WindowBorder), new FrameworkPropertyMetadata(typeof(WindowBorder)));
        }

        public WindowBorder()
        {

        }



        #region 依赖属性

        /// <summary>
        /// 设置或提取边框圆角半径。
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(WindowBorder),
            new UIPropertyMetadata(new CornerRadius(0)));


        /// <summary>
        /// 获取或设置是否可调整窗口大小。
        /// </summary>
        public bool IsResizable
        {
            get { return (bool)GetValue(IsResizableProperty); }
            set { SetValue(IsResizableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsResizable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsResizableProperty =
            DependencyProperty.Register("IsResizable", typeof(bool), typeof(WindowBorder), new UIPropertyMetadata(true));

        #endregion

        /// <summary>
        /// 在应用模板后为模板中的控件添加事件处理。
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            HandleThumbs();
        }

        /// <summary>
        /// 为用来调整窗口大小的Thumb控件设置DragDelta事件处理。
        /// </summary>
        private void HandleThumbs()
        {
            HandleThumbDragDelta("tmbTop", new DragDeltaEventHandler(tmbTop_DragDelta));
            HandleThumbDragDelta("tmbBottom", new DragDeltaEventHandler(tmbBottom_DragDelta));
            HandleThumbDragDelta("tmbLeft", new DragDeltaEventHandler(tmbLeft_DragDelta));
            HandleThumbDragDelta("tmbRight", new DragDeltaEventHandler(tmbRight_DragDelta));
            HandleThumbDragDelta("tmbTopLeft", new DragDeltaEventHandler(tmbTopLeft_DragDelta));
            HandleThumbDragDelta("tmbTopRight", new DragDeltaEventHandler(tmbTopRight_DragDelta));
            HandleThumbDragDelta("tmbBottomRight", new DragDeltaEventHandler(tmbBottomRight_DragDelta));
            HandleThumbDragDelta("tmbBottomLeft", new DragDeltaEventHandler(tmbBottomLeft_DragDelta));
            if (_Owner != null)
            {
                _SizeToContent = this.Owner.SizeToContent;
            }


        }

        private SizeToContent _SizeToContent = SizeToContent.Manual;

        /// <summary>
        /// 为指定名称的Thumb控件添加DragDelta事件处理。
        /// </summary>
        /// <param name="thumbName"></param>
        /// <param name="handler"></param>
        private void HandleThumbDragDelta(string thumbName, DragDeltaEventHandler handler)
        {
            Thumb tmbTop = this.Template.FindName(thumbName, this) as Thumb;
            if (tmbTop != null)
            {
                tmbTop.DragDelta += handler;
            }
        }

        void SetSizeToContent()
        {
            this.Owner.SizeToContent = _SizeToContent == SizeToContent.Manual ? _SizeToContent : SizeToContent.Manual;
        }

        #region 事件处理

        /// <summary>
        /// 上边框拖动调整窗口大小。
        /// </summary>
        /// <param name="sender">事件发送者。</param>
        /// <param name="e">事件参数。</param>
        void tmbTop_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this.Owner == null)
            {
                return;
            }
            SetSizeToContent();
            double newHeight = this.Owner.Height - e.VerticalChange;
            if (newHeight < this.Owner.MinHeight)
            {
                return;
            }

            this.Owner.Top += e.VerticalChange;
            this.Owner.Height = newHeight;
        }

        /// <summary>
        /// 下边框拖动调整窗口大小。
        /// </summary>
        /// <param name="sender">事件发送者。</param>
        /// <param name="e">事件参数。</param>
        void tmbBottom_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this.Owner == null)
            {
                return;
            }
            SetSizeToContent();
            double newHeight = this.Owner.Height + e.VerticalChange;
            if (newHeight < this.Owner.MinHeight)
            {
                return;
            }

            this.Owner.Height = newHeight;
        }

        /// <summary>
        /// 左边框拖动调整窗口大小。
        /// </summary>
        /// <param name="sender">事件发送者。</param>
        /// <param name="e">事件参数。</param>
        void tmbLeft_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this.Owner == null)
            {
                return;
            }
            SetSizeToContent();
            double newWidth = this.Owner.Width - e.HorizontalChange;
            if (newWidth < this.Owner.MinWidth)
            {
                return;
            }

            this.Owner.Left += e.HorizontalChange;
            this.Owner.Width = newWidth;
        }

        /// <summary>
        /// 右边框拖动调整窗口大小。
        /// </summary>
        /// <param name="sender">事件发送者。</param>
        /// <param name="e">事件参数。</param>
        void tmbRight_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this.Owner == null)
            {
                return;
            }
            SetSizeToContent();
            double newWidth = this.Owner.Width + e.HorizontalChange;
            if (newWidth < this.Owner.MinWidth)
            {
                return;
            }

            this.Owner.Width = newWidth;
        }

        /// <summary>
        /// 左上角拖动调整窗口大小。
        /// </summary>
        /// <param name="sender">事件发送者。</param>
        /// <param name="e">事件参数。</param>
        void tmbTopLeft_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this.Owner == null)
            {
                return;
            }
            SetSizeToContent();
            double newWidth = this.Owner.Width - e.HorizontalChange;
            double newHeight = this.Owner.Height - e.VerticalChange;
            if (newWidth >= this.Owner.MinWidth)
            {
                this.Owner.Width = newWidth;
                this.Owner.Left += e.HorizontalChange;
            }
            if (newHeight >= this.Owner.MinHeight)
            {
                this.Owner.Top += e.VerticalChange;
                this.Owner.Height = newHeight;
            }
        }

        /// <summary>
        /// 右上角拖动调整窗口大小。
        /// </summary>
        /// <param name="sender">事件发送者。</param>
        /// <param name="e">事件参数。</param>
        void tmbTopRight_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this.Owner == null)
            {
                return;
            }
            SetSizeToContent();
            double newWidth = this.Owner.Width + e.HorizontalChange;
            double newHeight = this.Owner.Height - e.VerticalChange;
            if (newWidth >= this.Owner.MinWidth)
            {
                this.Owner.Width = newWidth;
            }
            if (newHeight >= this.Owner.MinHeight)
            {
                this.Owner.Top += e.VerticalChange;
                this.Owner.Height = newHeight;
            }
        }

        /// <summary>
        /// 左下角拖动调整窗口大小。
        /// </summary>
        /// <param name="sender">事件发送者。</param>
        /// <param name="e">事件参数。</param>
        void tmbBottomLeft_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this.Owner == null)
            {
                return;
            }
            SetSizeToContent();
            double newWidth = this.Owner.Width - e.HorizontalChange;
            double newHeight = this.Owner.Height + e.VerticalChange;
            if (newWidth >= this.Owner.MinWidth)
            {
                this.Owner.Width = newWidth;
                this.Owner.Left += e.HorizontalChange;
            }
            if (newHeight >= this.Owner.MinHeight)
            {
                this.Owner.Height = newHeight;
            }
        }

        /// <summary>
        /// 右下角拖动调整窗口大小。
        /// </summary>
        /// <param name="sender">事件发送者。</param>
        /// <param name="e">事件参数。</param>
        void tmbBottomRight_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this.Owner == null)
            {
                return;
            }
            SetSizeToContent();
            double newWidth = this.Owner.Width + e.HorizontalChange;
            double newHeight = this.Owner.Height + e.VerticalChange;
            if (newWidth >= this.Owner.MinWidth)
            {
                this.Owner.Width = newWidth;
            }
            if (newHeight >= this.Owner.MinHeight)
            {
                this.Owner.Height = newHeight;
            }
        }

        #endregion
    }

    /// <summary>
    /// 提供拖动窗口和双击改变窗口状态的功能。
    /// </summary>
    public class MoveBorder : Border
    {
        Window hostWindow;
        public MoveBorder()
        {
            this.Loaded += new RoutedEventHandler(MoveBorder_Loaded);
        }

        void MoveBorder_Loaded(object sender, RoutedEventArgs e)
        {
            hostWindow = Window.GetWindow(this);
            if (hostWindow != null)
            {
                _SizeToContent = hostWindow.SizeToContent;
                this.MouseLeftButtonDown += new MouseButtonEventHandler(MoveBorder_MouseLeftButtonDown);

            }

        }
        private SizeToContent _SizeToContent = SizeToContent.Manual;

        /// <summary>
        /// 单击按下时拖拽窗口，双击时最大化/返回窗口大小。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MoveBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (e.Source != sender)
            {
                return;
            }

            Window hostWindow = Window.GetWindow(this);
            if (e.ClickCount == 1)
            {
                if (hostWindow != null)
                {
                    try
                    {
                        hostWindow.DragMove();
                    }
                    catch
                    { }
                }
            }
            else if (hostWindow.ResizeMode == ResizeMode.CanResize)
            {
                if (hostWindow.WindowState != WindowState.Maximized)
                {
                    hostWindow.SizeToContent = SizeToContent.Manual;
                    hostWindow.WindowState = WindowState.Maximized;
                }
                else
                {
                    hostWindow.WindowState = WindowState.Normal;
                    hostWindow.SizeToContent = _SizeToContent;
                }
            }
        }
    }
}
