using System;
using System.Collections.Generic;

namespace SimplyCore.Foundation.Service
{
    public class SimplyCoreRecorderService : ISimplyRecorder
    {
        private Dictionary<string,string> data;

        public SimplyCoreRecorderService()
        {
            data = new Dictionary<string, string>();
        }

        public void AddInfo(string key,string value)
        {
            data.Add(key,value);
        }

        public Dictionary<string,string> GetAllInfo()
        {
            return data;
        }

    }
}
