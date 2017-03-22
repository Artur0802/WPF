using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LogParser
{
    class TagDependencyProperty : DependencyObject
    {
        public string Title
        {
            get
            {
                return (string)GetValue(TitleProperty);
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        public string FileType
        {
            get
            {
                return (string)GetValue(FileTypeProperty);
            }
            set
            {
                SetValue(FileTypeProperty, value);
            }
        }

        public static readonly DependencyProperty TitleProperty;
        public static readonly DependencyProperty FileTypeProperty;

        static TagDependencyProperty()
        {
            FrameworkPropertyMetadata metaData = new FrameworkPropertyMetadata();
            metaData.CoerceValueCallback = new CoerceValueCallback(Formatter);
            TitleProperty = DependencyProperty.Register("TitleProperty", typeof(string), typeof(TagDependencyProperty), metaData, new ValidateValueCallback(ValidTitle));
            FileTypeProperty = DependencyProperty.Register("FileTypeProperty", typeof(string), typeof(TagDependencyProperty));
        }

        private static bool ValidTitle(object obj)
        {
            string data = (string)obj;
            if (obj == null)
                return true;
            else
            return data.Contains("Ok");
        }

        private static object Formatter(DependencyObject depObject, object value)
        {
            return (string)value + " :)";
        }

        
    }
}
