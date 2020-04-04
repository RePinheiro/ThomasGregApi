using ThomasGregAPI.Model.Enum;
using ThomasGregAPI.Model.Interfaces;

namespace ThomasGregAPI.Model.Entidades
{
    public class RespostaModel
    {
        public StatusResposta Status { get; set; }
        public object Conteudo { get; set; }
    }
}
