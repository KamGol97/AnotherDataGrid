using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CustomDataGrid.Design;
public static class DesignHelper
{
    public const int HowManyHeaders = 100;
    public const int HowManyRows = 10;

    public static Button GetButton(object content)
 => new Button() { Content = content };
}
