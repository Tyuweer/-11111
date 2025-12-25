using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.WPF.ViewModel
{
    public class GroupHeader
    {
        public string Author { get; set; }
        public int BookCount { get; set; }

        public override string ToString() =>
            $"{Author} ({BookCount} книг)";
    }
}
