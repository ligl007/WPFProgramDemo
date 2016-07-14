/***********************************************************************   
* Copyright(c) 2016-2050 ligl
* CLR 版本: 4.0.30319.42000   
* 文 件 名：TipMenu   
* 创 建 人：ligl   
* 创建日期：2016/7/14 20:00:49   
* 修 改 人：ligl   
* 修改日期：   
* 备注描述：
************************************************************************/
using IProject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace TipPlugs
{
    [Export(typeof(IPlugin))]
    public class TipMenu : IPlugin
    {
        public Control ContentControl
        {
            get
            {
                return new UserControlTip();
            }
        }

        public string PlugName
        {
            get
            {
                return "文本提示";
            }
        }
    }
}
