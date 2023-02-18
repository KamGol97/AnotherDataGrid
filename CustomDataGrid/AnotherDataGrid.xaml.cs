using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
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

namespace CustomDataGrid;

/// <summary>
/// Interaction logic for AnotherDataGrid.xaml
/// </summary>
public partial class AnotherDataGrid : UserControl
{

    public const int HowManyHeaders = 100;


    public AnotherDataGrid()
    {
        InitializeComponent();
        Columns = new Design.DesignHeadersSource();
        Items = new Design.DesignRowsSource();       
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        var bind = new Binding
        {
            Source = this,
            Path = new PropertyPath(nameof(Items)),
            Converter = new Converters.BuildRowConverter(),
            ConverterParameter = Columns
        };

        var converter = new Converters.BuildRowConverter();

        var itemsSource = Items.Select(x => converter.Convert(x, typeof(UIElement), Columns, new CultureInfo(""))).ToList();

        MyItemsControl.ItemsSource = itemsSource;
    }




    //---------------------------------------------------------------------------------------------------

    #region Properties

    public ObservableCollection<CustomDataGridColumn> Columns
    {
        get { return (ObservableCollection<CustomDataGridColumn>)GetValue(ColumnsProperty); }
        set { SetValue(ColumnsProperty, value); }
    }

    public ObservableCollection<object> Items
    {
        get { return (ObservableCollection<object>)GetValue(ItemsProperty); }
        set { SetValue(ItemsProperty, value); }
    }

    #endregion

    //---------------------------------------------------------------------------------------------------

    #region Dependecy Properies


    public static readonly DependencyProperty ColumnsProperty =
        DependencyProperty.Register(nameof(Columns), typeof(ObservableCollection<CustomDataGridColumn>), typeof(AnotherDataGrid));

    public static readonly DependencyProperty ItemsProperty =
        DependencyProperty.Register(nameof(Items), typeof(ObservableCollection<object>), typeof(AnotherDataGrid));

    #endregion

    //---------------------------------------------------------------------------------------------------
}