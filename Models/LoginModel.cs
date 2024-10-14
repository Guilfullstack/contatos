using System.ComponentModel.DataAnnotations;

namespace ControleContatos.Models
{
   public class LoginModel
    {
        [Required(ErrorMessage = "Informe o seu email")]
        //[EmailAddress(ErrorMessage = "Email inválido")]
        public string? Login { get; set;}
        [Required(ErrorMessage = "Informe sua senha")]
        public string? Senha { get; set;}
    
        public LoginModel()
        {
            
        }
    }
    
}