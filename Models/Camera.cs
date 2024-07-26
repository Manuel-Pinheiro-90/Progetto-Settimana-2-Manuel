using System.ComponentModel.DataAnnotations;

namespace Progetto_Settimana_2_Manuel.Models
{
    public class Camera
    {
        public int ID { get; set; }
        [Required]
        public int Numero { get; set; }
        [Required]
       
        public string Descrizione { get; set; }
        [Required]
       
        public string Tipologia { get; set; }



    }
}
