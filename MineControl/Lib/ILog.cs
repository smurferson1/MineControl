using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineControl.Lib
{
    public interface ILog
    {
        public void Append(string entry, LogType logType = LogType.Info, LogSource logSource = LogSource.Internal);
    }
}
