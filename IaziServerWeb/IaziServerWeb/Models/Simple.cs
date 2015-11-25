using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IaziServerWeb.Models
{
    public static class Simple
    {
        //criptografar senha
        public static string Encrypt(string texto)
        {
            try
            {
                Byte[] by = System.Text.ASCIIEncoding.ASCII.GetBytes(texto);
                string novoTexto = Convert.ToBase64String(by);

                return novoTexto;
            }
            catch (Exception erro)
            {
                throw (new Exception(erro.Source + " - " + erro.Message));
            }
        }

        public static string Decrypt(string texto)
        {
            try
            {
                Byte[] by = Convert.FromBase64String(texto);
                string novoTexto = System.Text.ASCIIEncoding.ASCII.GetString(by);

                return novoTexto;
            }
            catch (Exception erro)
            {
                throw (new Exception(erro.Source + " - " + erro.Message));
            }
        }
        public static Usuario VerificarUsuario(string password, int user)
        {
            try
            {
                var usuario = new Usuario();
                
                using (DBContext db = new DBContext())
                {
                    var check = Encrypt(password);
                    var pass = Decrypt(password);
                    var query = from u in db.Usuario
                                where u.idUsuario == user && u.senhaUsuario == pass
                                select u;
                    foreach (Usuario u in query)
                    {
                        usuario = u;
                    }
                }
                return usuario;
            }catch(Exception)
            {
                return null;
            }
        }
    }
}
