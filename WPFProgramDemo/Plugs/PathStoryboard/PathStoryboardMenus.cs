/***********************************************************************   
* Copyright(c) 2016-2050 ligl
* CLR 版本: 4.0.30319.42000   
* 文 件 名：PathStoryboardMenus   
* 创 建 人：ligl   
* 创建日期：2016/7/8 21:13:57   
* 修 改 人：ligl   
* 修改日期：   
* 备注描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using IProject;

namespace PathStoryboard
{
    [Export(typeof(IPlugin))]
    public class PathStoryboardMenus : IPlugin
    {
        #region IPlugin 成员

        public string PlugName
        {
            get
            {
                return "Path路径动画";
            }
        }

       


        public System.Windows.Controls.Control ContentControl
        {
            get { return null; }
        }

        #endregion
    }
}
