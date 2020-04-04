using System.Collections.Generic;
using ThomasGregAPI.Model.Entidades;

namespace ThomasGregAPI.Model.Entidade
{
    public class ClienteModel 
    {
        public string Email { get; set; }
        public string Nome { get; set; }
        public List<LogradouroModel> Logradouro { get; set; }
        public string Logotipo { get; set; }
    }
}
