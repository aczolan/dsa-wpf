using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVReader
{
    public class CSVSheetViewModel : INotifyPropertyChanged
    {
        #region PropertyChanged stuff
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region Properties

        private string m_fileText = "";

        private bool m_fileLoaded;
        public bool FileLoaded
        {
            get => m_fileLoaded;
            set
            {
                m_fileLoaded = value;
                OnPropertyChanged("FileLoaded");
            }
        }

        public DataTable OutputDataTable
        {
            get; set;
        } = new DataTable();

        public DataView OutputDataView
        {
            get => new DataView(OutputDataTable);
        }

        private string m_filePath = "";
        public string FilePath
        {
            get => m_filePath;
            set
            {
                m_filePath = value;
                OnPropertyChanged("FilePath");
            }
        }

        private List<CSVRowViewModel> m_childRows = new List<CSVRowViewModel>();
        public List<CSVRowViewModel> ChildRows
        {
            get => m_childRows;
            set
            {
                m_childRows = value;
                OnPropertyChanged("ChildRows");
            }
        }

        #endregion

        #region Methods

        public string GetCellValue(int column, int row)
        {
            return ChildRows[row].GetCellValue(column);
        }

        public void SetCellValue(int column, int row, string newValue)
        {
            ChildRows[row].SetCellValue(column, newValue);
        }

        private void loadFromFile(string path)
        {
            FilePath = path;
            m_fileText = File.ReadAllText(path);
            bool headerRowCreated = false;

            //Split the file text at every new line
            // "\r\n", "\r", and "\n" are different ways of indicating a new line in plain text
            string[] lines = m_fileText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            if (lines.Length > 1)
            {
                //Find the row with the longest number of cells in it.
                int maxNumCells = lines.Max(s => s.Split(',').Length);

                //Create child viewmodels for every line in the csv, including the header line
                foreach (var line in lines)
                {
                    CSVRowViewModel toAdd = new CSVRowViewModel(line, maxNumCells);
                    if (!headerRowCreated)
                    {
                        toAdd.IsHeaderRow = true;
                        headerRowCreated = true;
                    }
                    ChildRows.Add(toAdd);
                }

                //The file was loaded and parsed successfully!
                FileLoaded = true;

                //Create a new DataTable based on this new file
                fillDataTable();
            }
        }

        private void fillDataTable()
        {
            //Create a column for each header we find in the first row
            foreach (var header in ChildRows[0].CellValues)
            {
                OutputDataTable.Columns.Add(header, typeof(string));
            }

            //Parse the rest of the rows
            List<CSVRowViewModel> theRestOfTheRows = new List<CSVRowViewModel>(ChildRows);
            theRestOfTheRows.RemoveAt(0);
            foreach (var row in theRestOfTheRows)
            {
                DataRow dr = OutputDataTable.NewRow();
                for (int i = 0; i < row.CellValues.Capacity; i++)
                {
                    dr[i] = row.GetCellValue(i);
                }
                OutputDataTable.Rows.Add(dr);
            }

            //OutputDataTable and OutputDataView are now filled and ready to be accessed
        }

        public void WriteToCSVFile(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                //Create the new csv file
                using (StreamWriter sw = File.CreateText(path))
                {
                    string fullText = this.ToString();
                    sw.Write(fullText);
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public override string ToString()
        {
            string ret = "";
            foreach (var row in ChildRows)
            {
                ret += row + "\n";
            }
            return ret;
        }

        #endregion
        
        public CSVSheetViewModel()
        {
        }

        public CSVSheetViewModel(string filepath)
        {
            loadFromFile(filepath);
        }
    }

    public class CSVRowViewModel : INotifyPropertyChanged
    {
        #region PropertyChanged stuff
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region Properties

        public bool IsHeaderRow
        {
            get; set;
        }

        private List<string> m_cellValues = new List<string>();
        public List<string> CellValues
        {
            get => m_cellValues;
            set
            {
                m_cellValues = value;
                OnPropertyChanged("CellValues");
            }
        }

        #endregion

        #region Methods

        public string GetCellValue(int index)
        {
            return CellValues[index];
        }

        public void SetCellValue(int index, string newValue)
        {
            CellValues[index] = newValue;
        }

        private void resizeRow(int newLength)
        {
            if (CellValues.Capacity < newLength)
            {
                List<string> oldValues = new List<string>(CellValues);
                CellValues = new List<string>(newLength);
                foreach (var val in oldValues)
                {
                    CellValues.Add(val);
                }
            }
            else if (CellValues.Capacity > newLength)
            {
                List<string> oldValues = new List<string>();
                for (int i = 0; i < newLength; i++)
                {
                    oldValues.Add(CellValues[i]);
                }
                CellValues = new List<string>(newLength);
                foreach (var val in oldValues)
                {
                    CellValues.Add(val);
                }
            }
        }

        public override string ToString()
        {
            string ret = "";
            for (int i = 0; i < CellValues.Capacity; i++)
            {
                ret += CellValues[i];
                if (i != CellValues.Capacity - 1)
                {
                    ret += ",";
                }
            }
            return ret;
        }

        #endregion

        public CSVRowViewModel(string initRow, int rowLength)
        {
            var rows = initRow.Split(',');
            CellValues = new List<string>(rows);
            if (CellValues.Capacity != rowLength)
            {
                resizeRow(rowLength);
            }
        }
    }
}
