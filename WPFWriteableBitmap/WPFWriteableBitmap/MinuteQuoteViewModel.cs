/***********************************************************************   
* Copyright(c) 2016-2050 ligl
* CLR 版本: 4.0.30319.42000   
* 文 件 名：Class1   
* 创 建 人：ligl   
* 创建日期：2016/7/23 19:42:18   
* 修 改 人：ligl   
* 修改日期：   
* 备注描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFWriteableBitmap
{
    public class MinuteQuoteViewModel : INotifyPropertyChanged
    {
        private int ordinal;
        public int Ordinal
        {
            get { return this.ordinal; }
            set { if (this.ordinal != value) { this.ordinal = value; this.OnPropertyChanged("Ordinal"); } }
        }

        //private DateTime quoteTime;
        //public DateTime QuoteTime
        //{
        //    get { return this.quoteTime; }
        //    set { if (this.quoteTime != value) { this.quoteTime = value; this.OnPropertyChanged("QuoteTime"); } }
        //}

        private double lastPx = double.NaN;
        public double LastPx
        {
            get { return this.lastPx; }
            set { if (this.lastPx != value) { this.lastPx = value; this.OnPropertyChanged("LastPx"); } }
        }

        //private double avgPx = double.NaN;
        //public double AvgPx
        //{
        //    get { return this.avgPx; }
        //    set { if (this.avgPx != value) { this.avgPx = value; this.OnPropertyChanged("AvgPx"); } }
        //}

        //private int volume;
        //public int Volume
        //{
        //    get { return this.volume; }
        //    set { if (this.volume != value) { this.volume = value; this.OnPropertyChanged("Volume"); } }
        //}

        //private double amount = double.NaN;
        //public double Amount
        //{
        //    get { return this.amount; }
        //    set { if (this.amount != value) { this.amount = value; this.OnPropertyChanged("Amount"); } }
        //}

        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
