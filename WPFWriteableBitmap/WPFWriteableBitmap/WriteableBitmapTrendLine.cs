/***********************************************************************   
* Copyright(c) 2016-2050 ligl
* CLR 版本: 4.0.30319.42000   
* 文 件 名：WriteableBitmapTrendLine   
* 创 建 人：ligl   
* 创建日期：2016/7/23 19:41:23   
* 修 改 人：ligl   
* 修改日期：   
* 备注描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
//https://social.msdn.microsoft.com/Forums/zh-CN/febcee07-dc8b-44b4-8c0a-246daffdbe2b/wpf-?forum=wpfzhchs
namespace WPFWriteableBitmap
{
    public class WriteableBitmapTrendLine : FrameworkElement
    {
        #region DependencyProperties

        public static readonly DependencyProperty LatestQuoteProperty =
            DependencyProperty.Register("LatestQuote", typeof(MinuteQuoteViewModel), typeof(WriteableBitmapTrendLine),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, OnLatestQuotePropertyChanged));

        private static void OnLatestQuotePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WriteableBitmapTrendLine trendLine = (WriteableBitmapTrendLine)d;
            MinuteQuoteViewModel latestQuote = (MinuteQuoteViewModel)e.NewValue;
            if (latestQuote != null)
            {
                trendLine.DrawTrendLine(latestQuote.Ordinal, (float)latestQuote.LastPx);
                trendLine.DrawTrendLineF(latestQuote.Ordinal, (float)latestQuote.LastPx);
            }
        }

        public MinuteQuoteViewModel LatestQuote
        {
            get { return (MinuteQuoteViewModel)GetValue(LatestQuoteProperty); }
            set { SetValue(LatestQuoteProperty, value); }
        }

        #endregion

        private const int COLS = 1024;
        private const int ROWS = 768;

        private WriteableBitmap bitmap;
        private float maxPrice = 0.0F;
        private static int dx = 3;
        //private float[] prices = new float[COLS / dx];
        private float[] prices = new float[COLS];

        public WriteableBitmapTrendLine()
        {
            this.bitmap = new WriteableBitmap(COLS, ROWS, 96, 96, PixelFormats.Rgb24, null);

            this.bitmap.Lock();

            using (Bitmap backBufferBitmap = new Bitmap(COLS, ROWS,
               this.bitmap.BackBufferStride, System.Drawing.Imaging.PixelFormat.Format24bppRgb,
               this.bitmap.BackBuffer))
            {
                using (Graphics backBufferGraphics = Graphics.FromImage(backBufferBitmap))
                {
                    backBufferGraphics.Clear(System.Drawing.Color.Black);
                    backBufferGraphics.Flush();
                }
            }

            this.bitmap.AddDirtyRect(new Int32Rect(0, 0, COLS, ROWS));
            this.bitmap.Unlock();
        }

        private void DrawTrendLine(int ordinal, float latestPrice)
        {
            if (double.IsNaN(latestPrice))
                return;
            if (this.prices.Length <= latestPrice)
            {
                return;
            }

            this.prices[ordinal] = latestPrice;
            bool redraw = false;
            if (ordinal == 0)
            {
                this.maxPrice = latestPrice;
            }
            else
            {
                if (latestPrice > this.maxPrice)
                {
                    this.maxPrice = latestPrice;
                    redraw = true;
                }
            }

            if (ordinal == 0)
            {
                int width = this.bitmap.PixelWidth;
                int height = this.bitmap.PixelHeight;

                this.bitmap.Lock();

                using (Bitmap backBufferBitmap = new Bitmap(width, height,
                    this.bitmap.BackBufferStride, System.Drawing.Imaging.PixelFormat.Format24bppRgb,
                    this.bitmap.BackBuffer))
                {
                    using (Graphics backBufferGraphics = Graphics.FromImage(backBufferBitmap))
                    {
                        backBufferGraphics.Clear(System.Drawing.Color.WhiteSmoke);
                        backBufferGraphics.Flush();
                    }
                }

                this.bitmap.AddDirtyRect(new Int32Rect(0, 0, width, height));
                this.bitmap.Unlock();
            }
            else
            {
                System.Drawing.Point[] points = new System.Drawing.Point[ordinal + 1];
                float dy = (float)(ROWS / (this.maxPrice * 1.3));
                for (int i = 0; i <= ordinal; i++)
                {
                    points[i].X = i ;
                    points[i].Y = (int)(this.prices[i]);
                }

                int width = ordinal * dx + 1;
                int height = this.bitmap.PixelHeight;

                this.bitmap.Lock();

                using (Bitmap backBufferBitmap = new Bitmap(width, height,
                    this.bitmap.BackBufferStride, System.Drawing.Imaging.PixelFormat.Format24bppRgb,
                    this.bitmap.BackBuffer))
                {
                    using (Graphics backBufferGraphics = Graphics.FromImage(backBufferBitmap))
                    {
                        backBufferGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        backBufferGraphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.AssumeLinear;

                        //if (redraw)
                        //backBufferGraphics.Clear(System.Drawing.Color.Red);
                        backBufferGraphics.DrawLines(System.Drawing.Pens.White, points);
                        backBufferGraphics.Flush();
                    }
                }
                //this.bitmap.AddDirtyRect(new Int32Rect(0, 0, width, height));
                this.bitmap.AddDirtyRect(new Int32Rect(0, 0, COLS, ROWS));
                this.bitmap.Unlock();
            }
        }
        private System.Drawing.Pen defaultPens = System.Drawing.Pens.White;
        private void DrawTrendLineF(int ordinal, float latestPrice)
        {
            if (double.IsNaN(latestPrice))
                return;
            if (this.prices.Length <= latestPrice)
            {
                if (defaultPens == System.Drawing.Pens.White)
                {
                    defaultPens = System.Drawing.Pens.Blue;
                }
                else
                {
                    defaultPens = System.Drawing.Pens.White;
                }

                return;
            }
            this.prices[ordinal] = latestPrice;
            if (ordinal == 0)
            {
                this.maxPrice = latestPrice;
            }
            else
            {
                if (latestPrice > this.maxPrice)
                {
                    this.maxPrice = latestPrice;
                }
            }

            if (ordinal == 0)
            {
                int width = this.bitmap.PixelWidth;
                int height = this.bitmap.PixelHeight;

                this.bitmap.Lock();

                using (Bitmap backBufferBitmap = new Bitmap(width, height,
                    this.bitmap.BackBufferStride, System.Drawing.Imaging.PixelFormat.Format24bppRgb,
                    this.bitmap.BackBuffer))
                {
                    using (Graphics backBufferGraphics = Graphics.FromImage(backBufferBitmap))
                    {
                        backBufferGraphics.Clear(System.Drawing.Color.WhiteSmoke);
                        backBufferGraphics.Flush();
                    }
                }

                this.bitmap.AddDirtyRect(new Int32Rect(0, 0, width, height));
                this.bitmap.Unlock();
            }
            else
            {
                int count = this.prices.Length;
                PointF[] points = new PointF[ordinal + 1];
                float dy = (float)(ROWS / this.maxPrice);
                for (int i = 0; i <= ordinal; i++)
                {
                    points[i].X = i;
                    points[i].Y = (float)Math.Floor(this.prices[i] * dy);

                    if (float.IsNaN(points[i].Y))
                        points[i].Y = 0.0F;
                }

                int width = ordinal + 1;
                int height = this.bitmap.PixelHeight;

                this.bitmap.Lock();

                using (Bitmap backBufferBitmap = new Bitmap(width, height,
                    this.bitmap.BackBufferStride, System.Drawing.Imaging.PixelFormat.Format24bppRgb,
                    this.bitmap.BackBuffer))
                {
                    using (Graphics backBufferGraphics = Graphics.FromImage(backBufferBitmap))
                    {
                        backBufferGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        backBufferGraphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.AssumeLinear;
                        //backBufferGraphics.DrawLines(System.Drawing.Pens.Green, points);
                        backBufferGraphics.DrawLine(defaultPens, 0, 500, ordinal, 500);
                        backBufferGraphics.Flush();
                    }
                }
                this.bitmap.AddDirtyRect(new Int32Rect(0, 0, COLS, ROWS));
                this.bitmap.Unlock();
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            //设置坐标系方向成底部为X Y轴的原点
            drawingContext.PushTransform(new ScaleTransform(1, -1, 0, RenderSize.Height / 2));
            drawingContext.DrawImage(bitmap, new Rect(0, 0, RenderSize.Width, RenderSize.Height));
        }
    }
}
