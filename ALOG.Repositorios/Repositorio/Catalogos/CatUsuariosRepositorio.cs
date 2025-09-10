using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.DTO.Consultas;
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

    public class CatUsuariosRepositorio : ICatUsuariosRepositorio
    {
        private readonly ApplicationDbContext _db;
        UtileriasCifrados clsCifrados = new UtileriasCifrados();
        private readonly string claveSecreta;

        public CatUsuariosRepositorio(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            claveSecreta = configuration.GetValue<string>("ApiSettings:Secreta");
        }

        public async Task<CatUsuarios> CrearUsuario(CatUsuarios pCatUsuario)
        {
            if (pCatUsuario == null)
                return null;

            if (_db.catUsuarios.Any(u => u.Usuario.Trim().ToUpper() == pCatUsuario.Usuario.Trim().ToUpper()))
                return null;
            if (pCatUsuario.passSistema.Trim().Length <= 8)
                return null;

            try
            {


                var strError = "";
                CatUsuarios objCatUsario = new CatUsuarios();
                objCatUsario.Nombre = pCatUsuario.Nombre;
                objCatUsario.ApellidoPaterno = pCatUsuario.ApellidoPaterno;
                objCatUsario.ApellidoMaterno = pCatUsuario.ApellidoMaterno;
                objCatUsario.Correo = pCatUsuario.Correo;
                objCatUsario.Usuario = pCatUsuario.Usuario;
                objCatUsario.Puesto = pCatUsuario.Puesto;
                objCatUsario.Telefono = pCatUsuario.Telefono;

                string strSalt = "";
                objCatUsario.passSistema = clsCifrados.ComputeSha256HashWithSalt(pCatUsuario.passSistema, out strSalt);
                objCatUsario.salt = strSalt;
                objCatUsario.Activo = true;
                objCatUsario.FechaRegistro = DateTime.Now;
                _db.catUsuarios.Add(objCatUsario);
                var objResp = _db.SaveChanges();
                return objCatUsario;
            }
            catch (Exception)
            {

                return null;
            }

        }

        public bool Guardar()
        {
            try
            {
                _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                ////Console.WriteLine(ex.Message);
                return false;
            }


        }
        public CatUsuarios obtenerUsuario(int IdCatUsuario)
        {
            try
            {
                return _db.catUsuarios.FirstOrDefault(u => u.IdCatUsuarios == IdCatUsuario);
            }
            catch (Exception em)
            {

                return null;
            }
        }

        public ICollection<CatUsuarios> obtenerUsuarios(FiltroGenericoDTO pFiltro)
        {
            try
            {

                pFiltro.IdUsuario = pFiltro.IdUsuario == null ? 0 : pFiltro.IdUsuario;
                pFiltro.IdTipoPuesto = pFiltro.IdTipoPuesto == null ? 0 : pFiltro.IdTipoPuesto;
                pFiltro.IdEmpresa = pFiltro.IdEmpresa == null ? 0 : pFiltro.IdEmpresa;
                pFiltro.IdCliente = pFiltro.IdCliente == null ? 0 : pFiltro.IdCliente;
                pFiltro.IdLNegocio = pFiltro.IdLNegocio == null ? 0 : pFiltro.IdLNegocio;
                pFiltro.IdRol = pFiltro.IdRol == null ? 0 : pFiltro.IdRol;
                pFiltro.IdAduana = pFiltro.IdAduana == null ? 0 : pFiltro.IdAduana;
                pFiltro.IdPermiso = pFiltro.IdPermiso == null ? 0 : pFiltro.IdPermiso;
                pFiltro.Activo = pFiltro.Activo == null ? true : pFiltro.Activo;


                var lst = _db.catUsuarios.Include(a => a.catUsuarioRoles)
                    .Include(b => b.catTipoPuesto)
                    .Include(c => c.catUsuariosEmpresas)
                    .Include(d => d.catUsuariosPermisos)
                    .Include(e => e.catUsuariosAduanas)
                    .Where(a => (pFiltro.IdUsuario <= 0 || a.IdCatUsuarios == pFiltro.IdUsuario) &&
                                     (pFiltro.IdTipoPuesto <= 0 || a.IdCatTipoPuesto == pFiltro.IdTipoPuesto) &&
                                    (pFiltro.IdAduana <= 0 || a.catUsuariosAduanas.Any(b => b.IdCatAduana == pFiltro.IdAduana)) &&
                                    (pFiltro.IdEmpresa <= 0 || a.catUsuariosEmpresas.Any(b => b.idCatEmpresa == pFiltro.IdEmpresa)) &&
                                    (pFiltro.IdCliente <= 0 || a.catUsuariosEmpresas.Any(b => b.IdCatCliente == pFiltro.IdCliente)) &&
                                    (pFiltro.IdRol <= 0 || a.catUsuarioRoles.Any(b => b.IdCatRoles == pFiltro.IdRol)) &&
                                    (pFiltro.IdPermiso <= 0 || a.catUsuariosPermisos.Any(b => b.IdCatPermisos == pFiltro.IdPermiso)) &&
                                    (pFiltro.Activo == null || a.Activo == pFiltro.Activo) &&
                                    (string.IsNullOrEmpty(pFiltro.Nombre) || a.Nombre.Contains(pFiltro.Nombre)) &&
                                    (string.IsNullOrEmpty(pFiltro.ApellidoP) || a.ApellidoPaterno.Contains(pFiltro.ApellidoP)) &&
                                    (string.IsNullOrEmpty(pFiltro.ApellidoM) || a.ApellidoMaterno.Contains(pFiltro.ApellidoM)) &&
                                    (string.IsNullOrEmpty(pFiltro.RFC) || a.RFC.Contains(pFiltro.RFC)) &&
                                    (string.IsNullOrEmpty(pFiltro.Correo) || a.Correo.Contains(pFiltro.Correo)))


                        .ToList();
                return lst;


            }
            catch (Exception em)
            {
                return null;
            }
        }

        public async Task<SistemaLoginRespuestaDTO> Login(SistemaLoginDTO usuarioLoginDto)
        {

            try
            {

                //Consultamos por Usuario para obtener el SALT y compara contraseñas.
                var usuario = _db.catUsuarios.Include(x => x.catUsuarioRoles).FirstOrDefault(
                    u => u.Usuario.ToLower() == usuarioLoginDto.sUsuario.ToLower()
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
                                new Claim(ClaimTypes.Name, usuario.Nombre.ToString()),
                                new Claim(ClaimTypes.Email, usuario.Correo),
                                new Claim(ClaimTypes.NameIdentifier, usuario.IdCatUsuarios.ToString()),
                                new Claim(ClaimTypes.Role, usuario.catUsuarioRoles.FirstOrDefault().CatRoles.Nombre),
                                new Claim("TipoUsuario","1")
                            }),
                            Expires = DateTime.UtcNow.AddDays(1),
                            SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };
                        var token = manejadoToken.CreateToken(tokenDescriptor);

                        SistemaLoginRespuestaDTO usuarioLoginRespuestaDto = new SistemaLoginRespuestaDTO()
                        {
                            Token = manejadoToken.WriteToken(token),
                            Usuario = usuario.Usuario,
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

        public async Task<SistemaLoginRespuestaDTO> LoginUser(SistemaLoginDTO usuarioLoginDto)
        {
            try
            {
                // Consultamos por Usuario para obtener el SALT y comparar contraseñas
                var usuario = _db.catUsuarios
                    .Include(x => x.catUsuarioRoles)
                        .ThenInclude(ur => ur.CatRoles) // Incluimos los roles asociados al usuario
                    .Include(ue => ue.catUsuariosEmpresas)
                    .Include(uc => uc.catUsuariosClientes)
                    //.Include(uc => uc.catUsuariosClientes)
                    .FirstOrDefault(u => u.Usuario.ToLower() == usuarioLoginDto.sUsuario.ToLower());

                if (usuario != null)
                {
                    var passwordEncriptado = clsCifrados.VerifyPassword(usuarioLoginDto.sPass, usuario.passSistema, usuario.salt);
                    if (passwordEncriptado)
                    {
                        // Generamos los claims para el token, incluyendo los múltiples roles
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, usuario.IdCatUsuarios.ToString()), //Usuario    
                            new Claim(ClaimTypes.Name, usuario.Nombre.ToString()), //Nombre usuario
                            new Claim(ClaimTypes.Email, usuario.Correo)
                        };

                        if(usuario?.catUsuariosClientes != null || usuario.catUsuariosClientes.Any())
                        {
                            foreach(var usuarioCliente in usuario.catUsuariosClientes)
                            {
                                claims.Add(new Claim(ClaimTypes.Spn, usuarioCliente.IdCatCliente.ToString())); //IdCatCliente
                            }
                        }
                        
                        //// Agregamos los clientes a las que pertenece el usuario
                        //foreach (var usuarioCliente in usuario.catUsuariosClientes)
                        //{
                        //    claims.Add(new Claim(ClaimTypes.Spn, usuarioCliente.IdCatCliente.ToString())); //Clientes
                        //}
                        // Agregamos las empresas a las que pertenece el usuario
                        foreach (var usuarioEmpresa in usuario.catUsuariosEmpresas)
                        {
                            claims.Add(new Claim(ClaimTypes.Sid, usuarioEmpresa.idCatEmpresa.ToString())); //Empresas
                        }
                        //Agregamos cada rol del usuario como un Claim de tipo Role
                        foreach (var usuarioRol in usuario.catUsuarioRoles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, usuarioRol.CatRoles.Nombre));
                        }

                        // Creación del token con los claims generados
                        var manejadoToken = new JwtSecurityTokenHandler();
                        var key = Encoding.ASCII.GetBytes(claveSecreta);

                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(claims),
                            Expires = DateTime.UtcNow.AddDays(1),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };

                        var token = manejadoToken.CreateToken(tokenDescriptor);

                        // Preparamos la respuesta con el token generado
                        return new SistemaLoginRespuestaDTO
                        {
                            Exito = true,
                            Token = manejadoToken.WriteToken(token),
                            Usuario = usuario.Usuario,
                            StatusCode = HttpStatusCode.OK
                        };
                    }
                    else
                    {
                        return new SistemaLoginRespuestaDTO
                        {
                            Exito = false,
                            Token = "",
                            Usuario = null,
                            StatusCode = HttpStatusCode.NotFound,
                            Error = "Error en verificación de contraseña"
                        };
                    }
                }
                else
                {
                    return new SistemaLoginRespuestaDTO
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
                return new SistemaLoginRespuestaDTO
                {
                    Token = "",
                    Usuario = null,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Error = SQLEx.Message
                };
            }
            catch (Exception ex)
            {
                return new SistemaLoginRespuestaDTO
                {
                    Token = "",
                    Usuario = null,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Error = ex.Message
                };
            }
        }

        public Task<bool> BajaUsuario(CatUsuarios pCatUsuario)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ActivarUsuario(CatUsuarios pCatUsuario)
        {
            throw new NotImplementedException();
        }

        public async Task<CatUsuarios> ObtenerUsuario(int pIdCatUsuario)
        {
            try
            {
                var obj = await _db.catUsuarios.Include(up => up.catUsuariosPermisos).ThenInclude(up1 => up1.CatPermisos)
               .Include(up => up.catUsuarioRoles).ThenInclude(up1 => up1.CatRoles)
               .Include(up => up.catUsuariosAduanas).ThenInclude(up1 => up1.catAduana)
               .Include(up => up.catUsuariosEmpresas).ThenInclude(up1 => up1.catEmpresas)
               .Include(up => up.catTipoPuesto)
               .Where(x => x.IdCatUsuarios == pIdCatUsuario && x.Activo == true).FirstOrDefaultAsync();
                return obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Task<List<CatUsuarios>> ObtenerUsuarios(FiltroGenericoDTO pfiltroGenericoDTO)
        {
            throw new NotImplementedException();
        }
    }
}
