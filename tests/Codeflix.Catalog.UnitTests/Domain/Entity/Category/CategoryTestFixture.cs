using Codeflix.Catalog.UnitTests.Common;
using Xunit;
using DomainEntity = Codeflix.Catalog.Domain.Entity;

namespace Codeflix.Catalog.UnitTests.Domain.Entity.Category;

public class CategoryTestFixture : BaseFixture
{
    public CategoryTestFixture() : base()
    {

    }

    public string GetValidCategoryName()
    {
        var categoryName = "";

        while (categoryName.Length < 3)
            categoryName = faker.Commerce.Categories(1)[0];

        if (categoryName.Length> 255)
            categoryName = categoryName.Substring(0, 255);

        return categoryName;
    }

    public string GetValidCategoryDescription()
    {

        var categoryDesription = faker.Commerce.ProductDescription();

        if (categoryDesription.Length > 10_000)
            categoryDesription = categoryDesription.Substring(0, 10_000);

        return categoryDesription;
    }


        public DomainEntity.Category GetValidCategory() 
         => new(GetValidCategoryName(), GetValidCategoryDescription());
}


[CollectionDefinition(nameof(CategoryTestFixture))]
public class CategoryTestFixtureCollection : ICollectionFixture<CategoryTestFixture>
{

}
