using APIDiff.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace APIDiff.DataAccess
{
    public class DataDiff
    {
        public IEnumerable<Left> Left { get; set; }
        public IEnumerable<Right> Right { get; set; }
    }
}
