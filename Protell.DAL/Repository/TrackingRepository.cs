using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protell.Model;
using System.Net;
using System.Net.Sockets;
using Microsoft.Win32;
using System.Management;
using System.Web.Script.Serialization;

namespace Protell.DAL.Repository
{
    class TrackingRepository
    {
        public bool InsertTracking(TrackingModel tracking)
        {
            bool x = false;
            using (var entity =new db_SeguimientoProtocolo_r2Entities())
            {
                try
                {
                    entity.CI_TRACKING.AddObject(new CI_TRACKING()
                    {
                        IdTracking=tracking.IdTracking,
                        Accion=tracking.Accion,
                        Valor=tracking.Valor,
                        Ip=tracking.Ip,
                        Equipo=tracking.Equipo,
                        Ubicacion=tracking.Ubicacion,
                        IdRegistro=tracking.IdRegistro,
                        IdUsuario=tracking.IdUsuario ,
                        IsModified=tracking.IsModified,
                        LastModifiedDate=tracking.LastModifiedDate,
                        ServerLastModifiedDate=tracking.ServerLastModifiedDate
                    });
                    entity.SaveChanges();
                }
                catch (Exception)
                {
                    x = false;
                }
            }
            return x;
        }

        public string GetIP()
        {
            string ip = "";
            try
            {
                var strHostName = Dns.GetHostName();
                var ipEntry = Dns.GetHostEntry(strHostName);
                var addr = ipEntry.AddressList;
                var q = from a in addr
                        where a.AddressFamily == AddressFamily.InterNetwork
                        select a;
                ip = q.Last().ToString();
            }
            catch (Exception ex)
            {
                ip = ex.Message;
            }
            return ip;
        }

        public Model.TrackingModel createTracking(RegistroModel registro,UsuarioModel usuario,string accion)
        {
            Model.TrackingModel track = new Model.TrackingModel();            
            try
            {
                string valor="";
                valor = new JavaScriptSerializer().Serialize(registro);
                track.IdTracking = long.Parse(( String.Format("{0:yyyy:MM:dd:HH:mm:ss:fff}", DateTime.Now) ).Replace(":", ""));
                track.Accion = accion;
                track.Valor = valor;
                track.Ip = GetIP();
                track.Equipo = Environment.MachineName;
                track.Ubicacion = Environment.OSVersion.ToString();                
                track.IdRegistro = registro.IdRegistro;
                track.IdUsuario = usuario.IdUsuario;
                track.IsModified = true;
                track.LastModifiedDate = long.Parse((String.Format("{0:yyyy:MM:dd:HH:mm:ss:fff}", DateTime.Now)).Replace(":", ""));
                track.ServerLastModifiedDate = 0;
            }
            catch (Exception)
            {
                ;
            }
            return track;
        }
    }
}
