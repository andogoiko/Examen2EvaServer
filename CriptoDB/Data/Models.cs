using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cripto.Models
{
    public class Cartera
    {
        //Clave Principal NO AUTONUMERICA
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CarteraId { get; set; }
        public string Nombre { get; set; }
        public string Exchange { get; set; }

        //Escribe las propiedades de navegación a otras Entidades

        [System.Text.Json.Serialization.JsonIgnore]

        public List<Contrato> Contratos { get; } = new List<Contrato>();


        // A implementar
        public override string ToString() => $"La cartera {Nombre} se identifica como: {CarteraId}, pertenece a el exchange {Exchange} Y tiene los siguientes contratos: {Contratos.Count}";
    }
    public class Moneda
    {
        //Clave Principal String
        [Key]
        public string MonedaId { get; set; }
        public decimal Actual { get; set; }
        public decimal Maximo { get; set; }

        //Escribe las propiedades de navegación a otras Entidades

        [System.Text.Json.Serialization.JsonIgnore]
        public List<Contrato> Contrataciones { get; } = new List<Contrato>();

        // A implementar
        public override string ToString() => $"La cristomoneda #{MonedaId} tiene un valor actual de {Actual}, y un valor máximo de: {Maximo}, además consta de los siguientes contratos: {Contrataciones.Count}";
    }
    public class Contrato
    {
        //Decide cómo vas a implementar la clave principal

        //decido hacerla no autonumérica

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ContratoId { get; set; }

        //Escribe las propiedades de relación 1:N entre Moneda y Cartera

        public int CarteraId { get; set; }
        public string MonedaId { get; set; }

        public int Cantidad { get; set; }

        //Escribe las propiedades de navegación a otras Entidades

        [System.Text.Json.Serialization.JsonIgnore]
        public Cartera Cartera { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public Moneda Moneda { get; set; }


        // A implementar
        public override string ToString() => $"Cartera: #{CarteraId} X Moneda: #{MonedaId}";
    }

}