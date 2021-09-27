using System;
using System.Collections.Generic;
using System.Text;

namespace SampleDDD.Domain.Entities
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; set; }
    }
}
