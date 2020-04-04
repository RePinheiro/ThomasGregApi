using ThomasGregAPI.Model.Enum;

namespace ThomasGregAPI.Model.Interfaces
{
    public interface IRespostaModel
    {
        StatusResposta Status { get; set; }
        object Conteudo { get; set; }
    }
}
