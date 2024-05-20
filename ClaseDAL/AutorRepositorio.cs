using CapaDAL.Entidad;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CapaDAL
{
    public class AutorRepositorio
    {
        public int AgregarAutor(Autor Autor, ApplicationDbContext conection)
        {
            var id = 0;
            var parametroID = new SqlParameter("@IdAutor", SqlDbType.Int);
            parametroID.Direction = ParameterDirection.Output;

            conection.Database
                .ExecuteSqlInterpolatedAsync($@"Exec SP_InsertarAutor
                                             @Nombre={Autor.Nombre}, @Apellido={Autor.Apellido}, @FechaNacimiento={Autor.FechaNacimiento}, @IdAutor={parametroID} OUTPUT");

            System.Threading.Thread.Sleep(100);

            if (parametroID.SqlValue != null)
            {
                string valor = parametroID.SqlValue.ToString();
                id = Convert.ToInt32(valor);
            }
            return id;
        }

        public async Task<Autor> ConsultarAutorIDAsync(int id, ApplicationDbContext conection)
        {
            var parametroID = new SqlParameter("@IdAutor", SqlDbType.Int);
            parametroID.Direction = ParameterDirection.Output;

            var AutorsRespuesta = conection.Autores
                .FromSqlInterpolated($"Exec SP_ConsultarAutorPorID @idAutor={id}")
                .AsAsyncEnumerable();

            await foreach (var item in AutorsRespuesta)
            {
                return item;
            }
            return null;
        }
        public async Task<Autor> ConsultarAutorAsync(string nombre, ApplicationDbContext conection)
        {
            var AutorsRespuesta = conection.Autores
                .FromSqlInterpolated($"Exec SP_ConsultarAutorPorNombre @nombre={nombre}")
                .AsAsyncEnumerable();

            await foreach (var item in AutorsRespuesta)
            {
                return item;
            }
            return null;
        }


        public async Task<List<Autor>> ConsultarAutors(ApplicationDbContext conection)
        {
            var AutorsRespuesta = conection.Autores
                .FromSqlInterpolated($"Exec SP_ConsultarAutor ")
                .AsAsyncEnumerable();

            List<Autor> Lista = new List<Autor>();

            await foreach (var item in AutorsRespuesta)
            {
                Lista.Add(item);
            }
            return Lista;
        }

        public int ModificarAutor(int idAutor, Autor AutorEntidad, ApplicationDbContext conection)
        {
            var id = 0;
            var parametroID = new SqlParameter("@resultado", SqlDbType.Int);
            parametroID.Direction = ParameterDirection.Output;

            conection.Database
                .ExecuteSqlInterpolatedAsync($@"Exec SP_ModificarAutor
                                            @IdAutor={idAutor} , @Nombre={AutorEntidad.Nombre}, @Apellido={AutorEntidad.Apellido}, @FechaNacimiento={AutorEntidad.FechaNacimiento}, @resultado = {parametroID} OUTPUT");
            System.Threading.Thread.Sleep(100);

            if (parametroID.SqlValue != null)
            {
                string valor = parametroID.SqlValue.ToString();
                id = Convert.ToInt32(valor);
            }
            return id;
        }

        public int EliminarAutor(int idAutor, ApplicationDbContext conection)
        {
            var id = 0;
            var parametroID = new SqlParameter("@resultado", SqlDbType.Int);
            parametroID.Direction = ParameterDirection.Output;

            conection.Database
                .ExecuteSqlInterpolatedAsync($@"Exec SP_EliminarAutor   
                                            @IdAutor={idAutor}, @resultado = {parametroID} OUTPUT");
            System.Threading.Thread.Sleep(100);

            if (parametroID.SqlValue != null)
            {
                string valor = parametroID.SqlValue.ToString();
                id = Convert.ToInt32(valor);
            }
            return id;
        }

        public int AdicionarLibroAutor(int idAutor, int idLibro, ApplicationDbContext conection)
        {
            var id = 0;
            var parametroID = new SqlParameter("@resultado", SqlDbType.Int);
            parametroID.Direction = ParameterDirection.Output;

            conection.Database
                .ExecuteSqlInterpolatedAsync($@"Exec SP_AdicionarLibroAutor
                                            @IdAutor={idAutor} , @IdLibro={idLibro}, @resultado = {parametroID} OUTPUT");
            System.Threading.Thread.Sleep(100);

            if (parametroID.SqlValue != null)
            {
                string valor = parametroID.SqlValue.ToString();
                id = Convert.ToInt32(valor);
            }
            return id;
        }

        public int EliminarLibroAutor(int idAutor, int idLibro, ApplicationDbContext conection)
        {
            var id = 0;
            var parametroID = new SqlParameter("@resultado", SqlDbType.Int);
            parametroID.Direction = ParameterDirection.Output;

            conection.Database
                .ExecuteSqlInterpolatedAsync($@"Exec SP_EliminarLibroAutor
                                            @IdAutor={idAutor} , @IdLibro={idLibro}, @resultado = {parametroID} OUTPUT");
            System.Threading.Thread.Sleep(100);

            if (parametroID.SqlValue != null)
            {
                string valor = parametroID.SqlValue.ToString();
                id = Convert.ToInt32(valor);
            }
            return id;
        }


        public async Task<List<AutorLibro>> ConsultarLibrosAutor(int idAutor, ApplicationDbContext conection)
        {
            
            var AutorsRespuesta = conection.AutorLibro
                .FromSqlInterpolated($"Exec SP_ConsultarLibrosAutor @idAutor={idAutor}")
                .AsAsyncEnumerable();


            List<AutorLibro> Lista = new List<AutorLibro>();

            await foreach (var item in AutorsRespuesta)
            {
                Lista.Add(item);
            }
            return Lista;

        }
        public async Task<AutorLibro> ConsultarLibroAsignado(int idLibro, ApplicationDbContext conection)
        {
            var AutorsRespuesta = conection.AutorLibro
                .FromSqlInterpolated($"Exec SP_ConsultarLibroAsignado @idLibro={idLibro}")
                .AsAsyncEnumerable();

            await foreach (var item in AutorsRespuesta)
            {
                return item;
            }
            return null;
        }


    }
}
