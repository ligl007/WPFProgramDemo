/***********************************************************************   
* Copyright(c) 2016-2050 ligl
* CLR 版本: 4.0.30319.42000   
* 文 件 名：BaseNotifyChanged   
* 创 建 人：ligl   
* 创建日期：2016/7/8 19:40:36   
* 修 改 人：ligl   
* 修改日期：   
* 备注描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Commons
{
    public class BaseNotifyChanged : INotifyPropertyChanged
    {

        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
