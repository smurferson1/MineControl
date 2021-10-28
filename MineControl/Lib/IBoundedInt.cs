using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineControl.Lib
{
    public interface IBoundedInt
    {
        public int Minimum { get; set; }
        public int Maximum { get; set; }
        public int Value { get; set; }
    }
}
