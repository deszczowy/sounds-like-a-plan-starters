using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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

namespace Starters
{
    public partial class MainWindow : Window
    {
        private Process process;

        public MainWindow()
        {
            InitializeComponent();
            DrawerHide();
            this.process = new Process();
            this.DataContext = this.process;
        }

        private void DrawerShow()
        {
            Drawer.Visibility = Visibility.Visible;
        }

        private void DrawerHide()
        {
            Drawer.Visibility = Visibility.Hidden;
        }

        private void MenuClick(object sender, RoutedEventArgs e)
        {
            DrawerShow();
        }

        private void MenuClose(object sender, RoutedEventArgs e)
        {
            DrawerHide();
        }

        private void MenuOpen(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Texts (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true) {
                this.process.openFile(openFileDialog.FileName);
            }
            DrawerHide();
        }

        private void MenuSaveAs(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
                this.process.saveFile(saveFileDialog.FileName);
            DrawerHide();
        }

        private void MenuSave(object sender, RoutedEventArgs e)
        {
            if (this.process.isNew())
            {
                 MenuSaveAs(sender, e);
            }
            else
            {
                this.process.saveFile("");
            }
            DrawerHide();
        }

        private void MenuNew(object sender, RoutedEventArgs e)
        {
            this.process.newFile();
            DrawerHide();
        }

        private void MenuQuit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void DockPanelClick(object sender, MouseButtonEventArgs e)
        {
            DrawerHide();
        }
    }
}
