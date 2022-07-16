using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PM2P2T3.Models
{
    public class Audio
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Url { get; set; }
        public string Descripcion { get; set; }
        public DateTime Date { get; set; }
    }
}
