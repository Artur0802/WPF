using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace LogParser
{
    /// <summary>
    /// Interaction logic for TagEditor.xaml
    /// </summary>
    public partial class TagEditor : Window
    {
        private Tag editedTag = null;
        
        public TagEditor(Tag tag)
        {
            InitializeComponent();
            this.editedTag = tag;

        }

        private void AcceptChanges(object sender, RoutedEventArgs e)
        {
            if (txtEditedTag.Text != this.editedTag.Title)
            {
                this.editedTag.Title = txtEditedTag.Text;
                MessageBox.Show("Changes done");
                this.Close();
            }
            else
            {
                MessageBox.Show("No changes. Please, edit the tag");
            }
        }

        private void CancelChanges(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Changes canceled");
            this.Close();
        }
    }
}
