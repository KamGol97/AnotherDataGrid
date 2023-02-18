using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace CustomDataGrid.Design;
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
