/***********************************************************************   
* Copyright(c) 2016-2050 ligl
* CLR 版本: 4.0.30319.42000   
* 文 件 名：CoordinateXYMenus   
* 创 建 人：ligl   
* 创建日期：2016/7/8 19:51:21   
* 修 改 人：ligl   
* 修改日期：   
* 备注描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using IProject;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace CoordinateXY
{
    [Export(typeof(IPlugin))]
    public class CoordinateXYMenus : IPlugin
    {
        private Control _ContentControl;

        private Control GetContent()
        {
            //if (_ContentControl == null)
            //{
                _ContentControl = new UserControlShow();
            //}

            return _ContentControl;
        }

        #region IPlugin 成员

        public string PlugName
        {
            get
            {
                return "XY坐标系";
            }
        }

        public System.Windows.Controls.Control ContentControl
        {
            get { return GetContent(); }
        }

        #endregion
    }
}
