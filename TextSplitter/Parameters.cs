using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TextSplitter
{
    public class Parameters : INotifyPropertyChanged
    {
        #region Private fields

        private bool separateByParts;
        private bool separateByRows;
        private int separateByCount;
        private bool separateFile;
        private string filePath;
        private string resFileAlias;
        private string resFileExtension;

        #endregion

        #region Initialization

        public Parameters()
        {
            separateByParts = true;
            separateByRows = false;
            separateByCount = 1;
            separateFile = true;
            resFileExtension = "txt";
        }

        #endregion

        #region Public properties

        public bool SeparateByParts
        {
            get { return separateByParts; }
            set
            {
                separateByParts = value;
                NotifyPropertyChanged("SeparateByParts");

                //if (SeparateByRows)
                //    SeparateByRows = false;
            }
        }

        public bool SeparateByRows
        {
            get { return separateByRows; }
            set
            {
                separateByRows = value;
                NotifyPropertyChanged("SeparateByRows");

                //if (SeparateByParts)
                //    SeparateByParts = false;
            }
        }

        public int SeparateByCount
        {
            get { return separateByCount; }
            set
            {
                separateByCount = value;
                NotifyPropertyChanged("SeparateByCount");
            }
        }

        public bool SeparateFile
        {
            get { return separateFile; }
            set
            {
                separateFile = value;
                NotifyPropertyChanged("SeparateFile");
            }
        }

        public string FilePath
        {
            get { return filePath; }
            set
            {
                filePath = value;
                NotifyPropertyChanged("FilePath");
            }
        }

        public string ResFileAlias
        {
            get { return resFileAlias; }
            set
            {
                resFileAlias = value;
                NotifyPropertyChanged("ResFileAlias");
            }
        }

        public string ResFileExtension
        {
            get { return resFileExtension; }
            set
            {
                resFileExtension = value;
                NotifyPropertyChanged("ResFileExtension");
            }
        }

        #endregion

        #region INotifyPropertyChanged implemetation

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
