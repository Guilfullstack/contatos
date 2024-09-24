using System.ComponentModel.DataAnnotations;
using ControleContatos.Enums;

namespace ControleContatos.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A dicione o nome do usuario")]
        public string Nome { get; set; }
        [EmailAddress(ErrorMessage = "Email inválido")]
        [Required(ErrorMessage = "A dicione o email do usuario")]
        public string Email { get; set; }
        [Required(ErrorMessage = "A dicione o login do usuario")]
        public string Login { get; set; }
        [Required(ErrorMessage = "A dicione a senha do usuario")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "A dicione o perfil do usuario")]
        public PerfilEnum Perfil{ get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public UsuarioModel(int id, string name, string email,string login,string senha,PerfilEnum perfil,DateTime dataCadastro,DateTime? dataAtualizacao)
        {
            Id=id;
            Nome=name;
            Email=email;
            Login=login;
            Senha=senha;
            Perfil=perfil;
            DataCadastro=dataCadastro;
            DataAtualizacao=dataAtualizacao;
        }
        public UsuarioModel( ) { }

    }
}