using System.ComponentModel.DataAnnotations;

namespace FirstWebApp.Models;

public class LoginDataModel
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Не будь редиской введи имя")]
    public string? PlayerName { get; set; } 
}