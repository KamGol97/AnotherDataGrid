using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomDataGrid.SomeClass;

namespace CustomDataGrid.Design;
public class DesignRowSource
{
    public DesignRowSource(int rowNumer)
    {
        Source = new SapleData();
    }

    public object Source { get; init; }
}
