using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPost.Models.Abstract
{
    public interface IEntityBase
    {
        string Id { get; set; }
    }
}
