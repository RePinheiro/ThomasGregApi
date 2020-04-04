using System.Web;
using System.Web.SessionState;

namespace ThomasGregAPI.Web.Utils
{
    public class SessionManager
    {
        private static HttpSessionState HttpSessionState { get { return HttpContext.Current.Session; } }

        public static void ClearSessions()
        {
            HttpSessionState.RemoveAll();
            HttpSessionState.Abandon();
            HttpSessionState.Clear();
        }


        public static object UsuarioModel
        {
            get { return HttpSessionState["Usuario"]; }
            set { HttpSessionState["Usuario"] = value; }
        }

    }
}