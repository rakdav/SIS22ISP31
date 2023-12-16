using System.ComponentModel.DataAnnotations;

namespace PartyInvites.Models
{
    public class GuestResponse
    {
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
