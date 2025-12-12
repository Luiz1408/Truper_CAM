using Microsoft.EntityFrameworkCore;
using ExcelProcessorApi.Models;

namespace ExcelProcessorApi.Data
{
    public static class SeedAlmacenUbicacionFolio
    {
        public static async Task SeedData(ApplicationDbContext context)
        {
            // Verificar si ya existen datos
            if (await context.AlmacenUbicacionFolios.AnyAsync())
            {
                return; // Ya hay datos, no hacer nada
            }

            var almacenes = new List<AlmacenUbicacionFolio>
            {
                new AlmacenUbicacionFolio
                {
                    Almacen = "Tuxtla",
                    FolioAsignado1 = "STUX",
                    Ubicacion = "Sucursal",
                    Activo = true,
                    CreadoPor = "System"
                },
                new AlmacenUbicacionFolio
                {
                    Almacen = "Tapachula",
                    FolioAsignado1 = "STAP",
                    Ubicacion = "Sucursal Tapachula",
                    Activo = true,
                    CreadoPor = "System"
                },
                new AlmacenUbicacionFolio
                {
                    Almacen = "San Cristóbal",
                    FolioAsignado1 = "STSC",
                    Ubicacion = "Sucursal San Cristóbal",
                    Activo = true,
                    CreadoPor = "System"
                },
                new AlmacenUbicacionFolio
                {
                    Almacen = "Comitán",
                    FolioAsignado1 = "STCM",
                    Ubicacion = "Sucursal Comitán",
                    Activo = true,
                    CreadoPor = "System"
                }
            };

            await context.AlmacenUbicacionFolios.AddRangeAsync(almacenes);
            await context.SaveChangesAsync();
        }
    }
}
