using XMLTestApp.Contracts;
using XMLTestApp.Data;
using XMLTestApp.Models.Logic.XMLContainers;
using XMLTestApp.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLTestApp.Models.Logic.XMLWatchers
{
    public class ConfigurationWatcher : IXMLDirectoryWatcher<ProductSQLiteTable> 
    {
        private FileSystemWatcher FileSystemWatcher;
        private readonly ISQLiteContext<ProductSQLiteTable> SQLiteContext;

        public event EventHandler<IEnumerable<ProductSQLiteTable>> XMLProcessed;

        public ConfigurationWatcher(ISQLiteContext<ProductSQLiteTable> sqliteContext)
        {
            SQLiteContext = sqliteContext;
        }

        public void WatchDirectory(string directory)
        {
            if (System.IO.Directory.Exists(directory))
            {
                FileSystemWatcher = new FileSystemWatcher(directory, "*.xml");
                FileSystemWatcher.Created += FileSystemWatcher_Created;
                FileSystemWatcher.EnableRaisingEvents = true;
            }
        }

        private async void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            IEnumerable<ProductSQLiteTable> processedProducts = new List<ProductSQLiteTable>();
            await Task.Run(() =>
            {
                processedProducts = ProcessXMLFileProducts(e.FullPath);
                SQLiteContext.Insert(processedProducts);
            });
            XMLProcessed?.Invoke(this, processedProducts);
        }

        private IEnumerable<ProductSQLiteTable> ProcessXMLFileProducts(string path)
        {
            ConfigurationContainer configurationContainer = new ConfigurationContainer();
            configurationContainer.LoadFromXML(path);
            return configurationContainer.ToDBModel();
        }
    }
}
