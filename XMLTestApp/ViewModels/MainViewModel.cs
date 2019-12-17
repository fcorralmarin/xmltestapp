using XMLTestApp.Contracts;
using XMLTestApp.Data;
using XMLTestApp.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace XMLTestApp.ViewModels
{
    public class MainViewModel : Notifier
    {
        private readonly IXMLCreator<IXMLContainer> XMLCreator;
        private readonly ISQLiteContext<ProductSQLiteTable> SQLiteContext;
        private readonly IXMLDirectoryWatcher<ProductSQLiteTable> XMLDirectoryWatcher;

        private List<ProductSQLiteTable> NewProductsAddedList = new List<ProductSQLiteTable>();
        public MainViewModel(IXMLCreator<IXMLContainer> XMLCreator, ISQLiteContext<ProductSQLiteTable> SQLiteContext, IXMLDirectoryWatcher<ProductSQLiteTable> XMLDirectoryWatcher)
        {
            this.XMLCreator = XMLCreator;
            this.SQLiteContext = SQLiteContext;
            this.XMLDirectoryWatcher = XMLDirectoryWatcher;
            XMLDirectoryWatcher.XMLProcessed += XMLDirectoryWatcher_XMLProcessed;
            InitSQLContext();
            UpdateProductList();
            TryLoadInputFolderPath();
        }

        private void XMLDirectoryWatcher_XMLProcessed(object sender, IEnumerable<ProductSQLiteTable> e)
        {
            NewProductsAddedList.AddRange(e);
            UpdateProductList();
        }

        private void InitSQLContext()
        {
            SQLiteContext.Connect(Path.Combine(Environment.CurrentDirectory, Properties.Settings.Default.SQLiteResourcePath));
            SQLiteContext.CreateTable();
        }

        private void UpdateProductList()
        {
            ProductsCollection = (HideOldEntries ?
                                 new ObservableCollection<ProductSQLiteTable>(NewProductsAddedList.OrderByDescending(x => x._Id)) :
                                 new ObservableCollection<ProductSQLiteTable>(SQLiteContext.GetAll().OrderByDescending(x => x._Id)));
        }

        private void TryLoadInputFolderPath()
        {
            if (Directory.Exists(Properties.Settings.Default.InputFolderPath))
            {
                InputFolderPath = Properties.Settings.Default.InputFolderPath;
            }
        }

        private bool _hideOldEntries;
        public bool HideOldEntries
        {
            get
            {
                return _hideOldEntries;
            }
            set
            {
                _hideOldEntries = value;
            }
        }

        private ObservableCollection<ProductSQLiteTable> _productsCollection;
        public ObservableCollection<ProductSQLiteTable> ProductsCollection
        {
            get
            {
                return _productsCollection;
            }
            set
            {
                _productsCollection = value;
                RaisePropertyChanged("ProductsCollection");
            }
        }

        private string _inputFolderPath;
        public string InputFolderPath
        {
            get
            {
                return _inputFolderPath;
            }
            set
            {
                if(_inputFolderPath != value)
                {
                    _inputFolderPath = value;
                    XMLDirectoryWatcher.WatchDirectory(_inputFolderPath);
                    RaisePropertyChanged("InputFolderPath");
                    RaisePropertyChanged("CreateIsEnabled");
                }
            }
        }

        public bool CreateIsEnabled
        {
            get
            {
                return !string.IsNullOrEmpty(InputFolderPath);
            }
        }

        private ICommand _createNewRandomXMLCommand;
        public ICommand CreateNewRandomXMLCommand
        {
            get
            {
                _createNewRandomXMLCommand ??= new RelayCommand(async () =>
                {
                    Tuple<string, IXMLContainer> xmlContainer;
                    await Task.Run(() =>
                    {
                        xmlContainer = XMLCreator.CreateRandomXML();
                        xmlContainer.Item2.SaveAsXML(Path.Combine(InputFolderPath, xmlContainer.Item1));
                    });
                });
                return _createNewRandomXMLCommand;
            }
        }

        private ICommand _openFolderDialogCommand;
        public ICommand OpenFolderDialogCommand
        {
            get
            {
                _openFolderDialogCommand ??= new RelayCommand(() =>
                {
                    OpenFolderDialog();
                });
                return _openFolderDialogCommand;
            }
        }

        private void OpenFolderDialog()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            try
            {
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    InputFolderPath = dialog.SelectedPath;
                    StoreInputFolderPathInSettings();
                }
            }
            finally
            {
                dialog.Dispose();
            }
        }

        private void StoreInputFolderPathInSettings()
        {
            Properties.Settings.Default.InputFolderPath = InputFolderPath;
            Properties.Settings.Default.Save();
        }

        private ICommand _updateGridCommand;
        public ICommand UpdateGridCommand
        {
            get
            {
                _updateGridCommand ??= new RelayCommand(() =>
                {
                    UpdateProductList();
                });
                return _updateGridCommand;
            }
        }
    }
}
