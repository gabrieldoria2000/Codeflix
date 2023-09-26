using Codeflix.Catalog.Application.Interfaces;
using Codeflix.Catalog.Domain.Entity;
using Codeflix.Catalog.Domain.Repository;
using FluentAssertions;
using Moq;
using System.ComponentModel;
using Xunit;
using UseCases = Codeflix.Catalog.Application.UseCases.Category.CreateCategory;

namespace Codeflix.Catalog.UnitTests.Application.CreateCategory;

public class CreateCategoryTest
{
    [Fact(DisplayName= nameof(CreateCategory))]
    [Trait("Application", "CreateCategory - User Cases")]
    public async void CreateCategory()
    {
        var repositoryMock = new Mock<ICategoryRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        var useCase = new UseCases.CreateCategory(repositoryMock.Object, unitOfWorkMock.Object);

        var input = new UseCases.CreateCategoryInput(
            "Category Name", "Category Description", true
            );

        //como é um método assíncrono ele precisa tb de um CancellationToken, para o caso do usuário desistir e cancelar a requisição
        //o none é pq não precisamos de um cancelationtoken real
        var output = await useCase.Handle(input, CancellationToken.None);

        //verifica se foi chamado um Create em que foi passado como parametro um Category (It.IsAny<Category>) 
        //times - quantas vezes foi chamado
        repositoryMock.Verify(repository => repository.Insert(It.IsAny<Category>(), It.IsAny<CancellationToken>()), Times.Once);

        unitOfWorkMock.Verify(uow => uow.Commit(It.IsAny<CancellationToken>()), Times.Once);

        //usando o Fluent Assertion
        output.Should().NotBeNull();
        output.Name.Should().Be("Category Name");
        output.Description.Should().Be("Category Description");
        output.IsActive.Should().Be(true);
        output.Id.Should().NotBeEmpty();
        output.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
    }
}
