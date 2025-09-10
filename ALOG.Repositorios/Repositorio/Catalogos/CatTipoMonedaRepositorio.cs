using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Catalogos.ICatalogo;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;

namespace ALOG.Repositorios.Repositorio.Catalogos
{
    public class CatTipoMonedaRepositorio : ICatTipoMonedaRepositorio
    {

        private readonly ApplicationDbContext _db;
        public CatTipoMonedaRepositorio(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<RespuestaGenericaDTO> ListarMonedas()
        {
            var respuesta = new RespuestaGenericaDTO();
            respuesta.Entidades = new List<object>();
            var errores = new List<string>();

            try
            {
                using (var connection = _db.Database.GetDbConnection() as SqlConnection)
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand("Catalogos.ObtenerTipoMonedas", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Ejecutar el procedimiento almacenado
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var mensajeError = reader[0]?.ToString();
                                respuesta.Entidades.Add(new CatTipoMoneda
                                {
                                    IdCatTipoMoneda = int.Parse(reader[0].ToString()),
                                    Descripcion = reader[1].ToString(),
                                    ClaveSAT = reader[2].ToString(),
                                    IdUsuarioRegistro = int.Parse(reader[3].ToString()),
                                    Activo = bool.Parse(reader[4].ToString()),
                                    FechaRegistro = DateTime.Parse(reader[5].ToString()),
                                });
                                if (!string.IsNullOrEmpty(mensajeError))
                                {
                                    errores.Add(mensajeError);
                                }
                            }
                        }
                        //Console.WriteLine(JsonSerializer.Serialize(respuesta, new JsonSerializerOptions { WriteIndented = true }));
                        // Construcción de la respuesta
                        //respuesta.StatusCode = errores.Any() ? HttpStatusCode.BadRequest : HttpStatusCode.OK;
                        //respuesta.lstrErrorMessages = errores;
                        //respuesta.IsSuccess = !errores.Any();
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.StatusCode = HttpStatusCode.InternalServerError;
                respuesta.lstrErrorMessages = new List<string> { ex.Message };
                respuesta.IsSuccess = false;
                //Console.WriteLine($"Error: {ex.Message}");
            }

            return respuesta;
        }
    }
}
