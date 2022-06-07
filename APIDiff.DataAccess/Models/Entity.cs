using System;
using System.Collections.Generic;
using System.Text;

namespace APIDiff.DataAccess.Models
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public string Data { get; set; }
    }
}
