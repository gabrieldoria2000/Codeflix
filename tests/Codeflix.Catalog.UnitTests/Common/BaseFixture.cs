

using Bogus;

namespace Codeflix.Catalog.UnitTests.Common;

public abstract class BaseFixture
{
    public Faker faker { get; set; }

    protected BaseFixture()
    {
        faker = new Faker("pt_BR");
    }
}
