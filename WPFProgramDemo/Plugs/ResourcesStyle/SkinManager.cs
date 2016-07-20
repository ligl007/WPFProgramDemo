/***********************************************************************   
* Copyright(c) 2016-2050 ligl
* CLR 版本: 4.0.30319.42000   
* 文 件 名：SkinManager   
* 创 建 人：ligl   
* 创建日期：2016/7/20 22:15:08   
* 修 改 人：ligl   
* 修改日期：   
* 备注描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace ResourcesStyle
{
    /// <summary>
    /// 皮肤管理类。提供用于获取及设置皮肤列表与当前皮肤的属性。
    /// </summary>
    public class SkinManager : INotifyPropertyChanged
    {
        private Skin _CurrentSkin;

        /// <summary>
        /// 获取或设置当前皮肤。
        /// </summary>
        public Skin CurrentSkin
        {
            get
            {
                return _CurrentSkin;
            }
            set
            {
                if (_CurrentSkin != value)
                {
                    _CurrentSkin = value;
                    OnPropertyChange("CurrentSkin");
                    ChangeSkin(value);
                }
            }
        }

        /// <summary>
        /// 获取皮肤列表。
        /// </summary>
        public ObservableCollection<Skin> Skins { get; private set; }

        /// <summary>
        /// 属性更改通知。实现INotifyPropertyChanged接口。
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 获取SkinManager类的单一实例。
        /// </summary>
        public static SkinManager Instance { get; private set; }

        /// <summary>
        /// 静态构造，实例化SkinManager类。
        /// </summary>
        static SkinManager()
        {
            Instance = new SkinManager();
            Instance.Skins = new ObservableCollection<Skin>();
        }

        /// <summary>
        /// 触发指定属性的更改事件。
        /// </summary>
        /// <param name="propertyName">属性名。</param>
        private void OnPropertyChange(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// 更换为指定的皮肤。
        /// </summary>
        /// <param name="skin"></param>
        private void ChangeSkin(Skin skin)
        {
            if (skin != null)
            {
                if (skin.IsSystem)
                {
                    Application.Current.Resources["WindowBackground"] = skin.Brush;
                }
                else
                {
                    foreach (var rd in skin.Resources)
                    {
                        LinearGradientBrush lineBrush = rd["WindowBackground"] as LinearGradientBrush;
                        Application.Current.Resources["WindowBackground"] = lineBrush;
                        Application.Current.Resources.MergedDictionaries.Add(rd);
                    }
                }

            }

        }

        ///// <summary>
        ///// 更改某一部分画刷的值 ligl add 2012.10.27
        ///// </summary>
        ///// <param name="rd"></param>
        //private void ChangeColorBrush(ResourceDictionary rd)
        //{
        //    LinearGradientBrush lineBrush = rd["WindowBackground"] as LinearGradientBrush;
        //    SolidColorBrush Brush51 = Application.Current.Resources["Brush51"] as SolidColorBrush;
        //    SolidColorBrush ToolBarBrush = Application.Current.Resources["ChatToolBarBrush"] as SolidColorBrush;
        //    if (lineBrush != null && Brush51 != null && ToolBarBrush != null)
        //    {
        //        GradientStopCollection BrushCollection = lineBrush.GradientStops;
        //        GradientStop item = BrushCollection.FirstOrDefault(m => m.Offset == 1);
        //        GradientStop itemspoint = BrushCollection.FirstOrDefault(m => m.Offset == 0);

        //        SolidColorBrush CloneBrush = Brush51.Clone();
        //        //做更改
        //        CloneBrush.Color = item.Color;
        //        Application.Current.Resources["Brush51"] = CloneBrush;
        //        SolidColorBrush ToolBrush = ToolBarBrush.Clone();
        //        //做更改
        //        ToolBrush.Color = itemspoint.Color;
        //        Application.Current.Resources["ChatToolBarBrush"] = ToolBrush;


        //    }
        //    Application.Current.Resources.MergedDictionaries.Add(rd);
        //}
    }
}
