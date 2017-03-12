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
        string leftNum = "";
        string action = "";
        string rightNum = "";
        
        public MainWindow()
        {
            InitializeComponent();

            //subscribe
            foreach (UIElement guielement in digitGrid.Children)
            {
                //is
                if (guielement is Button)
                {
                    ((Button)guielement).Click += digitBtnHandler;
                }
            }
        }

        private void operationBtnHandler(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            String operation = ((Button)e.OriginalSource).Content.ToString();
            switch (operation)
            {
                case "=":
                    MakeActions();
                    txtBox.Text = this.rightNum.ToString();
                    break;
                case "1/x":
                    if (this.rightNum == "" && this.action == "")
                    {
                        txtBox.Text = (1 / Single.Parse(this.leftNum)).ToString();
                        this.leftNum = (1 / Single.Parse(this.leftNum)).ToString();
                    }
                    else
                    {
                        if (this.rightNum == "")
                        {
                            this.rightNum = (1 / Single.Parse(this.leftNum)).ToString();
                            txtBox.Text += this.rightNum;
                        }
                        else
                        {
                            txtBox.Text = txtBox.Text.Remove(txtBox.Text.LastIndexOf(this.rightNum)) + (1 / Single.Parse(this.rightNum)).ToString();
                            this.rightNum = (1 / Single.Parse(this.rightNum)).ToString();
                        }
                    }
                    break;
                case "%":
                    txtBox.Text = txtBox.Text.Remove(txtBox.Text.LastIndexOf(this.rightNum)) + (Single.Parse(this.leftNum) / 100 * Single.Parse(this.rightNum)).ToString();
                    this.rightNum = (Single.Parse(this.leftNum) / 100 * Single.Parse(this.rightNum)).ToString();
                    break;
            }
        }

        private void digitBtnHandler(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            String btnContent = ((Button)e.OriginalSource).Content.ToString();
            txtBox.Text += btnContent;
            float num;
            bool valid = Single.TryParse(btnContent, out num);
            if (valid == true)
            {
                if (this.action == "")
                {
                    this.leftNum += btnContent;
                }
                else
                {
                    this.rightNum += btnContent;
                    if (this.rightNum.Contains(',') == false)
                        comma.IsEnabled = true;
                }
            }
            else
            {
                if (btnContent == ",")
                {
                    comma.IsEnabled = false;

                    if (this.leftNum == "")
                    {
                        this.leftNum = '0' + btnContent;
                    }
                    else
                    {
                        if (this.action == "" && this.rightNum == "")
                            this.leftNum += btnContent;
                        else
                        {
                            if (this.action != "" && this.rightNum == "")
                                this.rightNum = '0' + btnContent;
                            else
                                this.rightNum = '0' + btnContent;
                        }
                    }
                }
                else
                {
                    if (this.rightNum != "")
                    {
                        MakeActions();
                        this.leftNum = this.rightNum;
                        this.rightNum = "";
                    }
                    action = btnContent;
                }
            }
        }

        private void memoryBtnHandler(object sender, RoutedEventArgs e)
        { }

        private void redactionBtnHandler(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            String btnContent = ((Button)e.OriginalSource).Content.ToString();
            switch (btnContent)
            {
                case "C":
                    this.leftNum = "";
                    this.action = "";
                    this.rightNum = "";
                    txtBox.Text = "";
                    break;
                case "CE":
                    
                    break;
                case "←":
                    
                    break;
                case "±":
                    
                    break;
            }
        }

        private void MakeActions()
        {
            float num1 = Single.Parse(this.leftNum);
            float num2 = Single.Parse(this.rightNum);
            switch (this.action)
            {
                case "+":
                    this.rightNum = (num1 + num2).ToString();
                    break;
                case "-":
                    this.rightNum = (num1 - num2).ToString();
                    break;
                case "*":
                    this.rightNum = (num1 * num2).ToString();
                    break;
                case "/":
                    this.rightNum = (num1 / num2).ToString();
                    break;
            }
        }

        private void ChangeMode(object sender, RoutedEventArgs e)
        {
            e.Handled = true;

            if (((MenuItem)e.OriginalSource).Header.ToString() == "Инженерный")
            {
                engineeringLayout.Width = new GridLength(100, GridUnitType.Star);
            }
            if (((MenuItem)e.OriginalSource).Header.ToString() == "Обычный")
            {
                engineeringLayout.Width = new GridLength(0, GridUnitType.Star);
            }
        }
    }
}
