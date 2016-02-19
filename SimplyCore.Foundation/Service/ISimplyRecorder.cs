using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimplyCore.Foundation.Service
{
    public interface ISimplyRecorder
    {
        void AddInfo(string key, string value);
        Dictionary<string, string> GetAllInfo();   
    }
}
