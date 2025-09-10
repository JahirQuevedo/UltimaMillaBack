namespace ALOG.Modelos.Modelos.DTO.Respuestas
{
    public class RespObtenerAcarreosDTO
    {
        public int idDtAcarreos { get; set; }

        public int Mes { get; set; }



        public DateTime Fecha { get; set; }

        public DateTime FechaRegistro { get; set; }
        public string NombreServicio { get; set; }

        public string NumeroContenedor { get; set; }
        public int IdCliente { get; set; } = 0;



        public int IdCatTipoEstado { get; set; }
        public string TipoEstado { get; set; }
        public string NombreCliente { get; set; }
        public int IdEmpresa { get; set; }
        public string EmpresaRazonSocial { get; set; }

        public int IdCatAduana { get; set; }
        public string NombreAduana { get; set; }
        public int IdOrden { get; set; }
        public bool Activo { get; set; }

        public int IdCatServicio { get; set; }
        public int IdUsarioRegistro { get; set; }
        public int IdCatProveedor { get; set; }
        public string Usuario { get; set; }


    }
}
