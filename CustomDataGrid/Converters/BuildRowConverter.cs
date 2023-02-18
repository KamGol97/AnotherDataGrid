using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows;

namespace CustomDataGrid.Converters;
public class BuildRowConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        object returnObject = null;

        if (parameter is IList<CustomDataGridColumn> columnsList)
        {
            Border rowBorder = new Border();
            var borderThickness = 0.3;
            rowBorder.BorderThickness = new Thickness(borderThickness);
            rowBorder.BorderBrush = Brushes.Black;

            var rowGrid = new Grid();
            rowBorder.Child = rowGrid;

            foreach (var col in columnsList)
            {
                var colDef = new ColumnDefinition();
                colDef.Width = new GridLength(col.Header.ActualWidth, GridUnitType.Pixel);

                var colBinding = new Binding
                {
                    Source = col.Header,
                    Path = new PropertyPath(nameof(col.Header.ActualWidth)),
                    Converter = new ActualWidthToGridLenghtConverter(),
                    Mode = BindingMode.OneWay
                };
                colDef.SetBinding(ColumnDefinition.WidthProperty, colBinding);

                rowGrid.ColumnDefinitions.Add(colDef);
            }

            for (int i = 0; i < columnsList.Count; i++)
            {
                var curCol = columnsList[i];
                var properties = value.GetType().GetProperties().ToList();

                var index = properties.FindIndex(x => x.Name == curCol.ContentBinding.Path.Path);


                var presenterBorder = new Border();
                presenterBorder.BorderThickness = new Thickness(0, 0, borderThickness, 0);
                presenterBorder.BorderBrush = Brushes.Black;
                Grid.SetColumn(presenterBorder, i);
                rowGrid.Children.Add(presenterBorder);

                if (index == -1)
                    continue;


                var property = properties[index];

                var itemValue = property.GetValue(value);


                var presenter = new ContentPresenter();
                presenterBorder.Child = presenter;


                var contentBinding = new Binding
                {
                    Source = value,
                    Path = new PropertyPath(property.Name),
                    Mode = BindingMode.TwoWay
                };
                //presenter.Content = itemValue;
                presenter.SetBinding(ContentPresenter.ContentProperty, contentBinding);
                presenter.HorizontalAlignment = HorizontalAlignment.Center;
                presenter.Height = 15;


            }

            return rowBorder;
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}
