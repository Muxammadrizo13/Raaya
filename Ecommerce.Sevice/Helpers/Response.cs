using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Sevice.Helpers
{
    public class Response<TResult>
    {
        //Header part
        public int StatusCode { get; set; }
        public string Message { get; set; }

        //Body part
        public TResult Value { get; set; }
    }
}
