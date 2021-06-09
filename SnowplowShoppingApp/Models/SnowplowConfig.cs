using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnowplowShoppingApp.Models
{
    public class SnowplowConfig
    {
        public string Endpoint { get; set; }
        public string LiteDbStorage { get; set; }
        public string Language { get; set; }
        public string TrackerNamespace { get; set; }
        public string AppId { get; set; }
    }
}
