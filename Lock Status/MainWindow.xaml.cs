using System;
using System.Windows;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Lock_Status
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KeyboardListener _kListener = new KeyboardListener();

        private System.Timers.Timer updateTimer = new System.Timers.Timer();

        public MainWindow()
        {
            InitializeComponent();

            updateTimer.Elapsed += UpdateKeys;
            updateTimer.Interval = 100;
            updateTimer.Enabled = true;
        }


        private void MainLoaded(object sender, RoutedEventArgs e)
        {
            _kListener.KeyDown += new RawKeyEventHandler(KListener_KeyDown);


            CapsCheck.IsChecked = Control.IsKeyLocked(Keys.CapsLock);
            NumCheck.IsChecked = Control.IsKeyLocked(Keys.NumLock);
            ScrollCheck.IsChecked = Control.IsKeyLocked(Keys.Scroll);
        }

        private void KListener_KeyDown(object sender, RawKeyEventArgs e)
        {
            int VKCode = e.VKCode;
            if (VKCode == 20) // Caps
            {
                CapsCheck.IsChecked = Control.IsKeyLocked(Keys.CapsLock);
            }
            if (VKCode == 144) // Num
            {
                NumCheck.IsChecked = Control.IsKeyLocked(Keys.NumLock);
            }
            if (VKCode == 145) // Scroll
            {
                ScrollCheck.IsChecked = Control.IsKeyLocked(Keys.Scroll);
            }
        }

        delegate void ParametrizedMethodInvoker5(int arg);

        private void UpdateKeys(object sender, ElapsedEventArgs e)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(() => UpdateKeys(sender, e));
                return;
            }

            CapsCheck.IsChecked = Control.IsKeyLocked(Keys.CapsLock);
            NumCheck.IsChecked = Control.IsKeyLocked(Keys.NumLock);
            ScrollCheck.IsChecked = Control.IsKeyLocked(Keys.Scroll);
        }

        private void MainClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _kListener.Dispose();
            updateTimer.Stop();
        }
    }
}
