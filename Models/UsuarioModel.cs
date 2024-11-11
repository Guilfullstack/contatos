using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using ControleContatos.Enums;
using ControleContatos.Helper;

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
        public PerfilEnum? Perfil{ get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public virtual List<ContatoModel>?Contatos{get; set;}
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
        public bool ValidarSenha(string senha){
            return Senha==senha.GerarHash();
        }
        public void SetSenhaHash(){
            Senha=Senha.GerarHash();
        }
        public void SetNovaSenha(string novaSenha){
            Senha=novaSenha.GerarHash();
        }
        public string GerarNovaSenha(){
            string novaSenha=Guid.NewGuid().ToString().Substring(0,8);
            Senha=novaSenha.GerarHash();
            return novaSenha;
        }

    }
}