using System.ComponentModel.DataAnnotations;
namespace L012020AM601.Models
{
    public class motorista
    {
        [Key]
        public int motoristaId { get; set; }
        public string nombreMotorista { get; set; }
    }
}
