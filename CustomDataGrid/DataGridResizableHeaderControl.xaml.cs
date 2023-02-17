using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CustomDataGrid
{
    /// <summary>
    /// Interaction logic for DataGridResizableHeaderControl.xaml
    /// </summary>
    public partial class DataGridResizableHeaderControl : UserControl
    {
        public DataGridResizableHeaderControl()
        {
            InitializeComponent();
        }
        private Point _startPoint;
        private Point _endPoint;
        private bool _subscribed = false;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = GetMousePosition();

            if(!_subscribed)
            {
                Splitter.PreviewMouseMove += TextBlock_PreviewMouseMove;
                _subscribed= true;
            }
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };
        public static Point GetMousePosition()
        {
            var w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);

            return new Point(w32Mouse.X, w32Mouse.Y);
        }

        private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _endPoint = GetMousePosition();
            Unsubscribe();
        }

        private void TextBlock_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            var point = GetMousePosition();

            var cc = _startPoint.X - point.X;
            _startPoint = point;

            this.Width = ActualWidth - cc;
        }

        private void Unsubscribe()
        {
            Splitter.PreviewMouseMove -= TextBlock_PreviewMouseMove;
            _subscribed = false;
        }
        private void Splitter_MouseLeave(object sender, MouseEventArgs e)
        {
            if(_subscribed)
            {
                Unsubscribe();
            }
        }
    }


}
