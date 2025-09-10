using ALOG.Modelos.Modelos.DTO.Respuestas;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ALOG.Repositorios.Repositorio.Control
{
    public static class ControlAccesoUnificadoRepositorio
    {
        public static RespuestaTokenDTO ObtenerUsuarioTokenUnificado(this HttpContext context)
        {
            if (context?.User == null)
                return null;

            var user = context.User;
            var usuarioToken = new RespuestaTokenDTO
            {
                IdCatEmpresa = new List<int>(),
                RolesUsuario = new List<string>()
            };

            // 1) Determinar si es SISTEMA o usuario normal
            if (user.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "SISTEMA"))
            {
                // => Usuario de catSistema
                var idCatSistemaClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(idCatSistemaClaim, out int idCatSistema))
                    usuarioToken.IdCatSistema = idCatSistema;
            }
            else
            {
                // => Usuario de catUsuarios
                var idCatUsuarioClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrWhiteSpace(idCatUsuarioClaim) && int.TryParse(idCatUsuarioClaim, out int idCatUsuario))
                    usuarioToken.IdCatUsuario = idCatUsuario;
            }

            // 2) Nombre del usuario
            var userClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            if (!string.IsNullOrWhiteSpace(userClaim))
                usuarioToken.User = userClaim;

            // 3) IdCatCliente
            var idCatClienteClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Spn)?.Value;
            if (int.TryParse(idCatClienteClaim, out int idCatCliente))
                usuarioToken.IdCatCliente = idCatCliente;

            // 4) Empresas (ClaimTypes.Sid)
            var listaEmpresas = user.Claims.Where(c => c.Type == ClaimTypes.Sid && int.TryParse(c.Value, out _))
                                            .Select(c => int.Parse(c.Value)).ToList();
            if (listaEmpresas.Any())
                usuarioToken.IdCatEmpresa.AddRange(listaEmpresas);

            // 5) Roles
            var rolesUsuario = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            if (rolesUsuario.Any())
                usuarioToken.RolesUsuario.AddRange(rolesUsuario);

            // 6) Email
            var userEmail = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            // Asignar el email solo si tiene contenido
            if (!string.IsNullOrWhiteSpace(userEmail))
                usuarioToken.Email = userEmail;

            // 7) Si tiene rol ADMIN o ADMINUSER y el User es "ALOgistics", IdCatCliente = null
            if (usuarioToken.RolesUsuario.Contains("ADMIN") || 
                usuarioToken.RolesUsuario.Contains("ADMINUSER") ||
                ("ALogistics".Equals(usuarioToken.User, StringComparison.OrdinalIgnoreCase) && usuarioToken.RolesUsuario.Contains("SISTEMA"))
               )
            {
                usuarioToken.IdCatCliente = null;
            }

            return usuarioToken;
        }
    }
}
