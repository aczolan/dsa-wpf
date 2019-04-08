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

        #region Event Handlers

        private void FilePathBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                //Initialize the ViewModel
                MainViewModel = new CSVSheetViewModel(dialog.FileName);
                _this.DataContext = MainViewModel;

                //Populate the DataGrid
                MainDataGrid.ItemsSource = MainViewModel.OutputDataView;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //Write to the CSV file when the user clicks "Save"
            MainViewModel.WriteToCSVFile(MainViewModel.FilePath);
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                //Pull the row and column from the DataGrid

                var column = e.Column as DataGridBoundColumn;
                if (column != null)
                {
                    var bindingPath = (column.Binding as Binding).Path.Path;

                    int columnIndex = MainViewModel.ChildRows[0].CellValues.FindIndex(s => s == bindingPath);
                    int rowIndex = e.Row.GetIndex() + 1;
                    var el = e.EditingElement as TextBox;
                    string newVal = el.Text;

                    //Set the value of the cell at that row and column
                    MainViewModel.SetCellValue(columnIndex, rowIndex, el.Text);

                    //MessageBox.Show("Col: " + columnIndex + " Row: " + rowIndex + " New Val: " + newVal);
                }
            }
        }

        #endregion

        public CSVReaderWindow()
        {
            InitializeComponent();
            DataContext = MainViewModel;

            //Hook up cell editing event handler
            MainDataGrid.CellEditEnding += DataGrid_CellEditEnding;
        }
    }
}
