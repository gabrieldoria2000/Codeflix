using Codeflix.Catalog.Domain.Exceptions;
using Codeflix.Catalog.Domain.SeedWork;
using System.Data;

namespace Codeflix.Catalog.Domain.Entity;

public class Category: AggregateRoot
{
    //não preciso do Guid aqui, pois ela vai herdar de Aggregate, que herda de Entity
    //public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Category(string name, string description, bool isActive = true): base()
    {
        // já coloquei o base(), então o ID já vai estar sendo gerado no Entity
        //Id = Guid.NewGuid();
        Name = name;
        Description = description;
        IsActive = isActive;
        CreatedAt = DateTime.Now;

        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            throw new EntityValidationException($"{nameof(Name)} should not be empty or null");
        }
        if (Description == null)
        {
            throw new EntityValidationException($"{nameof(Description)} should not be empty or null");
        }
        if (Name.Length < 3)
        {
            throw new EntityValidationException($"{nameof(Name)} should be at least 3 characters long");
        }
        if (Name.Length > 255)
        {
            throw new EntityValidationException($"{nameof(Name)} should be less or equal 255 characters long");
        }
        if (Description.Length > 10000)
        {
            throw new EntityValidationException($"{nameof(Description)} should be less than 10.000 characters long");
        }

    }

    public void Activate()
    {
        IsActive = true;
        Validate();
    }

    public void DeActivate()
    {
        IsActive = false;
        Validate();
    }

    public void Update(string Newname, string? Newdescription = null)
    {
        Name= Newname;
        //se for nulo(da esquerda), usa o valor da direita, ou seja, se for nula fica o próprio description da classe
        Description= Newdescription ?? Description;

        Validate();
    }


}
