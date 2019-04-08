using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

namespace CSVReader
{
    /// <summary>
    /// Interaction logic for CSVReaderWindow.xaml
    /// </summary>
    public partial class CSVReaderWindow : Window, INotifyPropertyChanged
    {
        #region PropertyChanged stuff
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
        
        private CSVSheetViewModel m_mainViewModel = new CSVSheetViewModel();
        public CSVSheetViewModel MainViewModel
        {
            get => m_mainViewModel;
            set
            {
                m_mainViewModel = value;
                OnPropertyChanged("MainViewModel");
            }
        }

        public CSVReaderWindow()
        {
            InitializeComponent();
            DataContext = MainViewModel;
        }

        private void FilePathBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                //Initialize the ViewModel
                MainViewModel = new CSVSheetViewModel(dialog.FileName);
                _this.DataContext = MainViewModel;
                PopulateDataGrid();
            }
        }

        private void PopulateDataGrid()
        {
            MainDataGrid.ItemsSource = MainViewModel.OutputDataView;
        }
    }
}
