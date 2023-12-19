using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMenu.DataAccess.Entities
{
    public interface IBaseEntity
    {
        public int Id { get; set; }

        public Guid ExternalId { get; set; }
        public DateTime ModificationTime { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
