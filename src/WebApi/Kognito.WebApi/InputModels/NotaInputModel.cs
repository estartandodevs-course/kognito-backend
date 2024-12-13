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
    
}
