using Microsoft.AspNet.Hosting;
using SimplyCore.Foundation.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebsite.Models
{
    public class ItemsModel
    {
        private IHostingEnvironment _env;
        private ISimplyRecorder _service;

        public ItemsModel(ISimplyRecorder service,IHostingEnvironment env)
        {
            _service = service;
            _env = env;
        }
        
        public int id { get; set; }

        public string UseService()
        {
            _service.AddInfo("Model","In Model");
            return "return";
        }

    }
}
