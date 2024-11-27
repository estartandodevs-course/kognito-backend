namespace Kognito.Usuario.App.ViewModels;

    public class EmblemaViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime? DesbloqueadoEm { get; set; }

        public EmblemaViewModel() { }

        public EmblemaViewModel(Guid id, string nome, string descricao, DateTime? desbloqueadoEm)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            DesbloqueadoEm = desbloqueadoEm;
        }
    }

