using Bogus;
using Microsoft.Extensions.Logging;
using Moq;
using proveedor.Persistence.DAOs.Implementations;
using proveedor.Persistence.Database;
using proveedor.Test.DataSeed;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace proveedor.Test.UnitTests.DAOs
{
    public class ProviderDAOTest
    {
        private readonly ProviderDAO _dao;
        private readonly Mock<IProveedorDbContext> _contextMock;
        private readonly Mock<ILogger<ProviderDAO>> _mockLogger;
       
       /* public ProviderDAOTest()
        {
            var faker = new Faker();
            _contextMock = new Mock<IProveedorDbContext>();
            _mockLogger = new Mock<ILogger<ProviderDAO>>();
            
            _dao = new ProviderDAO(_contextMock.Object);
            _contextMock.SetupDbContextData();
        }*/

       /* [Theory]
        [InlineData("Toyota")]
        public Task ShouldReturnAllClaimsProviderData(string brand)
        {
            var result = _dao.GetProvidersByBrand(brand);
            var data = result;
            Assert.True(data.Any());
            return Task.CompletedTask;
        }*/
    }
}
