using System.Data;
using Proyecto.Model;

namespace Proyecto.Controler
{
    internal class authControler
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public bool IniciarSesion()
        {
            authModel model = new authModel();
            DataTable ds = model.ValidarUsuario(Username, Password);

            if (ds != null && ds.Rows.Count > 0)
            {
                UserSession.Id = (int)ds.Rows[0]["Id"];
                UserSession.Username = ds.Rows[0]["Username"].ToString();
                UserSession.Role = ds.Rows[0]["Role"].ToString();
                return true;
            }
            return false;
        }
    }
}
