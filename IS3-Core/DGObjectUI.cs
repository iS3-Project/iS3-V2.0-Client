using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iS3.Core
{
    public class DGObjectUI : INotifyPropertyChanged
    {
        #region UI ShowState
        bool _isSelected = false;
        public bool IsSelected
        {
            get { return _isSelected; }
            set {
                if (_isSelected != value)
                {
                   if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs ("IsSelected"));
                    }
                }

                _isSelected = value;
            }
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
