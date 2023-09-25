

namespace Codeflix.Catalog.Domain.SeedWork;

public abstract class Entity
{
    //classe abstract - Não vai instanciar. Só serve para eu instanciar outras dela
    //protected, porque as classes que herdarão dessa classe poderão alterar o ID
    public Guid Id { get; protected set; }

    //Construtor
    protected Entity()=> Id = Guid.NewGuid();

}
