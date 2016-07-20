/***********************************************************************   
* Copyright(c) 2016-2050 ligl
* CLR 版本: 4.0.30319.42000   
* 文 件 名：Skin   
* 创 建 人：ligl   
* 创建日期：2016/7/20 22:17:33   
* 修 改 人：ligl   
* 修改日期：   
* 备注描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ResourcesStyle
{
    /// <summary>
    /// 表示一套皮肤。
    /// </summary>
    public class Skin : INotifyPropertyChanged
    {
        private ICollection<Uri> xamls;
        private ICollection<ResourceDictionary> _Resources;
        private AssemblyName skinAssembly;

        /// <summary>
        /// False 表示系统背景，True 表示用户自定义
        /// </summary>
        public bool IsSystem { get; set; }

        /// <summary>
        /// 获取或设置皮肤名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置皮肤缩略图。
        /// </summary>
        public ImageSource Thumbnail { get; set; }


        private Brush _Brush;

        /// <summary>
        /// 获取或设置皮肤刷子。
        /// </summary>
        public Brush Brush
        {
            get { return _Brush; }
            set { _Brush = value; OnRegister("Brush"); }
        }

        /// <summary>
        /// 获取皮肤资源字典集合。
        /// </summary>
        public ICollection<ResourceDictionary> Resources
        {
            get
            {
                if (_Resources.Count == 0)
                {
                    //if (this.xamls != null)
                    //{
                    //    Load(this.xamls);
                    //}
                    //else if (this.skinAssembly != null)
                    //{
                    //    Load(this.skinAssembly);
                    //}
                }
                return _Resources;
            }
            private set { _Resources = value; }
        }

        /// <summary>
        /// 使用指定的XAML文件实例化一个Skin类。
        /// </summary>
        /// <param name="xamlUri">包含资源字典的XAML文件，该XAML文件须以为ResourceDictionary根元素。</param>
        public Skin(Uri xamlUri)
        {
            if (xamlUri == null)
            {
                throw new ArgumentNullException("xamlUri");
            }

            this.xamls = new List<Uri>();
            this.xamls.Add(xamlUri);
            _Resources = new List<ResourceDictionary>();
            #region 取颜色值
            ResourceDictionary rd = new ResourceDictionary();
            try
            {
                rd.Source = xamlUri;
                Brush lineBrush = rd["WindowBackground"] as Brush;
                this.Brush = lineBrush;
            }
            catch (Exception ex)
            {
                
            }
            #endregion
            //this.Name = xamlUri.ToString();
        }

        public Skin()
        {

        }

        /// <summary>
        /// 用指定的名称和XAML文件实例化一个Skin类。
        /// </summary>
        /// <param name="name">皮肤名称。</param>
        /// <param name="xaml">包含资源字典的XAML文件，该XAML文件须以为ResourceDictionary根元素。</param>
        public Skin(string name, Uri xamlUri)
            : this(xamlUri)
        {
            this.Name = name;
        }

        /// <summary>
        /// 使用指定的程序集实例化一个Skin类。
        /// </summary>
        /// <param name="assembly">包含皮肤资源的程序集。</param>
        public Skin(AssemblyName assembly)
        {
            this.skinAssembly = assembly;
            _Resources = new List<ResourceDictionary>();
        }

        /// <summary>
        /// 使用指定的名称和包含资源的程序集实例化一个Skin类。
        /// </summary>
        /// <param name="name">皮肤名称。</param>
        /// <param name="assembly">包含皮肤资源的程序集。</param>
        public Skin(string name, AssemblyName assembly)
            : this(assembly)
        {
            this.Name = name;
        }

  

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="propertyname"></param>
        private void OnRegister(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
