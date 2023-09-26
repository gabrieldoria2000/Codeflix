

using System.Diagnostics;

namespace Codeflix.Catalog.Application.UseCases.Category.CreateCategory;

public class CreateCategoryOutput
{
    //DTO

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }

    //a maioria dos projetos só retorna ID no OUTPUT do Insert
    public CreateCategoryOutput(Guid id, string name, string description, bool isActive, DateTime createdAt)
    {
        Id = id;
        Name = name;
        Description = description;
        IsActive = isActive;
        CreatedAt = createdAt;
    }
}
