using System.ComponentModel.DataAnnotations;

namespace Mhd.Framework.Efcore
{
    public interface IEntity { }
    public abstract class Entity : IEntity { }


    public interface IEntity<T> : IEntity
    {
        T Id { get; }
    }
    public abstract class Entity<T> : IEntity<T>
    {
        protected T _id;

        [Key]
        public T Id => _id;

        protected static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
                throw new BusinessRuleValidationException(rule);
        }
    }
}
