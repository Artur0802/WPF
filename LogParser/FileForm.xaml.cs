using Microsoft.Win32;
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
using System.IO;

namespace LogParser
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            FillTags();
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialogbox = new OpenFileDialog();
            dialogbox.Multiselect = false;
            dialogbox.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialogbox.Title = "Выбрать файл";
            dialogbox.Filter = "Text file (*.txt)|*.txt|Pictures (.jpg, *.png, *.bmp)|*.jpg; *.png; *.bmp";
            if (dialogbox.ShowDialog() == true)
            {
                //MessageBox.Show(dialogbox.FileName);
                txtFilePath.Text = dialogbox.FileName;
            }
            else
            {
                MessageBox.Show("File was not selected");
            }
        }

        private void FillTags()
        {
            List<Tag> tags = new List<LogParser.Tag>();
            tags.Add(new Tag("Exitcode:"));
            tags.Add(new Tag("Errormessages"));
            lstTags.SelectionMode = SelectionMode.Extended;
            lstTags.ItemsSource = tags;
        }

        private void ShowSelected(object sender, RoutedEventArgs e)
        {
            foreach (object item in lstTags.SelectedItems)
            {
                MessageBox.Show(((Tag)item).Title);
            }
        }

        private void SelectNext(object sender, RoutedEventArgs e)
        {
            if (lstTags.SelectedIndex < 0)
                return;
            if (lstTags.SelectedIndex == lstTags.Items.Count - 1)
                lstTags.SelectedIndex = 0;
            else
                lstTags.SelectedIndex++;
        }

        private void SelectDeselect(object sender, RoutedEventArgs e)
        {
            int countofSelected = lstTags.SelectedItems.Count;
            int countofItems = lstTags.Items.Count;
            if (countofItems == countofSelected)
                lstTags.SelectedItems.Clear();
            if (countofSelected == 0)
                foreach (object item in lstTags.Items)
                    lstTags.SelectedItems.Add(item);
        }

        private void Deselect(object sender, RoutedEventArgs e)
        {
            if (lstTags.SelectedItem != null)
                lstTags.SelectedItems.Remove(lstTags.SelectedItem);
        }

        private void EditTag(object sender, RoutedEventArgs e)
        {
            if (lstTags.SelectedItem != null)
            {
                TagEditor tagEditor = new TagEditor((Tag)lstTags.SelectedItem);
                tagEditor.Owner = this;
                tagEditor.Show();
            }
        }
    }
}
