using System;
using System.Collections.Generic;
using System.Text;

namespace APIDiff.DataAccess.Models
{
    public class Response
    {
        public string DiffResultType { get; set; }
        public IEnumerable<Diff> Diffs { get; set; }
    }
}
