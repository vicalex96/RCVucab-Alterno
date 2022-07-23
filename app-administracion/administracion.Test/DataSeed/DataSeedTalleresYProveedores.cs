using MockQueryable.Moq;
using Moq;
using administracion.Persistence.Database;
using administracion.Persistence.Entities;
using administracion.Persistence.Enums;

namespace administracion.Test.DataSeed
{
    public static class DataSeedEmpresas
    {
        public static void SetupDbContextDataEmpresas(this Mock<IAdminDBContext> _mockContext)
        {
            var requestsTalleres = new List<Taller>
            {
                new Taller
                {
                    Id = new Guid("100001c9-12aa-46bf-82a3-05ff65bb2c86"),
                    nombreLocal = "Taller 1",
                    marcas = new List<MarcaTaller>{

                        new MarcaTaller
                        {
                            Id = new Guid("100001c9-1212-46bf-82a3-05ff65bb2c85"),
                            tallerId = new Guid("100001c9-12aa-46bf-82a3-05ff65bb2c86"),
                            manejaTodas = true,
                        },
                    }
                },
                new Taller
                {
                    Id = new Guid("200001c9-12aa-46bf-82a3-05ff65bb2c87"),
                    nombreLocal = "Taller 2",
                    marcas = new List<MarcaTaller>{

                        new MarcaTaller
                        {
                            Id = new Guid("200001c9-1212-46bf-82a3-05ff65bb2c88"),
                            tallerId = new Guid("200001c9-12aa-46bf-82a3-05ff65bb2c87"),
                            marca = MarcaName.Suzuki
                        },
                        new MarcaTaller
                        {
                            Id = new Guid("200001c9-1212-46bf-82a3-05ff65bb2c89"),
                            tallerId = new Guid("200001c9-12aa-46bf-82a3-05ff65bb2c87"),
                            marca = MarcaName.Volkswagen
                        },
                        new MarcaTaller
                        {
                            Id = new Guid("200001c9-1212-46bf-82a3-05ff65bb2c90"),
                            tallerId = new Guid("200001c9-12aa-46bf-82a3-05ff65bb2c87"),
                            marca = MarcaName.General_Motors
                        },
                    }
                },
            };

            var requestsProveedores = new List<Proveedor>
            {
                new Proveedor
                {
                    Id = new Guid("100001c9-1212-46bf-82a3-05ff65bb2c86"),
                    nombreLocal = "Proveedor 1",
                    marcas = new List<MarcaProveedor>{

                        new MarcaProveedor
                        {
                            Id = new Guid("100001c9-1313-46bf-82a3-05ff65bb2c87"),
                            proveedorId = new Guid("100001c9-1212-46bf-82a3-05ff65bb2c86"),
                            manejaTodas = true,
                        },
                    }
                },
                new Proveedor
                {
                    Id = new Guid("200001c9-1212-46bf-82a3-05ff65bb2c87"),
                    nombreLocal = "Proveedor 2",
                    marcas = new List<MarcaProveedor>{

                        new MarcaProveedor
                        {
                            Id = new Guid("100001c9-1313-46bf-82a3-05ff65bb2c88"),
                            proveedorId = new Guid("200001c9-1212-46bf-82a3-05ff65bb2c87"),
                            marca = MarcaName.Volkswagen
                        },
                        new MarcaProveedor
                        {
                            Id = new Guid("100001c9-1515-46bf-82a3-05ff65bb2c89"),
                            proveedorId = new Guid("200001c9-1212-46bf-82a3-05ff65bb2c87"),
                            marca = MarcaName.Suzuki
                        },
                        new MarcaProveedor
                        {
                            Id = new Guid("100001c9-1717-46bf-82a3-05ff65bb2c90"),
                            proveedorId = new Guid("200001c9-1212-46bf-82a3-05ff65bb2c87"),
                            marca = MarcaName.General_Motors
                        },
                    }
                },
            };

            var requestsMarcasTaller = new List<MarcaTaller>
            {

                new MarcaTaller
                {
                    Id = new Guid("100001c9-1212-46bf-82a3-05ff65bb2c85"),
                    tallerId = new Guid("100001c9-12aa-46bf-82a3-05ff65bb2c86"),
                    marca = null,
                    manejaTodas = true,
                },
                new MarcaTaller
                {
                    Id = new Guid("200001c9-1212-46bf-82a3-05ff65bb2c88"),
                    tallerId = new Guid("200001c9-12aa-46bf-82a3-05ff65bb2c87"),
                    marca = MarcaName.Suzuki,
                    manejaTodas = false,
                },
                new MarcaTaller
                {
                    Id = new Guid("200001c9-1212-46bf-82a3-05ff65bb2c89"),
                    tallerId = new Guid("200001c9-12aa-46bf-82a3-05ff65bb2c87"),
                    marca = MarcaName.Volkswagen,
                    manejaTodas = false,
                },
                new MarcaTaller
                {
                    Id = new Guid("200001c9-1212-46bf-82a3-05ff65bb2c90"),
                    tallerId = new Guid("200001c9-12aa-46bf-82a3-05ff65bb2c87"),
                    marca = MarcaName.General_Motors,
                    manejaTodas = false,
                },
            };
    
            var requestsMarcasProveedor = new List<MarcaProveedor>
            {
                new MarcaProveedor
                {
                    Id = new Guid("100001c9-1313-46bf-82a3-05ff65bb2c87"),
                    proveedorId = new Guid("100001c9-1212-46bf-82a3-05ff65bb2c86"),
                    marca = null,
                    manejaTodas = true,
                },
                new MarcaProveedor
                {
                    Id = new Guid("100001c9-1313-46bf-82a3-05ff65bb2c88"),
                    proveedorId = new Guid("200001c9-1212-46bf-82a3-05ff65bb2c87"),
                    marca = MarcaName.Volkswagen,
                    manejaTodas = false,
                },
                new MarcaProveedor
                {
                    Id = new Guid("100001c9-1515-46bf-82a3-05ff65bb2c89"),
                    proveedorId = new Guid("200001c9-1212-46bf-82a3-05ff65bb2c87"),
                    marca = MarcaName.Suzuki,
                    manejaTodas = false,
                },
                new MarcaProveedor
                {
                    Id = new Guid("100001c9-1717-46bf-82a3-05ff65bb2c90"),
                    proveedorId = new Guid("200001c9-1212-46bf-82a3-05ff65bb2c87"),
                    marca = MarcaName.General_Motors,
                    manejaTodas = false,
                },
            };

            _mockContext.Setup(
                c => c.Talleres
                ).Returns(
                    requestsTalleres.AsQueryable().BuildMockDbSet().Object
                    );
            _mockContext.Setup(
                c => c.Proveedores
                ).Returns(
                    requestsProveedores.AsQueryable().BuildMockDbSet().Object
                    );
            _mockContext.Setup(
                c => c.MarcasTaller
                ).Returns(
                    requestsMarcasTaller.AsQueryable().BuildMockDbSet().Object
            );
            _mockContext.Setup(
                c => c.MarcasProveedor
                ).Returns(
                    requestsMarcasProveedor.AsQueryable().BuildMockDbSet().Object
            );
        }

    }
}