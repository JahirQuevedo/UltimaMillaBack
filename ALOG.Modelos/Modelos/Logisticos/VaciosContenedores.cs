using ALOG.Modelos.Modelos.Vacios;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace ALOG.Modelos.Modelos.Logisticos
{
    public class VaciosContenedores
    {
        [Key]
        public int IdVaciosContenedor { get; set; }
        [Required]
        public string Contenedor { get; set; }
        [Required]
        public string RefenciaCliente { get; set; }
        [Required]
        public string ClaveTipoContenedor { get; set; }
        public int PatioId { get; set; }
        public string Patio_RazonSocial { get; set; }
        public string Patio_RFC { get; set; }
        public string Moneda { get; set; }
        public DateTime FechaTocaPiso { get; set; }
        public string BL { get; set; }
        public string Buque { get; set; }
        public int ClienteId { get; set; }
        [Required]
        public string Cliente_RazonSocial { get; set; }
        [Required]
        public string Cliente_RFC { get; set; }
        [Required]
        public string Cliente_Solicitante { get; set; }
        public int ConsignadoId { get; set; }
        public string Consignado_RazonSocial { get; set; }
        public string Consignado_RFC { get; set; }
        public string FondoFinanciamiento { get; set; }
        public double MontoSolicitud { get; set; }
        public double MontoTotal { get; set; }
        public int AduanaId { get; set; }
        public DateTime FechaSolDevolucion { get; set; }
        public DateTime FechaPagoGarantiaNav { get; set; }
        [Required]
        public string Naviera_RFC { get; set; }

        public int Naviera_Id { get; set; }
        [Required]
        public string Naviera_RazonSocial { get; set; }
        [Required]
        public string Aduana { get; set; }
        public ICollection<PeticionesServicios> Servicios { get; set; }


        [ForeignKey("PeticionesReferencias")]
        public int IdReferencia { get; set; }
        public PeticionesReferencias PeticionesReferencias { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public DateTime FechaCierre { get; set; }

        public string EstadoContenedor { get; set; } = "A";
        public bool Activo { get; set; } = true;

        public string ReferenciaFacturacion { get; set; }
    }
}
