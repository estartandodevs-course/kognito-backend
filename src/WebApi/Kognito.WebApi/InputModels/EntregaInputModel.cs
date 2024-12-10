using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kognito.WebApi.InputModels
{
    /// <summary>
    /// Modelo para entrega de uma tarefa
    /// </summary>
    public class DeliveryInputModel
    {
        /// <summary>
        /// Conteúdo da entrega da tarefa
        /// </summary>
        /// <example>Aqui está minha resolução do exercício...</example>
        public string Content { get; set; }
    }
}