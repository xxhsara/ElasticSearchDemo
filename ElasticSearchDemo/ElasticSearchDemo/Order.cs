using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchDemo
{
    public class Order
    {
        public string Id { get; set; }

        [Keyword]
        public string Name { get; set; }
    }
}
