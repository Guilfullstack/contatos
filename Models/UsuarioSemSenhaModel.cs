using System.ComponentModel.DataAnnotations;
using ControleContatos.Enums;

namespace ControleContatos.Models
{
    public class UsuarioSemSenhaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A dicione o nome do usuario")]
        public string Nome { get; set; }
        [EmailAddress(ErrorMessage = "Email inválido")]
        [Required(ErrorMessage = "A dicione o email do usuario")]
        public string Email { get; set; }
        [Required(ErrorMessage = "A dicione o login do usuario")]
        public string Login { get; set; }
       
        [Required(ErrorMessage = "A dicione o perfil do usuario")]
        public PerfilEnum? Perfil{ get; set; }
      
        public UsuarioSemSenhaModel(int id, string name, string email,string login,PerfilEnum perfil)
        {
            Id=id;
            Nome=name;
            Email=email;
            Login=login;
            Perfil=perfil;
          
        }
        public UsuarioSemSenhaModel( ) { }

    }
}