using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Identity.Domain.Entities
{
    public interface IBaseEntity<TId>
    {
        TId Id { get; set; }

        bool IsExcluded { get; set; }


        Guid? CreatedUser { get; set; }
        Guid? UpdatedUser { get; set; }

        DateTime CreatedDate { get; set; }
        DateTime? UpdatedDate { get; set; }
    }
    public abstract class BaseEntity<TId> : IBaseEntity<TId>
    {
        public TId Id { get; set; }
        public bool IsExcluded { get; set; }
        public Guid? CreatedUser { get; set; }
        public Guid? UpdatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
