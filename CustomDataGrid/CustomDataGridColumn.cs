using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace CustomDataGrid
{
    public class CustomDataGridColumn : DependencyObject
    {
        public FrameworkElement Header
        {
            get { return (FrameworkElement)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register(nameof(Header), typeof(FrameworkElement), typeof(CustomDataGridColumn));


        public Binding ContentBinding
        {
            get { return (Binding)GetValue(ContentBindingProperty); }
            set { SetValue(ContentBindingProperty, value); }
        }

        public static readonly DependencyProperty ContentBindingProperty =
            DependencyProperty.Register(nameof(ContentBinding), typeof(Binding), typeof(CustomDataGridColumn));

    }
}
