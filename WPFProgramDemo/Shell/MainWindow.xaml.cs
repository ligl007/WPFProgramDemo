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
using System.Reflection;
using System.IO;
using IProject;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using Commons.Helper;

namespace Shell
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowHelper.CorrectMaximization(this);
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Compose();
            InitMenus();

        }
        [ImportMany]
        public IEnumerable<IPlugin> Plugins { get; set; }
        private void Compose()
        {
            string pathPlugs = AppDomain.CurrentDomain.BaseDirectory + "\\Plugs";
            //var catalog = new DirectoryCatalog(pathPlugs, "CoordinateXY.dll");
            var catalog = new DirectoryCatalog(pathPlugs);
            CompositionContainer container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        private void InitMenus()
        {
            this.listBoxMenu.ItemsSource = Plugins;
        }

        private void listBoxMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IPlugin plug = this.listBoxMenu.SelectedItem as IPlugin;
            var contentControl = plug.ContentControl;
            if (plug != null && contentControl != null)
            {
                this.gvContent.Children.Clear();
                this.gvContent.Children.Add(contentControl);
            }

        }
    }
}
