

namespace Codeflix.Catalog.Domain.SeedWork;

public abstract class AggregateRoot : Entity
{
    //base - o construtor aqui vai usar o construtor base da Entity
    protected AggregateRoot(): base() { }
}
