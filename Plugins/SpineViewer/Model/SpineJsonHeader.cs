using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpineViewer.Model
{

    public class SpineJsonHeader
    {
        public SpineJsonHeaderSkeleton Skeleton { get; set; }
    }

    public class SpineJsonHeaderSkeleton
    {
        public string Hash { get; set; }
        [JsonProperty("spine")]
        public string Version { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string Images { get; set; }
        public string Audio { get; set; }
    }
}
