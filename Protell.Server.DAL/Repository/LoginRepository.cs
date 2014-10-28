using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model.IRepository;
using System.Collections.ObjectModel;
using Protell.Server.DAL.POCOS;
using Newtonsoft.Json;
using Protell.Model;
using Protell.Server.DAL.JSON;

using System.Net.Mail;
using System.Configuration;

namespace Protell.Server.DAL.Repository
{
    public class LoginRepository
    {
        public wappSpAttemptLogUser_Result5 UserSignIn(string userMail, string userPwd, bool saveSession)
        {
            wappSpAttemptLogUser_Result5 list = null;
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                list = entity.wappSpAttemptLogUser(userMail, userPwd, saveSession).ToList().First();
            }
            return list;
        }

        public wappSpGetUserInfo_Result GetUserBySessionId(string sessionId)
        {
            wappSpGetUserInfo_Result list = null;
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                list = entity.wappSpGetUserInfo(sessionId).ToList().First();
            }
            return list;
        }


        public wappSpValidateMail_Result3 SendMailUser(string usermail)
        {
            wappSpValidateMail_Result3 list = null;
            string mailServer = ConfigurationManager.AppSettings["MailServer"];
            string MailServerPass = ConfigurationManager.AppSettings["MailServerPass"];
            //string mailServer = ConfigurationManager.AppSettings["GDataUserName"];
            //string MailServerPass = ConfigurationManager.AppSettings["GDataUserPassword"];
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                list = entity.wappSpValidateMail(usermail).ToList().First();
                if (list.IdUsuario != null && list.IdSesion == null)
                {
                    string i = "Usuario no encontrado";
                }
                else
                {
                    try
                    {                       
                        MailMessage mail = new MailMessage();
                        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                        mail.From = new MailAddress(mailServer);                        
                        mail.To.Add(usermail);
                        mail.Subject = "RECUPERACIÓN DE CONTRASEÑA";                        
                        mail.IsBodyHtml = true;
                        string htmlBody;
                        htmlBody = "<div>"
                                   + "<div>"
                                   + "<table style='width:100%;font-family:Tahoma'>"
                                   + "<tbody>"
                                   + "<tr><td colspan='2'>"
                                   + "<img src='https://ci5.googleusercontent.com/proxy/Rh3eBT1tluC2xtk-BEX6lI8qDUdjUvEpLiPdethYJoWe8F-hIDDk-pswK7p2MYxAHBsZTJ1pDiXNY-fjgMmlxqE=s0-d-e1-ft#http://www.cna.gob.mx/Imagenes/semarnat.jpg'  width='100%'>"
                                   + "</td></tr>"
                                   + "<tr><td colspan='2'><hr>"
                                   + "<br>"
                                   + "Usted a solicitado un cambio de contraseña."
                                   +"<br> <br></td>"
                                   + "</tr><tr>"
                                   + "<td style='text-align:center;background-color:#507fe0;color:White;font-family:Tahoma;font-size:large' colspan='2'>"
                                   + "ACTUALIZACIÓN DE CONTRASEÑA"
                                   + "</td></tr>"
                                   + "<tr><td style='text-align:right' colspan='2'></td></tr>"
                                   + "<tr>"
                                   +"<td style='width:20%;text-align:right;vertical-align:top'>"
                                   + "Url:"
                                   +"</td>"
                                   +"<td>"
                                   + "<a href='"
                                   + "http://localhost:63342/LluviasAppLogin/RecovPass/index.html#/"
                                   + "Key:" + list.IdSesion
                                   +"'>"
                                   + "http://localhost:63342/LluviasAppLogin/RecovPass/index.html#/"
                                   + "Key:" + list.IdSesion
                                   +"</a>"
                                   + "</td>"
                                   +"</tr>"
                                   + "<tr>"
                                   + "<td style='width:20%;text-align:right;vertical-align:top'>"
                                   + "Email:"
                                   + "</td>"
                                   + "<td>"
                                   + usermail
                                   + "</td>"
                                   + "</tr>"
                                   + "<tr>"
                                   + "<td style='width:20%;text-align:right;vertical-align:top'>"
                                   + "Llave Unica:"
                                   + "</td>"
                                   + "<td>"
                                   + list.IdSesion
                                   + "</td>"
                                   + "</tr>"
                                   +"</tbody></table>"
                                   + "</div><div class='yj6qo'></div>"
                                   + "</div>";

                        mail.Body = htmlBody;
                        SmtpServer.Port = 587;
                        SmtpServer.Credentials = new System.Net.NetworkCredential(mailServer, MailServerPass);
                        SmtpServer.EnableSsl = true;

                        SmtpServer.Send(mail);                       
                    }
                    catch (Exception ex)
                    {
                        var err = ex.Message;
                    }                  
                }
            }
            return list;
        }

        public bool LogOutUser(string KeySesion, long IdUsuario)
        {
            bool res = true;
            ObservableCollection<WAPP_USUARIO_SESION> Key = new ObservableCollection<WAPP_USUARIO_SESION>();

            try
            {
                using (var entity_ = new db_SeguimientoProtocolo_r2Entities())
                {
                    (from s in entity_.WAPP_USUARIO_SESION
                     where s.IdSesion == KeySesion
                     select s).ToList().ForEach(row =>
                     {
                         Key.Add(new WAPP_USUARIO_SESION()
                         {
                             IdUsuario = row.IdUsuario,
                             IdSesion = row.IdSesion
                         });
                     });
                    if (Key[0].IdSesion == KeySesion.ToString())
                    {
                        using (var entity = new db_SeguimientoProtocolo_r2Entities())
                        {
                            entity.wappSpLogOut(IdUsuario);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var errr = ex.Message;
            }
            return res;
        }
        
    }
}
