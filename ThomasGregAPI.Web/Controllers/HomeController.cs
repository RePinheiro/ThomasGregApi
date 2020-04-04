using System;
using System.Web.Mvc;
using ThomasGregAPI.Model.Entidades;
using ThomasGregAPI.Model.Enum;
using ThomasGregAPI.Services.Interface;
using ThomasGregAPI.Web.Utils;

namespace ThomasGregAPI.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoginService _loginService;

        public HomeController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        #region Login
        public ActionResult Login()
        {
            ViewBag.Title = "Login";
            return View();
        }

        [HttpPost]
        public JsonResult EfetuarLogin(string Usuario, string Senha)
        {
            try
            {
                var Login = _loginService.AutenticarUsuario(Usuario, Senha);
                if (Login.Status == StatusResposta.Sucess)
                {
                    SessionManager.UsuarioModel = new UsuarioModel
                    {
                        ID = Login.Conteudo.ToString(),
                        Usuario = Usuario,
                        Senha = Senha
                    };
                }
                return Json(Login, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region SigIn
        public ActionResult Sigin()
        {
            ViewBag.Title = "SigIn";
            return View();
        }

        [HttpPost]
        public JsonResult EfetuarSigin(string Usuario, string Senha)
        {
            try
            {
                var Sigin = _loginService.CadastrarUsuario(Usuario, Senha);
                if (Sigin.Status == StatusResposta.Sucess)
                {
                    SessionManager.UsuarioModel = new UsuarioModel
                    {
                        Usuario = Usuario,
                        Senha = Senha
                    };
                }

                return Json(Sigin, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Alterar Senha

        public ActionResult AlterarSenha()
        {
            var Usuario = (SessionManager.UsuarioModel as UsuarioModel);

            if (Usuario == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.Info = Usuario.Usuario;
                return View();
            }

        }

        public JsonResult AlteraSenha(string Usuario, string SenhaAntiga, string SenhaNova)
        {
            ViewBag.Title = "Alterar Senha";

            return Json(_loginService.AlterarUsuario(Usuario, SenhaAntiga, SenhaNova), JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Deletar Usuario
        public ActionResult DeletarUsuario()
        {

            var Usuario = (SessionManager.UsuarioModel as UsuarioModel);

            if (Usuario == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var Resposa = _loginService.DeletarUsuario(Usuario.ID.ToString());

                if (Resposa.Status == StatusResposta.Sucess)
                {
                    SessionManager.UsuarioModel = null;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View();
                }
            }
        }
        #endregion

        
    }
}
