/***********************************************************************   
* Copyright(c) 2016-2050 ligl
* CLR 版本: 4.0.30319.42000   
* 文 件 名：SkinHelper   
* 创 建 人：ligl   
* 创建日期：2016/7/20 22:00:41   
* 修 改 人：ligl   
* 修改日期：   
* 备注描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ResourcesStyle
{
    public class SkinHelper
    {

        public SkinHelper()
        {
            LoadSkins();
        }

        /// <summary>
        /// 创建皮肤按钮。
        /// </summary>
        /// <returns></returns>
        public List<Button> CreateSkinButton()
        {
            Button btnSkin = new Button();
            Image imgSkin = new Image();
            imgSkin.Width = 16;
            imgSkin.Source = new BitmapImage(new Uri("/ResourcesStyle;component/Images/skinButton.png", UriKind.RelativeOrAbsolute));
            btnSkin.Content = imgSkin;
            btnSkin.ToolTip = "更改皮肤";
            btnSkin.Click += (s, e) =>
            {

            };
            List<Button> skinButtons = new List<Button>();
            skinButtons.Add(btnSkin);
            return skinButtons;
        }


        /// <summary>
        /// 设置当前皮肤
        /// </summary>
        /// <param name="CustomName"></param>
        void SetCurrentSkin(string CustomName)
        {
            Skin skin = SkinManager.Instance.Skins.FirstOrDefault(m => m.Name == CustomName);
            if (skin != null)
            {
                SkinManager.Instance.CurrentSkin = skin;
                Application.Current.Resources["WindowBackground"] = skin.Brush;
            }
        }

        /// <summary>
        /// 生成文件的MD5值
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string MD5(string path)
        {
            try
            {
                FileStream get_file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                System.Security.Cryptography.MD5CryptoServiceProvider get_md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] hash_byte = get_md5.ComputeHash(get_file);
                string resule = System.BitConverter.ToString(hash_byte);
                resule = resule.Replace("-", "");
                get_file.Close();
                return resule;
            }
            catch (Exception e)
            {
                return e.ToString();
            }

        }

        public static void LoadSkins()
        {
            Skin skin = GetSkin("Blue");
            SkinManager.Instance.CurrentSkin = skin;
            SkinManager.Instance.Skins.Add(skin);
            SkinManager.Instance.Skins.Add(GetSkin("Orange"));
            SkinManager.Instance.Skins.Add(GetSkin("Green"));
            SkinManager.Instance.Skins.Add(GetSkin("Red"));
            SkinManager.Instance.Skins.Add(GetSkin("Pink"));
            SkinManager.Instance.Skins.Add(GetSkin("Violet"));
            SkinManager.Instance.Skins.Add(GetSkin("LightGray"));
        }

        private static Skin GetSkin(string name)
        {
            Uri skinUri = new Uri(Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                "Skins\\" + string.Format("{0}.xaml", name)));
            Skin skin = new Skin(name, skinUri);
            //skin.Brush = brush;
            return skin;
        }
    }

    /// <summary>
    /// 保存当前设置的皮肤
    /// </summary>
    [Serializable]
    public class CurrentSkin
    {
        private bool _IsSystem;

        public bool IsSystem
        {
            get { return _IsSystem; }
            set { _IsSystem = value; }
        }

        public CurrentSkin()
        {

        }

        private string _SkinName;

        public string SkinName
        {
            get { return _SkinName; }
            set { _SkinName = value; }
        }
    }
}
