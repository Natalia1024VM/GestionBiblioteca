using CapaDAL.Entidad;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CapaDAL
{
    public class LibroRepositorio
    {
        public int AgregarLibro(Libro libro, ApplicationDbContext conection)
        {
            var id = 0;
            var parametroID = new SqlParameter("@IdLibro", SqlDbType.Int);
            parametroID.Direction = ParameterDirection.Output;

            conection.Database
                .ExecuteSqlInterpolatedAsync($@"Exec SP_InsertarLibro
                                             @Nombre={libro.Nombre}, @FechaPublicacion={libro.FechaPublicacion}, @IdLibro={parametroID} OUTPUT");

            System.Threading.Thread.Sleep(100);

            if (parametroID.SqlValue != null)
            {
                string valor=parametroID.SqlValue.ToString();
                id = Convert.ToInt32(valor);
            }
            return id;
        }

        public async Task<Libro> ConsultarLibroIDAsync(int id, ApplicationDbContext conection)
        {
            var parametroID = new SqlParameter("@IdLibro", SqlDbType.Int);
            parametroID.Direction = ParameterDirection.Output;

            var librosRespuesta = conection.Libros
                .FromSqlInterpolated($"Exec SP_ConsultarLibroPorID @idLibro={id}")
                .AsAsyncEnumerable();

            await foreach (var item in librosRespuesta)
            {
                return item;
            }
            return null;
        }

        public async Task<List<Libro>> ConsultarLibros(ApplicationDbContext conection)
        {
            var librosRespuesta = conection.Libros
                .FromSqlInterpolated($"Exec SP_ConsultarLibro ")
                .AsAsyncEnumerable();

            List<Libro> Lista= new List<Libro>();

            await foreach (var item in librosRespuesta)
            {
                Lista.Add(item);
            }
            return Lista;
        }

        public int ModificarLibro(int idLibro, Libro libroEntidad, ApplicationDbContext conection)
        {
            var id = 0;
            var parametroID = new SqlParameter("@resultado", SqlDbType.Int);
            parametroID.Direction = ParameterDirection.Output;

            conection.Database
                .ExecuteSqlInterpolatedAsync($@"Exec SP_ModificarLibro
                                            @IdLibro={idLibro} , @Nombre={libroEntidad.Nombre}, @FechaPublicacion={libroEntidad.FechaPublicacion}, @resultado = {parametroID} OUTPUT");
            System.Threading.Thread.Sleep(100);

            if (parametroID.SqlValue != null)
            {
                string valor = parametroID.SqlValue.ToString();
                id = Convert.ToInt32(valor);
            }
            return id;
        }

        public int EliminarLibro(int idLibro,ApplicationDbContext conection)
        {
            var id = 0;
            var parametroID = new SqlParameter("@resultado", SqlDbType.Int);
            parametroID.Direction = ParameterDirection.Output;

            conection.Database
                .ExecuteSqlInterpolatedAsync($@"Exec SP_EliminarLibro   
                                            @IdLibro={idLibro}, @resultado = {parametroID} OUTPUT");
            System.Threading.Thread.Sleep(100);

            if (parametroID.SqlValue != null)
            {
                string valor = parametroID.SqlValue.ToString();
                id = Convert.ToInt32(valor);
            }
            return id;
        }
    }
}
