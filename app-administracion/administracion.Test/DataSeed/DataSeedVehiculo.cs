using MockQueryable.Moq;
using Moq;
using administracion.Persistence.Database;
using administracion.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
namespace administracion.Test.DataSeed
{
    public static class DataSeedVehiculo
    {
        public static void SetupDbContextDataVehiculo(this Mock<IAdminDBContext> _mockContext)
        {

            var requests = new List<Vehiculo>
            {
                new Vehiculo
                {
                    vehiculoId = new Guid("38f401c9-12aa-46bf-82a3-05ff65bb2c86"),
                    anioModelo = 2007,
                    fechaCompra = DateTime.ParseExact("20-01-2007", "dd-MM-yyyy",null),
                    color = Color.Blanco,
                    placa = "AB123CM",
                    marca = Marca.Volkswagen,
                    polizas = new List<Poliza>(),
                    aseguradoId = new Guid("38f401c9-12aa-46bf-82a3-05ff65bb2c70"),
                    asegurado = null,

                },
                new Vehiculo
                {
                    vehiculoId = new Guid("38f401c9-12aa-46bf-82a3-05bb34bb2c70"),
                    anioModelo = 2013,
                    fechaCompra = DateTime.ParseExact("15-05-2015", "dd-MM-yyyy",null),
                    color = Color.Plateado,
                    placa = "AB234CM",
                    marca = Marca.Honda,
                    polizas = new List<Poliza>(),
                    aseguradoId = new Guid("38f401c9-12aa-46bf-82a3-05ff65bb2c50"),
                    asegurado = null,
                },
                new Vehiculo
                {
                    vehiculoId = new Guid("26f401c9-12aa-46bf-82a3-05bb34bb2c03"),
                    anioModelo = 2019,
                    fechaCompra = DateTime.ParseExact("03-08-2020", "dd-MM-yyyy",null),
                    color = Color.Dorado,
                    placa = "CD231CM",
                    marca = Marca.Ford,
                    polizas = new List<Poliza>(),
                    aseguradoId = new Guid("38f401c9-12aa-46bf-82a3-05ff65bb2c30"),
                    asegurado = new Asegurado
                    {
                        aseguradoId = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2c86"),
                        nombre = "Mario",
                        apellido = "Perez",
                        vehiculos = new List<Vehiculo>()
                    },
                },
            };

            var asegurados = new List<Asegurado>
            {
                new Asegurado
                {
                    aseguradoId = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2c86"),
                    nombre = "Rogelio",
                    apellido = "Zambrano",
                    vehiculos = new List<Vehiculo>()
                    {

                    }
                },
                new Asegurado
                {
                    aseguradoId = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2c86"),
                    nombre = "Mario",
                    apellido = "Perez",
                    vehiculos = new List<Vehiculo>()
                },
                new Asegurado
                {
                    aseguradoId = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2c86"),
                    nombre = "Juan",
                    apellido = "Willson",
                    vehiculos = new List<Vehiculo>()
                }
            };

            var polizas = new List<Poliza>
            {
                new Poliza
                {
                    polizaId = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2300"),
                    fechaRegistro  = DateTime.ParseExact("20-10-2013","dd-MM-yyyy",null),
                    fechaVencimiento = DateTime.ParseExact("16-07-2017","dd-MM-yyyy",null),
                    tipoPoliza = TipoPoliza.CoberturaCompleta,
                    vehiculoId = new Guid("26f401c9-12aa-46bf-82a3-05bb34bb2c03"),
                    vehiculo = new Vehiculo
                    {
                        vehiculoId = new Guid("26f401c9-12aa-46bf-82a3-05bb34bb2c03"),
                        anioModelo = 2007,
                        fechaCompra = DateTime.ParseExact("20-01-2007", "dd-MM-yyyy",null),
                        color = Color.Blanco,
                        placa = "AB123CM",
                        marca = Marca.Volkswagen,
                        polizas = new List<Poliza>(),
                        aseguradoId = new Guid("38f401c9-12aa-46bf-82a3-05ff65bb2c70"),
                        asegurado = new Asegurado
                        {
                            aseguradoId = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2c70"),
                            nombre = "Rogelio",
                            apellido = "Zambrano",
                            vehiculos = new List<Vehiculo>()
                            {

                            }
                        },

                    }, 
                },
                new Poliza
                {
                    polizaId = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2100"),
                    fechaRegistro  = DateTime.ParseExact("20-10-2000","dd-MM-yyyy",null),
                    fechaVencimiento = DateTime.ParseExact("16-07-2005","dd-MM-yyyy",null),
                    tipoPoliza = TipoPoliza.DaniosATerceros,
                    vehiculoId = new Guid("26f401c9-12aa-46bf-82a3-05bb34bb3aa5"),
                    vehiculo = new Vehiculo
                    {
                        vehiculoId = new Guid("26f401c9-12aa-46bf-82a3-05bb34bb3aa5"),
                        anioModelo = 2007,
                        fechaCompra = DateTime.ParseExact("20-01-2007", "dd-MM-yyyy",null),
                        color = Color.Blanco,
                        placa = "AB123CM",
                        marca = Marca.Volkswagen,
                        polizas = new List<Poliza>(),
                        aseguradoId = new Guid("38f401c9-12aa-46bf-82a3-05ff65bb2c70"),
                        asegurado = new Asegurado
                        {
                            aseguradoId = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2c70"),
                            nombre = "Rogelio",
                            apellido = "Zambrano",
                            vehiculos = new List<Vehiculo>()
                            {

                            }
                        },


                    }, 
                },
                new Poliza
                {
                    polizaId = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2200"),
                    fechaRegistro  = DateTime.ParseExact("20-10-2003","dd-MM-yyyy",null),
                    fechaVencimiento = DateTime.ParseExact("16-07-2007","dd-MM-yyyy",null),
                    tipoPoliza = TipoPoliza.DaniosATerceros,
                    vehiculoId = new Guid("AAAA01c9-12aa-46bf-82a3-05bb34bb2c03"), 
                    vehiculo = new Vehiculo
                    {
                        vehiculoId = new Guid("AAAA01c9-12aa-46bf-82a3-05bb34bb2c03"),
                        anioModelo = 2007,
                        fechaCompra = DateTime.ParseExact("20-01-2007", "dd-MM-yyyy",null),
                        color = Color.Blanco,
                        placa = "AB123CM",
                        marca = Marca.Volkswagen,
                        polizas = new List<Poliza>(),
                        aseguradoId = new Guid("38f401c9-12aa-46bf-82a3-05ff65bb2c70"),
                        asegurado = new Asegurado
                        {
                            aseguradoId = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2c70"),
                            nombre = "Rogelio",
                            apellido = "Zambrano",
                            vehiculos = new List<Vehiculo>()
                            {

                            }
                        },


                    },
                },
            };
            var incidentes = new List<Incidente>
                {
                    new Incidente
                    {
                        incidenteId = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2100"),
                        polizaId = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2200"),
                        estadoIncidente = EstadoIncidente.Pendiente,
                        fechaRegistrado = DateTime.ParseExact("23-05-2000","dd-MM-yyyy",null),
                        poliza = new Poliza
                        {
                            polizaId = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2200"),
                            fechaRegistro  = DateTime.ParseExact("20-10-2003","dd-MM-yyyy",null),
                            fechaVencimiento = DateTime.ParseExact("16-07-2007","dd-MM-yyyy",null),
                            vehiculoId = new Guid("26f401c9-12aa-46bf-82a3-05bb34bb2c03") 
                        },
                    },  
                };

            var requestsPoliza = requests
            .SelectMany(q => q.polizas)
            .Concat(new List<Poliza>
            {
            });

            

            _mockContext.Setup(
                c => c.Vehiculos
                ).Returns(
                    requests.AsQueryable().BuildMockDbSet().Object
                    );
            _mockContext.Setup(
                c => c.Polizas
                ).Returns(
                    requestsPoliza.AsQueryable().BuildMockDbSet().Object
                    );

            _mockContext.Setup(
                c => c.Asegurados
                ).Returns(
                    asegurados.AsQueryable().BuildMockDbSet().Object
                    );
            _mockContext.Setup(
                c => c.Polizas
                ).Returns(
                    polizas.AsQueryable().BuildMockDbSet().Object
                    );
            _mockContext.Setup(
                c => c.Incidentes
                ).Returns(
                    incidentes.AsQueryable().BuildMockDbSet().Object
                    );
        }
        
    }
}