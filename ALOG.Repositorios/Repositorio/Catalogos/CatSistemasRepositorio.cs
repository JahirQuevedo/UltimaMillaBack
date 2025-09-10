using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.DTO.Control;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Catalogos.ICatalogo;
using ALOG.Repositorios.Utilerias;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;


namespace ALOG.Repositorios.Repositorio.Catalogos
{
    public class CatSistemasRepositorio : ICatSistemasRepositorio
    {
        private readonly ApplicationDbContext _db;
        UtileriasCifrados clsCifrados = new UtileriasCifrados();
        private string claveSecreta;
        public CatSistemasRepositorio(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            claveSecreta = configuration.GetValue<string>("ApiSettings:Secreta");

        }
        public bool actualizarSistema(int IdCatSistemas, SistemaRegistroDTO catSistemas, out string strError)
        {
            throw new NotImplementedException();
        }

        public bool bajaSistema(int IdCatSistema, out string strError)
        {
            throw new NotImplementedException();
        }

        public bool crearSistema(SistemaRegistroDTO catSistemas, out string strError)
        {
            strError = null;
            CatSistemas objCatSistemas = new CatSistemas();
            objCatSistemas.nombre = catSistemas.nombre;
            objCatSistemas.userSistema = catSistemas.Usuario;
            string strSalt = "";
            objCatSistemas.passSistema = clsCifrados.ComputeSha256HashWithSalt(catSistemas.Password, out strSalt);
            objCatSistemas.salt = strSalt;
            objCatSistemas.rol = catSistemas.rol;
            objCatSistemas.IdCatCliente = catSistemas.IdCatCliente;


            _db.catSistemas.Add(objCatSistemas);
            return Guardar(out strError);


        }

        public CatSistemas obtenerSistema(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<CatSistemas> obtenerSistemas()
        {
            throw new NotImplementedException();
        }

        public CatSistemas obtieneSistema(string userSistema, string passSistema, out string strError)
        {
            throw new NotImplementedException();
        }

        public bool validaCredenciales(string userSistema, string passSistema, out string strError)
        {
            throw new NotImplementedException();
        }

        public async Task<SistemaLoginRespuestaDTO> Login(SistemaLoginDTO usuarioLoginDto)
        {

            try
            {

                //Consultamos por Usuario para obtener el SALT y compara contraseñas.
                var usuario = _db.catSistemas.FirstOrDefault(
                    u => u.userSistema.ToLower() == usuarioLoginDto.sUsuario.ToLower()
                    );
                if (usuario != null)
                {
                    //var passwordEncriptado = clsCifrados.obtenermd5(usuarioLoginDto.sPass);
                    var passwordEncriptado = clsCifrados.VerifyPassword(usuarioLoginDto.sPass, usuario.passSistema, usuario.salt);
                    if (passwordEncriptado)
                    {

                        //Aquí existe el usuario entonces podemos procesar el login
                        var manejadoToken = new JwtSecurityTokenHandler();
                        var key = Encoding.ASCII.GetBytes(claveSecreta);

                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                                new Claim(ClaimTypes.NameIdentifier, usuario.IdCatSistema.ToString()),
                                new Claim(ClaimTypes.Name, usuario.userSistema),
                                new Claim(ClaimTypes.Spn, usuario.IdCatCliente.ToString()),
                                new Claim(ClaimTypes.Sid, usuario.IdCatEmpresas.ToString()),
                                new Claim(ClaimTypes.Role, usuario.rol)
                            }),
                            Expires = DateTime.UtcNow.AddDays(1),
                            SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };
                        var token = manejadoToken.CreateToken(tokenDescriptor);

                        SistemaLoginRespuestaDTO usuarioLoginRespuestaDto = new SistemaLoginRespuestaDTO()
                        {
                            Token = manejadoToken.WriteToken(token),
                            Usuario = usuario.userSistema,
                            StatusCode = HttpStatusCode.OK
                        };
                        return usuarioLoginRespuestaDto;
                    }
                    else
                    {
                        return new SistemaLoginRespuestaDTO()
                        {
                            Token = "",
                            Usuario = null,
                            StatusCode = HttpStatusCode.NotFound,
                            Error = "Error en verificación de contraseña"
                        };
                    }
                }
                else
                {
                    return new SistemaLoginRespuestaDTO()
                    {
                        Token = "",
                        Usuario = null,
                        StatusCode = HttpStatusCode.NotFound,
                        Error = "Error usuario no encontrado"
                    };
                }





            }
            catch (SqlException SQLEx)
            {
                return new SistemaLoginRespuestaDTO()
                {
                    Token = "",
                    Usuario = null,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Error = SQLEx.Message
                };
            }
            catch (Exception ex)
            {
                return new SistemaLoginRespuestaDTO()
                {
                    Token = "",
                    Usuario = null,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Error = ex.Message
                };
            }





        }

        public bool Guardar(out string strError)
        {
            try
            {
                strError = "";
                return _db.SaveChanges() >= 0 ? true : false;
            }
            catch (DbUpdateException dbEx)
            {
                // Manejo de errores relacionados con la actualización de la base de datos
                //return StatusCode(500, $"Database update error: {dbEx.Message}");
                strError = dbEx.Message;
                return false;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }
    }
}
