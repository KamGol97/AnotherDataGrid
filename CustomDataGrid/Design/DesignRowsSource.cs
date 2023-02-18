using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomDataGrid.Design;
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
