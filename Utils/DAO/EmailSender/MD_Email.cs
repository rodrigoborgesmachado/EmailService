using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace DAO
{

    /// <summary>
    /// [EMAIL] Tabela EMAIL
    /// </summary>
    public class MD_Email : MDN_Model
    {
        #region Atributos e Propriedades

        private int _codigo;
        /// <summary>
        /// [CODIGO] 
        /// <summary>
        public int Codigo
        {
            get
            {
                return this._codigo;
            }
            set
            {
                this._codigo = value;
            }
        }

        private string _destinatario;
        /// <summary>
        /// [DESTINATARIO] 
        /// <summary>
        public string Destinatario
        {
            get
            {
                return this._destinatario;
            }
            set
            {
                this._destinatario = value;
            }
        }

        private string _assunto;
        /// <summary>
        /// [ASSUNTO] 
        /// <summary>
        public string Assunto
        {
            get
            {
                return this._assunto;
            }
            set
            {
                this._assunto = value;
            }
        }

        private string _texto;
        /// <summary>
        /// [TEXTO] 
        /// <summary>
        public string Texto
        {
            get
            {
                return this._texto;
            }
            set
            {
                this._texto = value;
            }
        }

        private DateTime _dataenvio;
        /// <summary>
        /// [DATAENVIO] 
        /// <summary>
        public DateTime Dataenvio
        {
            get
            {
                return this._dataenvio;
            }
            set
            {
                this._dataenvio = value;
            }
        }

        private string _status;
        /// <summary>
        /// [STATUS] 
        /// <summary>
        public string Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
            }
        }

        private string _observacao;
        /// <summary>
        /// [OBSERVACAO] 
        /// <summary>
        public string Observacao
        {
            get
            {
                return this._observacao;
            }
            set
            {
                this._observacao = value;
            }
        }


        #endregion Atributos e Propriedades

        #region Construtores

        /// <summary>
        /// Construtor Principal da classe
        /// </summary>
        public MD_Email()
            : base()
        {
            base.table = new MDN_Table("EMAIL");
            this.table.Fields_Table.Add(new MDN_Campo("CODIGO", false, Util.Enumerator.DataType.INT, null, true, false, 0, 10));
            this.table.Fields_Table.Add(new MDN_Campo("DESTINATARIO", false, Util.Enumerator.DataType.STRING, null, false, false, 300, 0));
            this.table.Fields_Table.Add(new MDN_Campo("ASSUNTO", false, Util.Enumerator.DataType.STRING, null, false, false, 300, 0));
            this.table.Fields_Table.Add(new MDN_Campo("TEXTO", false, Util.Enumerator.DataType.STRING, null, false, false, 8000, 0));
            this.table.Fields_Table.Add(new MDN_Campo("DATAENVIO", false, Util.Enumerator.DataType.DATE, null, false, false, 0, 0));
            this.table.Fields_Table.Add(new MDN_Campo("STATUS", false, Util.Enumerator.DataType.STRING, null, false, false, 1, 0));
            this.table.Fields_Table.Add(new MDN_Campo("OBSERVACAO", true, Util.Enumerator.DataType.STRING, "", false, false, 1000, 0));

        }

        /// <summary>
        /// Construtor Secundário da classe
        /// </summary>
        public MD_Email(int _codigo)
            : this()
        {
            this.Codigo = _codigo;
            this.Load();
        }


        #endregion Construtores

        #region Métodos

        /// <summary>
        /// Método que faz o load da classe
        /// </summary>
        public override void Load()
        {
            Util.CL_Files.WriteOnTheLog("MD_Email.Load()", Util.Global.TipoLog.DETALHADO);

            string sentenca = base.table.CreateCommandSQLTable() + $" WHERE CODIGO = {this.Codigo}";
            DbDataReader reader = DataBase.Connection.Select(sentenca);

            if (reader == null)
            {
                this.Empty = true;
            }
            else if (reader.Read())
            {
                this.Destinatario = reader["DESTINATARIO"].ToString();
                this.Assunto = reader["ASSUNTO"].ToString();
                DateTime.TryParse(reader["DATAENVIO"].ToString(), out this._dataenvio);
                this.Status = reader["STATUS"].ToString();
                this.Observacao = reader["OBSERVACAO"].ToString();

                byte[] textoByte = (byte[])reader["TEXTO"];
                this.Texto = Encoding.UTF8.GetString(textoByte);

                this.Empty = false;
                reader.Close();
            }
            else
            {
                this.Empty = true;
                reader.Close();
            }
        }

        /// <summary>
        /// Método que faz o delete da classe
        /// </summary>
        /// <returns>True - sucesso; False - erro</returns>
        public override bool Delete()
        {
            string sentenca = $"DELETE FROM {base.table.Table_Name} WHERE CODIGO = {this.Codigo}";

            return DataBase.Connection.Delete(sentenca);
        }

        /// <summary>
        /// Método que faz o insert da classe
        /// </summary>
        /// <returns></returns>
        public override bool Insert()
        {
            string sentenca = $"INSERT INTO {table.Table_Name} ({table.TodosCampos()})" +
                       " VALUES ( " +
                       (Codigo == int.MinValue ? "NULL" : $"'{this.Codigo}'") + ", " +
                       (string.IsNullOrEmpty(Destinatario) ? "NULL" : $"'{this.Destinatario}'") + ", " +
                       (string.IsNullOrEmpty(Assunto) ? "NULL" : $"'{this.Assunto}'") + ", " +
                       (string.IsNullOrEmpty(Texto) ? "NULL" : $"CAST('{this.Texto}' AS VARBINARY(MAX))") + ", " +
                       (Dataenvio == DateTime.MinValue ? "NULL" : "'" + this.MontaStringDateTimeFromDateTime(this.Dataenvio) + "'") + ", " +
                       (string.IsNullOrEmpty(Status) ? "NULL" : $"'{this.Status}'") + ", " +
                       $"'{this.Observacao}'" + ")";


            if (DataBase.Connection.Insert(sentenca))
            {
                Empty = false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Método que faz o update da classe
        /// </summary>
        /// <returns>True - sucesso; False - erro</returns>
        public override bool Update()
        {
            string sentenca = $"UPDATE {this.table.Table_Name} SET " +
                              $"DESTINATARIO = {(string.IsNullOrEmpty(this.Destinatario) ? "NULL" : $"'{this.Destinatario}'")}" + ", " +
                              $"DATAENVIO = {(this.Dataenvio == DateTime.MinValue ? "NULL" : $"'{this.MontaStringDateTimeFromDateTime(this.Dataenvio)}'")}" + ", " +
                              $"STATUS = {(string.IsNullOrEmpty(this.Status) ? "NULL" : $"'{this.Status}'")}" + ", " +
                              $"OBSERVACAO = '{this.Observacao}'" +
                              $" WHERE CODIGO = {this.Codigo}";

            return DataBase.Connection.Update(sentenca);
        }

        #endregion Métodos
    }
}
