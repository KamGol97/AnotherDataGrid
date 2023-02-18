using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomDataGrid.SomeClass;
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
