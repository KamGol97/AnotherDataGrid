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
        Columns = new DesignHeadersSource();
        Items = new DesignRowsSource();

        this.Loaded += AnotherDataGrid_Loaded;
    }

    private void AnotherDataGrid_Loaded(object sender, RoutedEventArgs e)
    {
        var bind = new Binding
        {
            Source = this,
            Path = new PropertyPath(nameof(Items)),
            Converter = new BuildRowConverter(),
            ConverterParameter = Columns
        };

        var converter = new BuildRowConverter();

        var itemsSource = Items.Select(x => converter.Convert(x, typeof(UIElement), Columns, new CultureInfo(""))).ToList();

        MyItemsControl.ItemsSource = itemsSource;
    }

    public ObservableCollection<CustomDataGridColumn> Columns
    {
        get { return (ObservableCollection<CustomDataGridColumn>)GetValue(ColumnsProperty); }
        set { SetValue(ColumnsProperty, value); }
    }

    public static readonly DependencyProperty ColumnsProperty =
        DependencyProperty.Register(nameof(Columns), typeof(ObservableCollection<CustomDataGridColumn>), typeof(AnotherDataGrid));
    public ObservableCollection<object> Items
    {
        get { return (ObservableCollection<object>)GetValue(ItemsProperty); }
        set { SetValue(ItemsProperty, value); }
    }

    public static readonly DependencyProperty ItemsProperty =
        DependencyProperty.Register(nameof(Items), typeof(ObservableCollection<object>), typeof(AnotherDataGrid));

}


public class BuildRowConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        object returnObject = null;

        if (parameter is IList<CustomDataGridColumn> columnsList

            )
        {
            var grid = new Grid();


            foreach (var col in columnsList)
            {
                var colDef = new ColumnDefinition();
                colDef.Width = new GridLength(col.Header.ActualWidth, GridUnitType.Pixel);
                grid.ColumnDefinitions.Add(colDef);
            }

            for (int i = 0; i < columnsList.Count; i++)
            {
                var curCol = columnsList[i];
                var properties = value.GetType().GetProperties().ToList();

                var index = properties.FindIndex(x => x.Name == curCol.ContentBinding.Path.Path);
                if (index == -1)
                    continue;
                ;

                var property = properties[index];

                var itemValue = property.GetValue(value);

                var presenter = new ContentPresenter();
                presenter.Content = itemValue;
                presenter.HorizontalAlignment = HorizontalAlignment.Stretch;
                presenter.Height = 15;

                Grid.SetColumn(presenter, i);

                grid.Children.Add(presenter);
            }

            return grid;
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}

public class DesignHeadersSource : ObservableCollection<CustomDataGridColumn>
{
    public DesignHeadersSource()
    {
        var list = Enumerable
            .Range(0, DesignHelper.HowManyHeaders)
            .Select(i =>
            {
                var aa = new DataGridResizableHeaderControl();
                aa.MainContentPresenter.Content = DesignHelper.GetButton("i'm header " + i.ToString());
                return new CustomDataGridColumn()
                {
                    Header = aa,
                    ContentBinding = new Binding { Path = new PropertyPath($"Column{i}") }
                };
            })
            .ToList();

        list.ForEach(x => Add(x));
    }
}

public static class DesignHelper
{
    public const int HowManyHeaders = 100;
    public const int HowManyRows = 10;

    public static Button GetButton(object content)
 => new Button() { Content = content };
}

public class DesignRowSource
{
    public DesignRowSource(int rowNumer)
    {


        Source = new SapleData();
    }


    public object Source { get; init; }
}

public class SapleData
{
    public string? Column1 { get; set; }
    public string? Column2 { get; set; }
    public string? Column3 { get; set; }
    public string? Column5 { get; set; }
    public string? Column6 { get; set; }
    public string? Column10 { get; set; }
    public string? Column20 { get; set; }
    public string? Column21 { get; set; }
    public string? Column22 { get; set; }

    public SapleData()
    {
        foreach (var item in this.GetType().GetProperties())
        {
            item.SetValue(this, "some column");
        }
    }
}

public class DesignRowsSource : ObservableCollection<object>
{
    public DesignRowsSource()
    {
        var list = Enumerable
            .Range(0, DesignHelper.HowManyRows)
            .Select(i => new DesignRowSource(i).Source)
            .ToList();

        list.ForEach(Add);
    }
}


