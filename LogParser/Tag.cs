using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LogParser
{
    public class Tag : INotifyPropertyChanged
    {
        private string tagName = "";
        public string Title
        {
            get
            {
                return this.tagName;
            }
            set
            {
                this.tagName = value;
                OnPropertyChanged("Title");
            }
        }

        public Tag(string tagName)
        {
            this.tagName = tagName;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
