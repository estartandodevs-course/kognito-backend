using System.ComponentModel.DataAnnotations;
using Kognito.Usuarios.App.Domain;

namespace Kognito.WebApi.InputModels;

/// <summary>
/// Modelo para adicionar neurodivergência a um usuário
/// </summary>
public class AdicionarNeurodivergenciaInputModel
{
    /// <summary>
    /// Código do responsável que autoriza a adição da neurodivergência
    /// </summary>
    /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
    [Required(ErrorMessage = "O código do responsável é obrigatório")]
    public Guid parentCode { get; set; }

    /// <summary>
    /// Tipo de neurodivergência a ser adicionada
    /// </summary>
    /// <example>TDAH</example>
    [Required(ErrorMessage = "A neurodivergência é obrigatória")]
    public Neurodivergencia Neurodivergence { get; set; }
}
