using Microsoft.EntityFrameworkCore;
using proveedor.BussinesLogic.DTOs;
using proveedor.Exceptions;
using proveedor.Persistence.DAOs.Interfaces;
using proveedor.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace proveedor.Persistence.DAOs.Implementations
{
    public class ProviderDAO : IProviderDAO
    {
        public readonly IProveedorDbContext _context;

        public ProviderDAO(IProveedorDbContext context)
        {
            _context = context;
        }

      /*  public List<MarcaDTO> GetProvidersByBrand(string brand)
        {
            try
            {
                var data = _context.Marcas
                   .Include(b => b.Providers)
                   .Where(b => b.Name == brand)
                   .Select(b => new MarcaDTO
                   {
                       Id = b.Id,
                       Nombre = b.Name,
                       Providers = b.Providers.Select(p => new ProviderDTO
                       {
                           Id = p.Id,
                           FullName = p.Nombre 
                       }).ToList()
                   });

                return data.ToList();
            }
            catch(Exception ex)
            {
                throw new ProveedorException("Ha ocurrido un error al intentar consultar la lista de proveedores para la marca: "
              + brand, ex.Message, ex);
            }
        }*/
    }
}
