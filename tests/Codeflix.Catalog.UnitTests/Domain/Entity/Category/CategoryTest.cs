
using Codeflix.Catalog.Domain.Exceptions;
using Xunit;
//usamos um ALIAS, para diferenciar o Category do Domain do Category namespace
using DomainEntity = Codeflix.Catalog.Domain.Entity;

namespace Codeflix.Catalog.UnitTests.Domain.Entity.Category;

public class CategoryTest
{
    [Fact(DisplayName =nameof(Instantiate))]
    [Trait("Domain", "Category - Aggregates")]
    public void Instantiate()
    {
        //Triple A - AAA
        //Arrange - dados necessários para fazer os testes
        // os testes vem do negócio (documentação)
        var validData = new
        {
            Name = "category name",
            Description = "category description"
        };

        var DatetimeBefore = DateTime.Now;
        //Act
        var category = new DomainEntity.Category(validData.Name, validData.Description);

        var DatetimeAfter = DateTime.Now;

        //Assert
        Assert.NotNull(category);
        Assert.Equal(validData.Name, category.Name);
        Assert.Equal(validData.Description, category.Description);
        Assert.NotEqual(default(Guid), category.Id);
        Assert.NotEqual(default(DateTime), category.CreatedAt);
        Assert.True(category.CreatedAt > DatetimeBefore);
        Assert.True(category.CreatedAt < DatetimeAfter);
        Assert.True(category.IsActive);
    }

    [Theory(DisplayName = nameof(InstantiateWithActive))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData(true)]
    [InlineData(false)]
    public void InstantiateWithActive(bool isActive)
    {
        //Theory - Diferente do FACT, o THEORY acredita que se vc passa varios valores e em teoria aqueles valores dão certo.
        //Então isso é uma teoria, pois se aqueles valores passaram, em TEORIA, está OK!
        //InlineData - são os valores para os testes
        var validData = new
        {
            Name = "category name",
            Description = "category description"
        };

        var DatetimeBefore = DateTime.Now;
        //Act
        var category = new DomainEntity.Category(validData.Name, validData.Description, isActive);

        var DatetimeAfter = DateTime.Now;

        //Assert
        Assert.NotNull(category);
        Assert.Equal(validData.Name, category.Name);
        Assert.Equal(validData.Description, category.Description);
        Assert.NotEqual(default(Guid), category.Id);
        Assert.NotEqual(default(DateTime), category.CreatedAt);
        Assert.True(category.CreatedAt > DatetimeBefore);
        Assert.True(category.CreatedAt < DatetimeAfter);
        Assert.Equal(category.IsActive, isActive);
    }

