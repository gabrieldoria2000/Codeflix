

using System.Diagnostics;

namespace Codeflix.Catalog.Application.UseCases.Category.CreateCategory;

public class CreateCategoryInput
{
    //DTO

    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }

    public CreateCategoryInput(string name, string? description = null, bool isActive = true)
    {
        Name = name;
        //se o description vier nulo, passamos ele para string vazia
        Description = description ?? "";
        IsActive = isActive;
    }
}
