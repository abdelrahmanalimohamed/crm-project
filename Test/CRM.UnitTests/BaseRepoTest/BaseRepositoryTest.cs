using Crm.Domain.Repository;
using Crm.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CRM.UnitTests.BaseRepoTest;
public class BaseRepositoryTest : IDisposable
{
	private readonly IBaseRepository<TestEntity> baseRepository;
	private readonly TestDbContext _dbContext;
	public BaseRepositoryTest()
	{
		var options = new DbContextOptionsBuilder<TestDbContext>()
			   .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
			   .Options;

		_dbContext = new TestDbContext(options);
		baseRepository = new BaseRepository<TestEntity>(_dbContext);
	}

	[Fact]
	public async Task GetAll_WhenEntitiesExist_ReturnsAllEntities()
	{
		// Arrange
		var entities = new List<TestEntity>
			{
				new TestEntity { Id = Guid.NewGuid(), Name = "Entity1", Description = "Description1", CreatedDate = DateTime.Now },
				new TestEntity { Id = Guid.NewGuid(), Name = "Entity2", Description = "Description2", CreatedDate = DateTime.Now },
				new TestEntity { Id = Guid.NewGuid(), Name = "Entity3", Description = "Description3", CreatedDate = DateTime.Now }
			};

		await _dbContext.TestEntities.AddRangeAsync(entities);
		await _dbContext.SaveChangesAsync();

		//Act
		var returnedValues = await baseRepository.GetAll();

		//Assert
		Assert.NotNull(returnedValues);
		Assert.Equal(3, returnedValues.Count());
		Assert.Contains(returnedValues, e => e.Name == "Entity1");
		Assert.Contains(returnedValues, e => e.Name == "Entity2");
		Assert.Contains(returnedValues, e => e.Name == "Entity3");
	}

	[Fact]
	public async Task GetAll_WhenNoEntitiesExist_ReturnsEmptyCollection()
	{
		// Act
		var result = await baseRepository.GetAll();

		// Assert
		Assert.NotNull(result);
		Assert.Empty(result);
	}

	[Fact]
	public async Task GetById_WhenEntityExists_ReturnsEntity()
	{
		// Arrange
		var entityId = Guid.NewGuid();
		var entity = new TestEntity
		{
			Id = entityId,
			Name = "TestEntity",
			Description = "Test Description",
			CreatedDate = DateTime.Now
		};

		await _dbContext.TestEntities.AddAsync(entity);
		await _dbContext.SaveChangesAsync();

		// Act
		var result = await baseRepository.GetById(entityId);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(entityId, result.Id);
		Assert.Equal("TestEntity", result.Name);
		Assert.Equal("Test Description", result.Description);
	}

	[Fact]
	public async Task GetById_WhenEntityDoesNotExist_ThrowsException()
	{
		// Arrange
		var nonExistentId = Guid.NewGuid();

		// Act & Assert
		var exception = await Assert.ThrowsAsync<Exception>(
			async () => await baseRepository.GetById(nonExistentId));

		Assert.Contains($"Entity of type TestEntity with id {nonExistentId} not found", exception.Message);
	}

	[Fact]
	public async Task Insert_WhenValidEntity_AddsEntityToContext()
	{
		// Arrange
		var entity = new TestEntity
		{
			Id = Guid.NewGuid(),
			Name = "NewEntity",
			Description = "New Description",
			CreatedDate = DateTime.Now
		};

		// Act
		await baseRepository.Insert(entity);
		await _dbContext.SaveChangesAsync();

		// Assert
		var insertedEntity = await _dbContext.TestEntities.FindAsync(entity.Id);

		Assert.NotNull(insertedEntity);
		Assert.Equal(entity.Id, insertedEntity.Id);
		Assert.Equal(entity.Name, insertedEntity.Name);
		Assert.Equal(entity.Description, insertedEntity.Description);
	}

	[Fact]
	public async Task Update_WhenValidEntity_UpdatesEntity()
	{
		// Arrange
		var entity = new TestEntity
		{
			Id = Guid.NewGuid(),
			Name = "NewEntity",
			Description = "New Description",
			CreatedDate = DateTime.Now
		};

		// Act
		await _dbContext.TestEntities.AddAsync(entity);
		await _dbContext.SaveChangesAsync();

		entity.Name = "UpdatedName";
		entity.Description = "Updated Description";
		await baseRepository.Update(entity);
		await _dbContext.SaveChangesAsync();

		// Assert
		var updatedEntity = await _dbContext.TestEntities.FindAsync(entity.Id);

		Assert.NotNull(updatedEntity);
		Assert.Equal("UpdatedName", updatedEntity.Name);
		Assert.Equal("Updated Description", updatedEntity.Description);
	}

	[Fact]
	public async Task Delete_WhenValidEntity_RemovesEntity()
	{
		// Arrange
		var entity = new TestEntity
		{
			Id = Guid.NewGuid(),
			Name = "EntityToDelete",
			Description = "This will be deleted",
			CreatedDate = DateTime.Now
		};

		await _dbContext.TestEntities.AddAsync(entity);
		await _dbContext.SaveChangesAsync();

		// Act
		await baseRepository.Delete(entity);
		await _dbContext.SaveChangesAsync();

		// Assert
		var deletedEntity = await _dbContext.TestEntities.FindAsync(entity.Id);
		Assert.Null(deletedEntity);
	}

	[Fact]
	public async Task FindWithIncludeAsync_WithPredicate_ReturnsMatchingEntities()
	{
		// Arrange
		var entities = new List<TestEntity>
			{
				new TestEntity { Id = Guid.NewGuid(), Name = "Match1", Description = "Description1", CreatedDate = DateTime.Now },
				new TestEntity { Id = Guid.NewGuid(), Name = "Match2", Description = "Description2", CreatedDate = DateTime.Now },
				new TestEntity { Id = Guid.NewGuid(), Name = "NoMatch", Description = "Description3", CreatedDate = DateTime.Now }
			};

		await _dbContext.TestEntities.AddRangeAsync(entities);
		await _dbContext.SaveChangesAsync();

		Expression<Func<TestEntity, bool>> predicate = e => e.Name.StartsWith("Match");

		// Act
		var result = await baseRepository.FindWithIncludeAsync(predicate);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(2, result.Count());
		Assert.All(result, entity => Assert.StartsWith("Match", entity.Name));
	}

	[Fact]
	public async Task FindWithIncludeAsync_WithoutPredicate_ReturnsAllEntities()
	{
		// Arrange
		var entities = new List<TestEntity>
			{
				new TestEntity { Id = Guid.NewGuid(), Name = "Entity1", Description = "Description1", CreatedDate = DateTime.Now },
				new TestEntity { Id = Guid.NewGuid(), Name = "Entity2", Description = "Description2", CreatedDate = DateTime.Now }
			};

		await _dbContext.TestEntities.AddRangeAsync(entities);
		await _dbContext.SaveChangesAsync();

		// Act
		var result = await baseRepository.FindWithIncludeAsync();

		// Assert
		Assert.NotNull(result);
		Assert.Equal(2, result.Count());
	}

	[Fact]
	public async Task FindWithIncludeAsync_WithEmptyResult_ReturnsEmptyCollection()
	{
		// Arrange
		Expression<Func<TestEntity, bool>> predicate = e => e.Name == "NonExistent";

		// Act
		var result = await baseRepository.FindWithIncludeAsync(predicate);

		// Assert
		Assert.NotNull(result);
		Assert.Empty(result);
	}
	public void Dispose()
	{
		_dbContext.Dispose();
	}
}
