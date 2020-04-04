using System.Collections.Generic;
using ThomasGregAPI.Model.Entidades;

namespace ThomasGregAPI.Model.Interfaces
{
    public interface  IClienteModel
    {
        string Email { get; set; }
        string Nome { get; set; }
        List<LogradouroModel> Logradouros { get; set; }
        string Logotipo { get; set; }
    }
}
