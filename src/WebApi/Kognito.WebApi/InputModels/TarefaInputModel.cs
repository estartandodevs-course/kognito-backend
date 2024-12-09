using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kognito.Usuarios.App.Domain;

namespace Kognito.WebApi.InputModels
{
    public class TaskInputModel
    {
        [Required(ErrorMessage = "A descrição é obrigatória")]
        public string Description { get; set; }

        [Required(ErrorMessage = "O conteúdo é obrigatório")]
        public string Content { get; set; }

        [Required(ErrorMessage = "A data final de entrega é obrigatória")]
        public DateTime FinalDeliveryDate { get; set; }

        [Required(ErrorMessage = "A turma é obrigatória")]
        public Guid ClassId { get; set; }

        public List<Neurodivergencia> NeurodivergenceTargets { get; set; } = new();
    }
}