using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace PM2E11800.Models
{
    public class sitios
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(70)]
        public double latitud { get; set; }

        [MaxLength(70)]
        public double longitud { get; set; }

        [MaxLength(250)]
        public string descripcion { get; set; }

        public string imagen { get; set; }
    }
}
