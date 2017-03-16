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
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace LogParser
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        /*
        Сделать дерево тэгов
        Сделать кнопки:
        1)Открыть все дерево
        2)Закрыть все дерево
        3)Открыть каждый второй элемент уровня
        4)Закрыть каждый третий элемент уровня 
        */

        List<Tag> tags = null;

        public Window1()
        {
            InitializeComponent();
            FillTags();
            FillFiles();
            FillTabs();
            FillListView();
            CreateTreeView();
        }

        private void CreateTreeView()
        {
            TreeViewItem item = null;

            item = new TreeViewItem();
            item.Header = "North America";

            TreeViewItem inneritem = null;
            inneritem = new TreeViewItem();
            inneritem.Header = "USA";
            item.Items.Add(inneritem);
            inneritem = new TreeViewItem();
            inneritem.Header = "Canada";
            item.Items.Add(inneritem);
            inneritem = new TreeViewItem();
            inneritem.Header = "Mexico";
            item.Items.Add(inneritem);

            treeView.Items.Add(item);

            item = new TreeViewItem();
            item.Header = "South America";

            inneritem = null;
            inneritem = new TreeViewItem();
            inneritem.Header = "Argentina";
            item.Items.Add(inneritem);
            inneritem = new TreeViewItem();
            inneritem.Header = "Brasil";
            item.Items.Add(inneritem);
            inneritem = new TreeViewItem();
            inneritem.Header = "Columbia";
            item.Items.Add(inneritem);

            treeView.Items.Add(item);
            treeView.MouseDoubleClick += AddChild;

            foreach (TreeViewItem parentItem in treeView.Items)
            {
                Subscriber(parentItem);
            }
        }

        private void FillListView()
        {
            
        }

        private void FillTabs()
        {
            tbCtrl.Items.Add("TXT");
            tbCtrl.Items.Add("XML");
        }

        private void FillFiles()
        {
            cmbbxFiles.Items.Add("Find file");
            cmbbxFiles.Items.Add("Delete file");
            cmbbxFiles.Items.Add("Edit file");
            cmbbxFiles.IsEditable = true;
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
            this.tags = new List<Tag>();
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

        private void RemoveTag(object sender, RoutedEventArgs e)
        {
            if (lstTags.SelectedItem != null)
            {
                tags.Remove((Tag)lstTags.SelectedItem);
                lstTags.ItemsSource = tags;
            }
        }

        private void ISearcher(object sender, TextCompositionEventArgs e)
        {
            Regex rgx = new Regex('^' + e.Text + ".*", RegexOptions.IgnoreCase);


            foreach (string item in cmbbxFiles.Items)
            {
                MatchCollection rezult = rgx.Matches(item);

                if (rezult.Count > 0)
                {
                    cmbbxFiles.SelectedItem = item;
                    break;
                }
            }

            e.Handled = true;
        }

        private void ShowTreeSelected(object sender, RoutedEventArgs e)
        {
            TreeViewItem currentItem = (TreeViewItem)sender;
            
            MessageBox.Show(currentItem.Header.ToString());
            e.Handled = true;
        }

        private void Subscriber(TreeViewItem currentItem)
        {
            currentItem.Selected += ShowTreeSelected;
            currentItem.MouseDoubleClick += AddChild;
            if (currentItem.Items.Count > 0)
                foreach (TreeViewItem child in currentItem.Items)
                    Subscriber(child);
        }

        private void AddChild(object sender, RoutedEventArgs e)
        {
            if (sender is TreeView)
            {
                ((TreeView)sender).Items.Add("New Child");
            }
            else
            {
                ((TreeViewItem)sender).Items.Add("New Child");
            }
        }
    }
}
