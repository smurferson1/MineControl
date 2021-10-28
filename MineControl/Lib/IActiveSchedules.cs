using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineControl.Lib
{
    public interface IActiveSchedules
    {
        public Schedule.Schedule GPU { get; }
        public Schedule.Schedule CPU { get; }
    }
}
