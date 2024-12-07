using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kognito.WebApi.InputModels;

public class GradeInputModel
{
   
    /// <summary>
    /// Valor da nota atribuída
    /// </summary>
    [Required(ErrorMessage = "O valor da nota é obrigatório")]
    [Range(0, 10, ErrorMessage = "A nota deve estar entre 0 e 10")]
    public double GradeValue { get; set; }

    /// <summary>
    /// ID do aluno que receberá a nota
    /// </summary>
    [Required(ErrorMessage = "O ID do aluno é obrigatório")]
    public Guid StudentId { get; set; }

    /// <summary>
    /// ID da turma onde a tarefa foi entregue
    /// </summary>
    [Required(ErrorMessage = "O ID da turma é obrigatório")]
    public Guid ClassId { get; set; }
    
}
