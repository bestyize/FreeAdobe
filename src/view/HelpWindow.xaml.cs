using FreeAdobe.src.model;
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

namespace FreeAdobe.src.view
{
    /// <summary>
    /// HelpWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HelpWindow : Window
    {
        private string title;
        private string content;
        private NotifyEventListener listener;
        public HelpWindow(string title, string content,NotifyEventListener listener)
        {
            this.title = title;
            this.content = content;
            this.listener = listener;
            InitializeComponent();
            initView();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            base.DragMove();
        }

        private void initView() {
            lbTitle.Content = title;
            tbContent.Text = content;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (listener != null)
            {
                listener.onOkClicked(tbContent.Text);
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (listener != null)
            {
                listener.onCancelClicked("cancel");
            }
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
