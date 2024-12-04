// using System.ComponentModel.DataAnnotations;
//
// namespace Kognito.WebApi.InputModels;
//
// public class UsuarioInputModel
// {
//     [Required(ErrorMessage = "Informe um nome!")]
//     public string Nome { get; set; }
//
//     [Required(ErrorMessage = "Informe um CPF!")]
//     public string Cpf { get; set; }
//
//     public string? Neurodivergencia { get; set; }
//
//     [Required(AllowEmptyStrings = true)]
//     [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
//     public string Email { get; set; }
//
//     [Required(ErrorMessage = "Informe uma senha!")]
//     [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 5)]
//     public string Senha { get; set; }
// }
//
// public class AtualizarUsuarioInputModel
// {
//     [Required(ErrorMessage = "Informe um nome!")]
//     public string Nome { get; set; }
//
//     public string? Neurodivergencia { get; set; }
// }