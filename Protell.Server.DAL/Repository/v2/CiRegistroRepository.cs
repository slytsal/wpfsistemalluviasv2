using Protell.Model;
using Protell.Model.SyncModels;
using Protell.Server.DAL.POCOS;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Protell.Server.DAL.Repository.v2
{
    public class CiRegistroRepository
    {
        private string GetTmpUploadedTable(long unid)
        {
            return "TMP_CI_REGISTRO_UPLOADED_" + unid.ToString();
        }

        private string GetSqlInsertString(CiRegistroPOCO r,bool notIncludeUnion)
        {
            string sql = "select ";
            sql+=r.IdRegistro.ToString()+",";
            sql += r.IdPuntoMedicion.ToString() + ",";
            sql += "'" + String.Format("{0:yyyyMMdd HH:mm:ss}", DateTime.ParseExact(r.FechaNumerica.ToString().Substring(0, 8), "yyyyMMdd", CultureInfo.InvariantCulture)) + "',";
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

        public void BulkUpsertUploaded(List<CiRegistroPOCO> registros,long unidSession)
        {
            List<Thread> workerThreads = new List<Thread>();

            string _base = "SET DATEFORMAT YMD; Insert into " + this.GetTmpUploadedTable(unidSession) + " ";
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

                        sqlStatement = _base;
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

        public List<CiRegistroUploadConfirmationModel> CommitBulkUpsertUploaded(long session)
        {
            List<CiRegistroUploadConfirmationModel> confirmation = new List<CiRegistroUploadConfirmationModel>();
            try
            {
                using (var entity = new db_SeguimientoProtocolo_r2Entities())
                {
                    List<spCommitBulkUpsertCiRegistroUploaded_Result> res = entity.spCommitBulkUpsertCiRegistroUploaded(session).ToList();
                    if (res != null && res.Count > 0)
                    {
                        res.ForEach(o =>
                        {
                            confirmation.Add(new CiRegistroUploadConfirmationModel()
                            {
                                IdRegistro=Int64.Parse( o.FechaNumerica.ToString()+o.IdPuntoMedicion.ToString() ),
                                IdPuntoMedicion = o.IdPuntoMedicion,
                                FechaNumerica = (long)o.FechaNumerica,
                                LMD = o.LastModifiedDate,
                                SLMD = (long)o.ServerLastModifiedDate
                            });
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return confirmation;
        }

        /// <summary>
        /// Metodo principal para proceso de insercion de información recibida del cliente
        /// </summary>
        public List<CiRegistroUploadConfirmationModel> UpsertUploaded(List<CiRegistroPOCO> registros)
        {
            List<CiRegistroUploadConfirmationModel> confirmation=null;
            if (registros != null && registros.Count > 0)
            {
                long session = this.PrepareBulkUpsertUploaded();
                this.BulkUpsertUploaded(registros, session);
                confirmation=this.CommitBulkUpsertUploaded(session);
            }
            else
            {
                confirmation = null;
            }//endIfElse

            return confirmation;
        }
    }
}
