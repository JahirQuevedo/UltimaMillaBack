using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.Vacios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALOG.Modelos.Modelos.Logisticos
{
    public class VaciosServiciosCont
    {
        [Key]
        public int IdVaciosServiciosCont { get; set; }
        [ForeignKey("catServicios")]   
        public int IdCatServivios { get; set; }
        public CatServicios catServicios { get; set; }
        public string DescServicio { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public DateTime FechaCierre { get; set; }

        public ICollection<PeticionesDocumentos> Documentos { get; set; }



        [ForeignKey("vaciosContenedores")]
        public int IdVaciosContenedor { get; set; }
        public VaciosContenedores vaciosContenedores { get; set; }

        public string EstadoServicio { get; set; } = "A";

        public bool Activo { get; set; } = true;

    }
}
