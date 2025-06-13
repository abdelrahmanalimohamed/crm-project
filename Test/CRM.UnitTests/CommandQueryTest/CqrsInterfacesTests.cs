using Crm.Application.Abstraction;
using Moq;

namespace CRM.UnitTests.CommandQueryTest;

public class CqrsInterfacesTests
{
	[Fact]
	public void ICommand_ShouldBeImplementableWithoutResult()
	{
		// Arrange & Act
		var command = new UpdateUserCommand { UserId = 1, Name = "Updated Name" };

		// Assert
		Assert.IsAssignableFrom<ICommand>(command);
		Assert.Equal(1, command.UserId);
		Assert.Equal("Updated Name", command.Name);
	}

	[Fact]
	public void ICommand_ShouldBeImplementableWithResult()
	{
		// Arrange & Act
		var command = new CreateUserCommand { Name = "John Doe", Email = "john@example.com" };

		// Assert
		Assert.IsAssignableFrom<ICommand>(command);
		Assert.IsAssignableFrom<ICommand<int>>(command);
		Assert.Equal("John Doe", command.Name);
		Assert.Equal("john@example.com", command.Email);
	}

	[Fact]
	public void IQuery_ShouldBeImplementableWithResult()
	{
		// Arrange & Act
		var query = new GetUserQuery { UserId = 123 };

		// Assert
		Assert.IsAssignableFrom<IQuery<User>>(query);
		Assert.Equal(123, query.UserId);
	}

	[Fact]
	public async Task ICommandHandler_ShouldHandleVoidCommands()
	{
		// Arrange
		var handler = new UpdateUserCommandHandler();
		var command = new UpdateUserCommand { UserId = 1, Name = "Updated Name" };
		var cancellationToken = CancellationToken.None;

		// Act & Assert (should not throw)
		await handler.Handle(command, cancellationToken);

		// Verify handler implements correct interface
		Assert.IsAssignableFrom<ICommandHandler<UpdateUserCommand>>(handler);
	}

	[Fact]
	public async Task ICommandHandler_ShouldHandleCommandsWithResult()
	{
		// Arrange
		var handler = new CreateUserCommandHandler();
		var command = new CreateUserCommand { Name = "John Doe", Email = "john@example.com" };
		var cancellationToken = CancellationToken.None;

		// Act
		var result = await handler.Handle(command, cancellationToken);

		// Assert
		Assert.True(result > 0);
		Assert.IsAssignableFrom<ICommandHandler<CreateUserCommand, int>>(handler);
	}

	[Fact]
	public async Task IQueryHandler_ShouldHandleQueriesWithResult()
	{
		// Arrange
		var handler = new GetUserQueryHandler();
		var query = new GetUserQuery { UserId = 123 };
		var cancellationToken = CancellationToken.None;

		// Act
		var result = await handler.Handle(query, cancellationToken);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(123, result.Id);
		Assert.Equal("Test User", result.Name);
		Assert.Equal("test@example.com", result.Email);
		Assert.IsAssignableFrom<IQueryHandler<GetUserQuery, User>>(handler);
	}

	[Fact]
	public async Task IQueryHandler_ShouldHandleCollectionResults()
	{
		// Arrange
		var handler = new GetUsersQueryHandler();
		var query = new GetUsersQuery { PageSize = 10, PageNumber = 1 };
		var cancellationToken = CancellationToken.None;

		// Act
		var result = await handler.Handle(query, cancellationToken);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(2, result.Count);
		Assert.Contains(result, u => u.Name == "User 1");
		Assert.Contains(result, u => u.Name == "User 2");
		Assert.IsAssignableFrom<IQueryHandler<GetUsersQuery, List<User>>>(handler);
	}

	[Fact]
	public async Task QueryHandlers_ShouldWorkWithMockingFrameworks()
	{
		// Arrange
		var expectedUser = new User { Id = 1, Name = "Mocked User", Email = "mock@example.com" };

		var mockHandler = new Mock<IQueryHandler<GetUserQuery, User>>();

		mockHandler.Setup(h => h.Handle(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()))
				  .ReturnsAsync(expectedUser);

		var query = new GetUserQuery { UserId = 1 };

		// Act
		var result = await mockHandler.Object.Handle(query, CancellationToken.None);

		// Assert
		Assert.Equal(expectedUser, result);
		Assert.Equal("Mocked User", result.Name);
		mockHandler.Verify(h => h.Handle(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()), Times.Once);
	}

	[Fact]
	public async Task Handlers_ShouldWorkWithMockingFrameworks()
	{
		// Arrange
		var mockHandler = new Mock<ICommandHandler<CreateUserCommand, int>>();
		mockHandler.Setup(h => h.Handle(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
				  .ReturnsAsync(42);

		var command = new CreateUserCommand { Name = "Test", Email = "test@example.com" };

		// Act
		var result = await mockHandler.Object.Handle(command, CancellationToken.None);

		// Assert
		Assert.Equal(42, result);
		mockHandler.Verify(h => h.Handle(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
	}
}