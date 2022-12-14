using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogBugz.Exporter.API.APIModels
{
    public class Response<T>
    {
        public T Data { get; set; }

        public List<Error> Errors { get; set; }

        public int? ErrorCode { get; set; }

        public void Check(string ErrorPrefix)
        {
            if (Errors != null && Errors.Count > 0) throw new Exception($"{ErrorPrefix}\nError Code: {ErrorCode}\n{ string.Join("\n", Errors.Select(i => i.Message))}");
        }
    }
}