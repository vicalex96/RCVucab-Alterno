using MockQueryable.Moq;
using Moq;
using proveedor.Persistence.Database;
using proveedor.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace proveedor.Test.DataSeed
{
    public static class DataSeed{
    public static void SetupDbContextData(this Mock<IProveedorDbContext> _mockContext)
        {
          var requests = new List<CotizacionParteEntity>
                {
                    new CotizacionParteEntity
                    {
                        CotizacionParteId = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2c87"),
                        ProveedorId = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2c87"),
                        PrecioParteUnidad = 123,
                        FechaEntrega = new DateTime(2022,06,1),
                        estado = 0,
                        RequerimientoId = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2c88"),
                        requerimientos = new List<Requerimiento>()
                        {

                        }
                    },
                    new CotizacionParteEntity
                    {
                        CotizacionParteId = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2c86"),
                        ProveedorId = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2c87"),
                        PrecioParteUnidad = 123,
                        FechaEntrega = new DateTime(2022,06,1),
                        estado = 0,
                        RequerimientoId = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2c88"),
                        requerimientos = new List<Requerimiento>()
                        {

                        }
                    },
                    new CotizacionParteEntity
                    {
                        CotizacionParteId = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2c89"),
                        ProveedorId = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2c87"),
                        PrecioParteUnidad = 123,
                        FechaEntrega = new DateTime(2022,06,1),
                        estado = 0,
                        RequerimientoId = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2c88"),
                        requerimientos = new List<Requerimiento>()
                        {

                        }
                    },
                };
            
            /*
                        var requestsPoliza = requestsvehiculo.SelectMany(q => q.polizas).Concat(new List<Vehiculo>
                        {
                        });
            */
            _mockContext.Setup(
                c => c.CotizacionPartes
                ).Returns(
                    requests.AsQueryable().BuildMockDbSet().Object
                    );
           /* _mockContext.Setup(
                c => c.Vehiculos
                ).Returns(
                    requestsvehiculo.AsQueryable().BuildMockDbSet().Object
                    );*/
            /* _mockContext.Setup(
                c => c.Polizas
                ).Returns(
                    requestsPoliza.AsQueryable().BuildMockDbSet().Object
                    );
             */
        
    }
}
}
