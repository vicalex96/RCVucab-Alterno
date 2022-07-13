using MockQueryable.Moq;
using Moq;
using administracion.Persistence.Database;
using administracion.Persistence.Entities;

namespace administracion.Test.DataSeed
{
    public static class DataSeedIncidenteProcess
    {
        public static void SetupDbContextDataIncidenteProcess(this Mock<IAdminDBContext> _mockContext)
        {
            var asegurados = new List<Asegurado>
            {
                new Asegurado //Asegurado 0
                {
                    aseguradoId = new Guid("00000001-12aa-46bf-82a2-05ff65bb2c86"),
                    nombre = "Rogelio",
                    apellido = "Zambrano",
                    vehiculos = new List<Vehiculo>()
                },
                new Asegurado //Asegurado 1
                {
                    aseguradoId = new Guid("00000002-12aa-46bf-82a2-05ff65bb2c87"),
                    nombre = "Mario",
                    apellido = "Perez",
                    vehiculos = new List<Vehiculo>
                    {
                        new Vehiculo//vehiculo 0
                        {
                            vehiculoId = new Guid("00f401c9-12aa-46bf-82a3-05ff65bb2c00"),
                            anioModelo = 2007,
                            fechaCompra = DateTime.Parse("20/01/2007"),
                            color = Color.Blanco,
                            placa = "AB123CM",
                            marca = Marca.Volkswagen,
                            polizas = new List<Poliza>(),
                            aseguradoId = new Guid("00000002-12aa-46bf-82a2-05ff65bb2c87"),
                            asegurado = new Asegurado(),
                        }
                    }
                },
                new Asegurado //Asegurado 2
                {
                    aseguradoId = new Guid("00000003-12aa-46bf-82a2-05ff65bb2c88"),
                    nombre = "Juan",
                    apellido = "Willson",
                    vehiculos = new List<Vehiculo>
                    {
                        new Vehiculo //vehiculo 1
                        {
                            vehiculoId = new Guid("00f401c9-12aa-46bf-82a3-05bb34bb2c01"),
                            anioModelo = 2013,
                            fechaCompra = DateTime.Parse("15/05/2015"),
                            color = Color.Plateado,
                            placa = "AB234CM",
                            marca = Marca.Honda,
                            aseguradoId = new Guid("00000003-12aa-46bf-82a2-05ff65bb2c88"),
                            asegurado = new Asegurado(),
                            polizas = new List<Poliza>(),
                        },
                        new Vehiculo //vehiculo 2
                        {
                            vehiculoId = new Guid("00f401c9-12aa-46bf-82a3-05bb34bb2c02"),
                            anioModelo = 2019,
                            fechaCompra = DateTime.Parse("03/08/2020"),
                            color = Color.Dorado,
                            placa = "CD231CM",
                            marca = Marca.Ford,
                            aseguradoId = new Guid("00000003-12aa-46bf-82a2-05ff65bb2c88"),
                            asegurado = new Asegurado(),
                            polizas = new List<Poliza>(),
                        }
                    }
                }
            };

            var vehiculos = asegurados.SelectMany(q => q.vehiculos!).Concat(new List<Vehiculo>
            {
                new Vehiculo //vehiculo 3
                {
                    vehiculoId = new Guid("00f401c9-12aa-46bf-82a3-05bb34bb2c03"),
                    anioModelo = 2019,
                    fechaCompra = DateTime.Parse("15/05/2021"),
                    color = Color.Negro,
                    placa = "AB234CM",
                    marca = Marca.Audi,
                    aseguradoId = null,
                    asegurado = new Asegurado(),
                    polizas = new List<Poliza>(),
                },
            });
            vehiculos.ToList()[0].asegurado = asegurados[1];
            vehiculos.ToList()[1].asegurado = asegurados[2];
            vehiculos.ToList()[2].asegurado = asegurados[2];

            var polizas = new List<Poliza>
            {
                new Poliza //Poliza 0 (vencida, CoberturaCompleta)
                {
                    polizaId = Guid.Parse("000401c9-12aa-46bf-82a2-05ff65bb2000"),
                    fechaRegistro  = DateTime.ParseExact("20-10-2013","dd-MM-yyyy",null),
                    fechaVencimiento = DateTime.ParseExact("16-07-2017","dd-MM-yyyy",null),
                    tipoPoliza = TipoPoliza.CoberturaCompleta,
                    vehiculoId = vehiculos.ToList()[0].vehiculoId,
                    vehiculo = vehiculos.ToList()[0]
                },
                new Poliza //Poliza 1 (vigente, CoberturaCompleta)
                {
                    polizaId = Guid.Parse("000401c9-12aa-46bf-82a2-05ff65bb2001"),
                    fechaRegistro  = DateTime.ParseExact("17-07-2017","dd-MM-yyyy",null),
                    fechaVencimiento = DateTime.ParseExact("16-07-2024","dd-MM-yyyy",null),
                    tipoPoliza = TipoPoliza.CoberturaCompleta,
                    vehiculoId = vehiculos.ToList()[0].vehiculoId,
                    vehiculo = vehiculos.ToList()[0]
                },
                new Poliza //Poliza 2 (vigente, DaniosATerceros)
                {
                    polizaId = Guid.Parse("000401c9-12aa-46bf-82a2-05ff65bb2002"),
                    fechaRegistro  = DateTime.ParseExact("20-10-2020","dd-MM-yyyy",null),
                    fechaVencimiento = DateTime.ParseExact("16-07-2026","dd-MM-yyyy",null),
                    tipoPoliza = TipoPoliza.DaniosATerceros,
                    vehiculoId = vehiculos.ToList()[1].vehiculoId, 
                    vehiculo = vehiculos.ToList()[1], 
                },
            };
            vehiculos.ToList()[0].polizas!.Add(polizas[0]);
            vehiculos.ToList()[0].polizas!.Add(polizas[1]);
            vehiculos.ToList()[1].polizas!.Add(polizas[2]);
            
            var incidentes = new List<Incidente>
            {
                new Incidente
                {
                    incidenteId = Guid.Parse("000001c9-12aa-46bf-82a2-05ff65bb0000"),
                    polizaId = Guid.Parse("000401c9-12aa-46bf-82a2-05ff65bb2001"),
                    estadoIncidente = EstadoIncidente.Pendiente,
                    fechaRegistrado = DateTime.ParseExact("23-05-2021","dd-MM-yyyy",null),
                    poliza = polizas.ToList()[1],
                }
            };

            _mockContext.Setup(
                c => c.Asegurados
                ).Returns(
                    asegurados.AsQueryable().BuildMockDbSet().Object
                    );
            _mockContext.Setup(
                c => c.Vehiculos
                ).Returns(
                    vehiculos.AsQueryable().BuildMockDbSet().Object
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