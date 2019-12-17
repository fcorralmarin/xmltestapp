using XMLTestApp.Data;
using XMLTestApp.Extensions;
using XMLTestApp.Models.Logic.XMLCreators;
using XMLTestApp.Models.Logic.XMLWatchers;
using XMLTestApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XMLTestApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel(new ConfigurationCreator(), new ProductContext(), new ConfigurationWatcher(new ProductContext()));
            
            //string inputpath = System.IO.Path.Combine(Environment.CurrentDirectory, "Resources", "XMLTest.xml");
            //Models.Logic.XMLContainers.ConfigurationContainer configuration = new Models.Logic.XMLContainers.ConfigurationContainer();
            //configuration.LoadFromXML(inputpath);
            //configuration.SaveAsXML(System.IO.Path.Combine(Environment.CurrentDirectory, "Resources", "XMLTestOut.xml"));
            //ProductsAccessor configAccess = new ProductsAccessor(new ProductContext());
            //var a = configAccess.Products;
            //configuration.ToDBModel();
        }
    }
}
