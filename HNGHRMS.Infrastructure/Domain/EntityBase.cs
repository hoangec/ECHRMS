using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

namespace HNGHRMS.Infrastructure.Domain
{
    public abstract class EntityBase
    {
        private List<BrokenRule> _brokenRules = new List<BrokenRule>();
       
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int Id { get; set; }

        public abstract void Validate();

        public IEnumerable<BrokenRule> GetBrokenRules()
        {
            _brokenRules.Clear();
            Validate();
            return _brokenRules;
        }

        protected void AddBrokenRule(BrokenRule brokenRule)
        {
            _brokenRules.Add(brokenRule);
        }

        public override bool Equals(object entity)
        {
            var compareTo = entity as EntityBase;
            if (ReferenceEquals(compareTo, null))
                return false;
            if (ReferenceEquals(this, compareTo))
                return true;
            if (this.GetRealType() != compareTo.GetRealType())
                return false;
            if (!IsTransient() && compareTo.IsTransient() && Id == compareTo.Id)
                return true;
            return false;
        }       

        public static bool operator ==(EntityBase entity1,EntityBase entity2)
        {
            if (ReferenceEquals(entity1, null) && ReferenceEquals(entity2, null))
                return true;
            if (ReferenceEquals(entity1, null) || ReferenceEquals(entity2, null))
                return false;
            
            return entity1.Equals(entity2);
        }

        public static bool operator !=(EntityBase entity1,EntityBase entity2)
        {
            return (!(entity1 == entity2));
        }

        public override int GetHashCode()
        {
            return (this.GetRealType().ToString() + this.Id).GetHashCode();
        }
        public virtual bool IsTransient()
        {
            return this.Id == 0;            
        }

        public virtual Type GetRealType()
        {
            return ObjectContext.GetObjectType(this.GetType());
        }

    }

}
