using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDesktopUI.Helpers
{
    public static class BindingListEx
    {
        public static BindingList<T> ToBindingList<T>(this IEnumerable<T> input)
        {
            return new BindingList<T>(input.ToList());
        }
    }
}
