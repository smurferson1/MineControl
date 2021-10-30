using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineControl.Lib
{
    public interface ISettingsFile
    {
        void Export(string destFilePath, string verb = "");
    }
}
