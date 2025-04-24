﻿using Microsoft.Win32;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using CW_read_xlsx.ViewModel;

namespace CW_read_xlsx
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowVM();
            undo.IsEnabled = false;
            redo.IsEnabled = false;
            if (DataContext is MainWindowVM vm)
            {
                vm.TextChanged += CanUndoRedo;
            }

        }

        private void CanUndoRedo()
        {
            if (textBox1.CanUndo)
            {
                undo.IsEnabled = true;
            }
            else
            {
                undo.IsEnabled = false;
            }
            if (textBox1.CanRedo)
            {
                redo.IsEnabled = true;
            }
            else
            {;
                redo.IsEnabled = false;
            }
        }
        void WindowClosing(object sender, CancelEventArgs e)
        {
            if (DataContext is MainWindowVM vm)
            {
                vm.PromptSave();
            }
        }

        private void Undo(object sender, RoutedEventArgs e)
        {
            textBox1.Undo();
        }

        private void Redo(object sender, RoutedEventArgs e)
        {
            textBox1.Redo();
        }

        private void Copy(object sender, RoutedEventArgs e)
        {
            textBox1.Copy();
        }

        private void Cut(object sender, RoutedEventArgs e)
        {
            textBox1.Cut();
        }

        private void Paste(object sender, RoutedEventArgs e)
        {
            textBox1.Paste();
        }
        private void Clear(object sender, RoutedEventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Help(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            string currentDirectory = Directory.GetCurrentDirectory();
            string fullPath = System.IO.Path.Combine(currentDirectory, "resource", "Справка", "Меню.htm");
            p.StartInfo = new ProcessStartInfo(fullPath)
            {
                UseShellExecute = true
            };
            p.Start();
        }
        private void TaskInfo(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            string currentDirectory = Directory.GetCurrentDirectory();
            string fullPath = System.IO.Path.Combine(currentDirectory, "resource", "Справка", "Постановка задачи.htm");
            p.StartInfo = new ProcessStartInfo(fullPath)
            {
                UseShellExecute = true
            };
            p.Start();
        }
        private void GrammarInfo(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            string currentDirectory = Directory.GetCurrentDirectory();
            string fullPath = System.IO.Path.Combine(currentDirectory, "resource", "Справка", "Грамматика.htm");
            p.StartInfo = new ProcessStartInfo(fullPath)
            {
                UseShellExecute = true
            };
            p.Start();
        }
        private void ClassificationInfo(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            string currentDirectory = Directory.GetCurrentDirectory();
            string fullPath = System.IO.Path.Combine(currentDirectory, "resource", "Справка", "Классификация грамматики.htm");
            p.StartInfo = new ProcessStartInfo(fullPath)
            {
                UseShellExecute = true
            };
            p.Start();
        }
        private void AnalysisInfo(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            string currentDirectory = Directory.GetCurrentDirectory();
            string fullPath = System.IO.Path.Combine(currentDirectory, "resource", "Справка", "Метод анализа.htm");
            p.StartInfo = new ProcessStartInfo(fullPath)
            {
                UseShellExecute = true
            };
            p.Start();
        }
        private void ErrorsFixInfo(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            string currentDirectory = Directory.GetCurrentDirectory();
            string fullPath = System.IO.Path.Combine(currentDirectory, "resource", "Справка", "Диагностика и нейтрализация ошибок.htm");
            p.StartInfo = new ProcessStartInfo(fullPath)
            {
                UseShellExecute = true
            };
            p.Start();
        }
        private void TestsInfo(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            string currentDirectory = Directory.GetCurrentDirectory();
            string fullPath = System.IO.Path.Combine(currentDirectory, "resource", "Справка", "Тестовые примеры.htm");
            p.StartInfo = new ProcessStartInfo(fullPath)
            {
                UseShellExecute = true
            };
            p.Start();
        }
        private void LiteratureInfo(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            string currentDirectory = Directory.GetCurrentDirectory();
            string fullPath = System.IO.Path.Combine(currentDirectory, "resource", "Справка", "Список литературы.htm");
            p.StartInfo = new ProcessStartInfo(fullPath)
            {
                UseShellExecute = true
            };
            p.Start();
        }
        private void SourceCodeInfo(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            string currentDirectory = Directory.GetCurrentDirectory();
            string fullPath = System.IO.Path.Combine(currentDirectory, "resource", "Справка", "Исходный код программы.htm");
            p.StartInfo = new ProcessStartInfo(fullPath)
            {
                UseShellExecute = true
            };
            p.Start();
        }
        private void About(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            string currentDirectory = Directory.GetCurrentDirectory();
            string fullPath = System.IO.Path.Combine(currentDirectory, "resource", "Справка", "О программе.htm");
            p.StartInfo = new ProcessStartInfo(fullPath)
            {
                UseShellExecute = true
            };
            p.Start();
        }
    }
}