    //Regras de Negocio
    //o nome deve ter no minimo 3 caracteres
    //o nome deve ter no maximo 255 caracteres
    //a descrição deve ter no maximo 10.000 caracteres

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameEmpty))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("    ")]
    public void InstantiateErrorWhenNameEmpty(string? name)
    {
        Action action = 
            () => new DomainEntity.Category(name!, "Category Name");

        var exception = Assert.Throws<EntityValidationException>(action);

        Assert.Equal("Name should not be empty or null",exception.Message);
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsnull))]
    [Trait("Domain", "Category - Aggregates")]
    public void InstantiateErrorWhenDescriptionIsnull()
    {
        Action action =
            () => new DomainEntity.Category("Category Description", null);

        var exception = Assert.Throws<EntityValidationException>(action);

        Assert.Equal("Description should not be empty or null", exception.Message);
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameMenor3))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("a")]
    public void InstantiateErrorWhenNameMenor3(string Invalidname)
    {
        Action action =
            () => new DomainEntity.Category(Invalidname,"Category Name size");
        var exception = Assert.Throws<EntityValidationException>(action);

        Assert.Equal("Name should be at least 3 characters long", exception.Message);
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenNameMaior255))]
    [Trait("Domain", "Category - Aggregates")]
    public void InstantiateErrorWhenNameMaior255()
    {
        var invalidName = String.Join(null, Enumerable.Range(1, 256).Select(_ => "a").ToArray());
        Action action =
            () => new DomainEntity.Category(invalidName, "Category Name size");
        var exception = Assert.Throws<EntityValidationException>(action);

        Assert.Equal("Name should be less or equal 255 characters long", exception.Message);
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionMaior10000))]
    [Trait("Domain", "Category - Aggregates")]
    public void InstantiateErrorWhenDescriptionMaior10000()
    {
        var invalidDescription = String.Join(null, Enumerable.Range(1, 10_001).Select(_ => "a").ToArray());
        Action action =
            () => new DomainEntity.Category("Category Name", invalidDescription);
        var exception = Assert.Throws<EntityValidationException>(action);

        Assert.Equal("Description should be less than 10.000 characters long", exception.Message);
    }


    [Fact(DisplayName = nameof(Activate))]
    [Trait("Domain", "Category - Aggregates")]
   public void Activate()
    {
        var validData = new
        {
            Name = "category name",
            Description = "category description"
        };

        var DatetimeBefore = DateTime.Now;
        //Act
        var category = new DomainEntity.Category(validData.Name, validData.Description, true);

        category.Activate();

        //Assert
       
        Assert.True(category.IsActive);
    }

    [Fact(DisplayName = nameof(DeActivate))]
    [Trait("Domain", "Category - Aggregates")]
    public void DeActivate()
    {
        var validData = new
        {
            Name = "category name",
            Description = "category description"
        };

        var DatetimeBefore = DateTime.Now;
        //Act
        var category = new DomainEntity.Category(validData.Name, validData.Description, false);

        category.DeActivate();

        //Assert

        Assert.False(category.IsActive);
    }

    [Fact(DisplayName = nameof(Update))]
    [Trait("Domain", "Category - Aggregates")]
    public void Update()
    {
        var category = new DomainEntity.Category("Category name","Category Description");

        //para simular valores passados pelo usuario
        var newValues = new { Name = "New Name", Description = "new description" };

        category.Update(newValues.Name, newValues.Description);

        Assert.Equal(newValues.Name, category.Name);
        Assert.Equal(newValues.Description, category.Description);
    }

    [Fact(DisplayName = nameof(UpdateOnlyName))]
    [Trait("Domain", "Category - Aggregates")]
    public void UpdateOnlyName()
    {
        var category = new DomainEntity.Category("Category name", "Category Description");

        //para simular valores passados pelo usuario
        var newValues = new { Name = "New Name" };
        var currentDescription = category.Description;

        category.Update(newValues.Name);

        Assert.Equal(newValues.Name, category.Name);
        Assert.Equal(currentDescription, category.Description);
    }

    [Theory(DisplayName = nameof(UpdateErrorWhenNameEmpty))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("    ")]
    public void UpdateErrorWhenNameEmpty(string? name)
    {
        var category = new DomainEntity.Category("Category name", "Category Description");

        Action action =
            () => category.Update(name!);

        var exception = Assert.Throws<EntityValidationException>(action);

        Assert.Equal("Name should not be empty or null", exception.Message);
    }


    [Theory(DisplayName = nameof(UpdateWhenNameMenor3))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("a")]
    public void UpdateWhenNameMenor3(string Invalidname)
    {
        var category = new DomainEntity.Category("Category name", "Category Description");

        Action action =
            () => category.Update(Invalidname);
        var exception = Assert.Throws<EntityValidationException>(action);

        Assert.Equal("Name should be at least 3 characters long", exception.Message);
    }

    [Fact(DisplayName = nameof(UpdateErrorWhenNameMaior255))]
    [Trait("Domain", "Category - Aggregates")]
    public void UpdateErrorWhenNameMaior255()
    {
        var category = new DomainEntity.Category("Category name", "Category Description");

        var invalidName = String.Join(null, Enumerable.Range(1, 256).Select(_ => "a").ToArray());
        Action action =
            () => category.Update(invalidName);
        var exception = Assert.Throws<EntityValidationException>(action);

        Assert.Equal("Name should be less or equal 255 characters long", exception.Message);
    }

    [Fact(DisplayName = nameof(UpdateErrorWhenDescriptionMaior10000))]
    [Trait("Domain", "Category - Aggregates")]
    public void UpdateErrorWhenDescriptionMaior10000()
    {
        var category = new DomainEntity.Category("Category name", "Category Description");

        var invalidDescription = String.Join(null, Enumerable.Range(1, 10_001).Select(_ => "a").ToArray());
        Action action =
            () => category.Update("Category Name", invalidDescription);
        var exception = Assert.Throws<EntityValidationException>(action);

        Assert.Equal("Description should be less than 10.000 characters long", exception.Message);
    }

}
