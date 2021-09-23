using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterMaker.Domain.Models
{
    public interface ISoftDelete
    {
        public bool Archived { get; set; }
    }
}
