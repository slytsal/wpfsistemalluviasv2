using Protell.Model;
using Protell.Model.SyncModels;
using Protell.Server.DAL.POCOS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;

namespace Protell.Server.DAL.Repository.v2
{
    public class CiRegistroRepository
    {
        private string GetTmpUploadedTable(long unid)
        {
            return "TMP_CI_REGISTRO_UPLOADED_" + unid.ToString();
        }

        private string GetSqlInsertString(RegistroModel r,bool notIncludeUnion)
        {
            string sql = "select ";
            sql+=r.IdRegistro.ToString()+",";
            sql += r.IdPuntoMedicion.ToString() + ",";
            sql += "'"+String.Format("{0:yyyyMMdd HH:mm:ss}", r.FechaCaptura)+"',";
            sql += r.HoraRegistro.ToString() + ",";
            sql += r.DiaRegistro.ToString() + ",";
            sql += r.Valor.ToString() + ",";
            sql += "'"+r.AccionActual.ToString() + "',";
            sql += (r.IsActive ? 1 : 0).ToString() + ",";
            sql += (r.IsModified ? 1 : 0).ToString() + ",";
            sql += r.LastModifiedDate.ToString() + ",";
            sql += r.IdCondicion.ToString() + ",";
            sql += r.ServerLastModifiedDate.ToString() + ",";
            sql += r.FechaNumerica.ToString() + "";
            sql += (notIncludeUnion) ? "": " union all";

            return sql;
        }

        //Upsert a la info recibida del cliente
        public long PrepareBulkUpsertUploaded()
        {
            long session = 0;
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                session = (new UNID()).getNewUNID();
                string sqlCmd = "select TOP 1 * into " + this.GetTmpUploadedTable(session) + " from CI_REGISTRO; TRUNCATE TABLE " + this.GetTmpUploadedTable(session);
                entity.ExecuteStoreCommand(sqlCmd);
            }

            return session;
        }

        public void BulkUpsertUploaded(ObservableCollection<Model.RegistroModel> registros,long unidSession)
        {
            List<Thread> workerThreads = new List<Thread>();

            string _base = "Insert into " + this.GetTmpUploadedTable(unidSession) + " ";
            string sqlStatement = _base;

            int maxReg = 0;
            int i = 0;
            int batch = 1000;

            if (registros != null && registros.Count > 0)
            {
                maxReg = registros.Count-1;
                foreach (var r in registros)
                {
                    if ((i+1) % batch == 0)
                    {
                        sqlStatement += " " + this.GetSqlInsertString(r, true);
                        workerThreads.Add(new Thread(CiRegistroRepository.ExecuteSqlString));
                        workerThreads.Last().IsBackground = true;
                        workerThreads.Last().Start(sqlStatement);

                        sqlStatement = "Insert into " + this.GetTmpUploadedTable(unidSession) + " ";
                    }
                    else
                    {
                        sqlStatement += " " + this.GetSqlInsertString(r, ((maxReg == i) ? true : false));
                    }

                    i++;
                }

                if (sqlStatement != _base)
                {
                    workerThreads.Add(new Thread(CiRegistroRepository.ExecuteSqlString));
                    workerThreads.Last().IsBackground = true;
                    workerThreads.Last().Start(sqlStatement);
                }

                foreach (Thread thread in workerThreads)
                {
                    thread.Join();
                }
            }
        }

        public static void ExecuteSqlString(object sqlStmt)
        {
            string sSqlStmt = (string)sqlStmt;
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                entity.ExecuteStoreCommand(sSqlStmt);
            }
        }

        public void CommitBulkUpsertUploaded()
        {
            using (var entity = new db_SeguimientoProtocolo_r2Entities())
            {
                
            }
        }

        /// <summary>
        /// Metodo principal para proceso de insercion de información recibida del cliente
        /// </summary>
        public void UpsertUploaded(ObservableCollection<Model.RegistroModel> registros)
        {
            if (registros != null && registros.Count > 0)
            {
                long session = this.PrepareBulkUpsertUploaded();
                this.BulkUpsertUploaded(registros, session);
                this.CommitBulkUpsertUploaded();
            }
            else
            {

            }//endIfElse
        }
    }
}
