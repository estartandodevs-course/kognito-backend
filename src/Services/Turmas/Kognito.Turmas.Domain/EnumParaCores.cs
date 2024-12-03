using System.ComponentModel.DataAnnotations;

namespace Kognito.Turmas.Domain;

public enum Cor
{
    [Display(Name = "Português")]
    Portugues = 1,  // #0e6ba8
    
    [Display(Name = "Matemática")]
    Matematica = 2, // #ed4c5c
    
    [Display(Name = "Física")]
    Fisica = 3,     // #6db0dc
    
    [Display(Name = "Química")]
    Quimica = 4,    // #76129d
    
    [Display(Name = "Biologia")]
    Biologia = 5,   // #18924a
    
    [Display(Name = "História")]
    Historia = 6,   // #e3b128
    
    [Display(Name = "Geografia")]
    Geografia = 7   // #d4812d
}