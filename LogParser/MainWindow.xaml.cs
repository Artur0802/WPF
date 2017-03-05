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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LogParser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //subscribe
            foreach (UIElement guielement in mainLayout.Children)
            {
                //is
                if (guielement is Button)
                {
                    ((Button)guielement).Click += getBtnContent;
                }
            }
        }

        private void operationEqual(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(" = was pressed");
        }

        private void getBtnContent(object sender, RoutedEventArgs e)
        {
            String btnContent = ((Button)sender).Content.ToString();
            this.txtOutput.Text = btnContent;

            String btnContent2 = ((Button)e.OriginalSource).Content.ToString();
            this.txtOutput.Text += " " + btnContent2;
        }
    }
}
