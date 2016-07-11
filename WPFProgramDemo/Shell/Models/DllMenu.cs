/***********************************************************************   
* Copyright(c) 2016-2050 ligl
* CLR 版本: 4.0.30319.42000   
* 文 件 名：DllMenu   
* 创 建 人：ligl   
* 创建日期：2016/7/8 19:37:11   
* 修 改 人：ligl   
* 修改日期：   
* 备注描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Commons;

namespace WPFProgramDemo.Models
{
    public class DllMenu 
    {

        private string _MenuName;

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName
        {
            get { return _MenuName; }
            set { _MenuName = value; }
        }

       

        private string _DllPath;
        /// <summary>
        /// 菜单路径
        /// </summary>
        public string DllPath
        {
            get { return _DllPath; }
            set { _DllPath = value; }
        }
       
    }
}
