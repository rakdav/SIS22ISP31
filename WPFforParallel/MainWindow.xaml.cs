using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WPFforParallel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CancellationTokenSource ctoken = new CancellationTokenSource();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            ctoken.Cancel();
        }

        private void cmdProcess_Click(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(()=>ProcessFiles());
            this.Title = "Процесс завершен";
        }
        private void ProcessFiles()
        {
            var basePath = Directory.GetCurrentDirectory();
            var pictureDirectory = System.IO.Path.Combine(basePath, "TestPictures");
            var outputDirectory = System.IO.Path.Combine(basePath,"ModifiedPictures");
            if(Directory.Exists(outputDirectory))
            {
                Directory.Delete(outputDirectory,true);
            }
            Directory.CreateDirectory(outputDirectory);
            ParallelOptions options = new ParallelOptions();
            options.CancellationToken= ctoken.Token;          options.MaxDegreeOfParallelism=Environment.ProcessorCount;
            string[] files = Directory.GetFiles(pictureDirectory,"*.jpg",SearchOption.AllDirectories);
            //foreach(string currentFile in files)
            //{
            try
            {
                Parallel.ForEach(files, options, currentFile =>
                {
                    string filename = System.IO.Path.GetFileName(currentFile);
                    //this.Title = $"Процесс {filename} в потоке {Thread.CurrentThread.ManagedThreadId}";
                    Dispatcher?.Invoke(() =>
                    {
                        this.Title = $"Обработка {filename}";
                    }
                    );
                    using (Bitmap bitmap = new Bitmap(currentFile))
                    {
                        bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        bitmap.Save(System.IO.Path.Combine(outputDirectory, filename));

                    }
                });
                Dispatcher?.Invoke(()=>this.Title="Завершено");
            }
            catch (OperationCanceledException ex)
            {
                Dispatcher?.Invoke(()=>this.Title=ex.Message);
            }
        }
    }
}
