using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineControl.Lib
{
    public interface IStats
    {
        public string Get(string rowText);

        public void Set(string statName, string value);
    }
}
