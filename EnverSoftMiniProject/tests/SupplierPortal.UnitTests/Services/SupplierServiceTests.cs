using Moq;
using SupplierPortal.Application.Interfaces;
using SupplierPortal.Application.Models;
using SupplierPortal.Application.Services;
using SupplierPortal.Domain.Entities;
using SupplierPortal.Domain.Interfaces;
using Xunit;

namespace SupplierPortal.UnitTests.Services;

public class SupplierServiceTests
{
    private readonly Mock<ISupplierRepository> _mockRepository;
    private readonly SupplierService _service;

    public SupplierServiceTests()
    {
        _mockRepository = new Mock<ISupplierRepository>();
        _service = new SupplierService(_mockRepository.Object);
    }

    [Fact]
    public async Task CreateAsync_ValidRequest_ReturnsResponse()
    {
        // Arrange
        var request = new CreateSupplierRequest { CompanyName = "Test Co", TelephoneNo = "123456" };
        var supplier = new Supplier { Name = "Test Co", TelephoneNo = "123456", Code = 1 };
        
        _mockRepository.Setup(r => r.ExistsByCompanyNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        _mockRepository.Setup(r => r.AddAsync(It.IsAny<Supplier>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(supplier);

        // Act
        var result = await _service.CreateAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Co", result.CompanyName);
        Assert.Equal("123456", result.TelephoneNo);
        Assert.Equal(1, result.Code);
    }

    [Theory]
    [InlineData("", "123456")]
    [InlineData("Test Co", "")]
    [InlineData(" ", "123456")]
    [InlineData("Test Co", " ")]
    public async Task CreateAsync_InvalidData_ThrowsArgumentException(string name, string phone)
    {
        // Arrange
        var request = new CreateSupplierRequest { CompanyName = name, TelephoneNo = phone };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAsync(request));
    }

    [Fact]
    public async Task CreateAsync_DuplicateName_ThrowsInvalidOperationException()
    {
        // Arrange
        var request = new CreateSupplierRequest { CompanyName = "Exists", TelephoneNo = "123" };
        _mockRepository.Setup(r => r.ExistsByCompanyNameAsync("Exists", It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _service.CreateAsync(request));
    }

    [Fact]
    public async Task SearchByCompanyNameAsync_Found_ReturnsPagedList()
    {
        // Arrange
        var suppliers = new List<Supplier> { 
            new Supplier { Name = "Found 1", TelephoneNo = "123", Code = 10 },
            new Supplier { Name = "Found 2", TelephoneNo = "456", Code = 11 }
        };
        _mockRepository.Setup(r => r.SearchByCompanyNameAsync("Found", 1, 5, It.IsAny<CancellationToken>()))
            .ReturnsAsync((suppliers, 2));

        // Act
        var result = await _service.SearchByCompanyNameAsync("Found", 1, 5);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Items.Count);
        Assert.Equal(2, result.TotalCount);
        Assert.Equal("Found 1", result.Items[0].CompanyName);
    }

    [Fact]
    public async Task SearchByCompanyNameAsync_NotFound_ReturnsEmptyPagedList()
    {
        // Arrange
        _mockRepository.Setup(r => r.SearchByCompanyNameAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((new List<Supplier>(), 0));

        // Act
        var result = await _service.SearchByCompanyNameAsync("Missing", 1, 5);

        // Assert
        Assert.Empty(result.Items);
        Assert.Equal(0, result.TotalCount);
    }

    [Fact]
    public async Task SearchByCompanyNameAsync_EmptyInput_ReturnsEmptyPagedList()
    {
        // Act
        var result = await _service.SearchByCompanyNameAsync("", 1, 5);

        // Assert
        Assert.Empty(result.Items);
        Assert.Equal(0, result.TotalCount);
    }
}
