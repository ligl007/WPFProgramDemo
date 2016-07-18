using Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TipPlugs
{
    /// <summary>
    /// UserControlTip.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlTip : UserControl
    {
        public UserControlTip()
        {
            InitializeComponent();
            UserControlTipViewModel viewModel = new UserControlTipViewModel();
            this.DataContext = viewModel;
        }
    }

    public class UserControlTipViewModel: BaseNotifyChanged
    {
        private string _name="";

        public int Age;

        private string _signature = "个性签名是个啥啊";

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
                OnPropertyChanged(Name);


            }
        }

        public string Signature
        {
            get
            {
                return _signature;
            }

            set
            {
                _signature = value;
                OnPropertyChanged(Signature);
            }
        }
    }

}
