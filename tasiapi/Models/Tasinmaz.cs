using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace tasiapi.Models
{
    [Table("tasinmazlar")]
    public class Tasinmaz
    {
        //[Key]
        //[Column("Id")]
        public int Id { get; set; }

        //[Column("UserId")]
        public int UserId { get; set; }

        //[Column("Il_ad")]
        public string Il_ad { get; set; }

        //[Column("Ilce_ad")]
        public string Ilce_ad { get; set; }

        //[Column("Mahalle_ad")]
        public string Mahalle_ad { get; set; }

        //[Column("Ada")]
        public string Ada { get; set; }

        //[Column("Parsel")]
        public int Parsel { get; set; }
            
        
    }
   

}
