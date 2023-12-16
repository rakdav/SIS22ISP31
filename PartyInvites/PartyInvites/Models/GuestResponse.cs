using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartyInvites.Models
{
    public class GuestResponse
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Required(ErrorMessage ="Введите ваше имя")]
        public string? Name {  get; set; }
        [Required(ErrorMessage = "Введите ваш email")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Введите ваш телефон")]
        public string? Phone { get; set; }
        [Required(ErrorMessage = "Выберите согласны или нет")]
        public bool? WillAttend { get; set; }
    }
}
