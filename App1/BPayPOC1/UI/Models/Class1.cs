using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPayPOCns.Models
{

    public class TablaItem
    {
        [JsonProperty("Columna_su_pago")]
        public double Columna_su_pago { get; set; }

        [JsonProperty("Columna_unidad")]
        public int Columna_unidad { get; set; }

        [JsonProperty("Columna_fecha")]
        public DateTime Columna_fecha { get; set; }

        [JsonProperty("Columna_cantidad_original")]
        public double Columna_cantidad_original { get; set; }

        [JsonProperty("Columna_balance")]
        public double Columna_balance { get; set; }

        [JsonProperty("Columna_concepto")]
        public string Columna_concepto { get; set; }

        [JsonProperty("Columna_descripcion")]
        public string Columna_descripcion { get; set; }
    }

    /* public class TablaItem
     {
           public string Descripcion { get; set; }
            public string Tamanho { get; set; }

          [JsonProperty("Columna_su_pago")]
          public double Columna_su_pago { get; set; }

         [JsonProperty("Columna_unidad")]
         public int Columna_unidad { get; set; }

         [JsonProperty("Columna_fecha")]
         public DateTime Columna_fecha { get; set; }

         [JsonProperty("Columna_cantidad_original")]
         public double Columna_cantidad_original { get; set; }

         [JsonProperty("Columna_balance")]
         public double Columna_balance { get; set; }

         [JsonProperty("Columna_concepto")]
         public string Columna_concepto { get; set; }

         [JsonProperty("Columna_descripcion")]
         public string Columna_descripcion { get; set; }
         // Agrega mas propiedades según las columnas de tu tabla
     }*/

}
