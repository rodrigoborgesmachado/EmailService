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

        private int codigo;
        /// <summary>
        /// [CODIGO] Código do email
        /// <summary>
        public int Codigo
        {
            get
            {
                return this.codigo;
            }
            set
            {
                this.codigo = value;
            }
        }

        private string destinatario;
        /// <summary>
        /// [DESTINATARIO] Destinatário do Email
        /// <summary>
        public string Destinatario
        {
            get
            {
                return this.destinatario;
            }
            set
            {
                this.destinatario = value;
            }
        }

        private string assunto;
        /// <summary>
        /// [ASSUNTO] Assunto do Email
        /// <summary>
        public string Assunto
        {
            get
            {
                return this.assunto;
            }
            set
            {
                this.assunto = value;
            }
        }

        private string texto;
        /// <summary>
        /// [TEXTO] Corpo do Email
        /// <summary>
        public string Texto
        {
            get
            {
                return this.texto;
            }
            set
            {
                this.texto = value;
            }
        }

        private DateTime dataenvio = DateTime.Now;
        /// <summary>
        /// [DATAENVIO] Data de Envio do Email
        /// <summary>
        public DateTime Dataenvio
        {
            get
            {
                return this.dataenvio;
            }
            set
            {
                this.dataenvio = value;
            }
        }

        private string status;
        /// <summary>
        /// [STATUS] Status do envio (0 - Não enviado; 1 - Enviado; 2 - Erro)
        /// <summary>
        public string Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }

        private string observacao;
        /// <summary>
        /// [OBSERVACAO] Campo de observação do email
        /// <summary>
        public string Observacao
        {
            get
            {
                return this.observacao;
            }
            set
            {
                this.observacao = value;
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
            this.table.Fields_Table.Add(new MDN_Campo("CODIGO", false, Util.Enumerator.DataType.INT, 0, true, false, 0, 0));
            this.table.Fields_Table.Add(new MDN_Campo("DESTINATARIO", true, Util.Enumerator.DataType.STRING, "", false, false, 300, 0));
            this.table.Fields_Table.Add(new MDN_Campo("ASSUNTO", true, Util.Enumerator.DataType.STRING, "", false, false, 300, 0));
            this.table.Fields_Table.Add(new MDN_Campo("TEXTO", true, Util.Enumerator.DataType.STRING, "", false, false, 8000, 0));
            this.table.Fields_Table.Add(new MDN_Campo("DATAENVIO", true, Util.Enumerator.DataType.DATE, DateTime.Now, false, false, 0, 0));
            this.table.Fields_Table.Add(new MDN_Campo("STATUS", true, Util.Enumerator.DataType.CHAR, "0", false, false, 1, 0));
            this.table.Fields_Table.Add(new MDN_Campo("OBSERVACAO", false, Util.Enumerator.DataType.STRING, "", false, false, 1000, 0));
        }

		/// <summary>
        /// Construtor Secundário da classe
        /// </summary>
        /// <param name="CODIGO">Código do email
        public MD_Email(int codigo)
            :this()
        {
            this.codigo = codigo;
            this.Load();
        }


		#endregion Construtores

        #region Métodos

		/// <summary>
        /// Método que faz o load da classe
        /// </summary>
        public override void Load()
        {

            string sentenca = base.table.CreateCommandSQLTable() + " WHERE CODIGO = " + Codigo + "";
            DbDataReader reader = DataBase.Connection.Select(sentenca);

            if (reader == null)
            {
                this.Empty = true;
            }
            else if (reader.Read())
            {
                this.Destinatario = reader["DESTINATARIO"].ToString();
                this.Assunto = reader["ASSUNTO"].ToString();
                this.Texto = reader["TEXTO"].ToString();
                this.Dataenvio = DateTime.Parse(reader["DATAENVIO"].ToString());
                this.Status = reader["STATUS"].ToString();
                this.Observacao = reader["OBSERVACAO"].ToString();

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
            string sentenca = "DELETE FROM " + this.table.Table_Name + " WHERE CODIGO = " + Codigo + "";
            return DataBase.Connection.Delete(sentenca);
        }

        /// <summary>
        /// Método que faz o insert da classe
        /// </summary>
        /// <returns></returns>
        public override bool Insert()
        {
            string sentenca = string.Empty;

            sentenca = "INSERT INTO " + table.Table_Name + " (" + table.TodosCampos() + ")" + 
                              " VALUES (" + this.codigo + ",  '" + this.destinatario + "',  '" + this.assunto + "',  '" + this.texto + "',  '" + this.MontaStringDateTimeFromDateTime(this.dataenvio) + "',  '" + this.status + "',  '" + this.observacao + "')";
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
            string sentenca = string.Empty;

            sentenca = "UPDATE " + table.Table_Name + " SET " + 
                        "CODIGO = " + Codigo + ", DESTINATARIO = '" + Destinatario + "', ASSUNTO = '" + Assunto + "', TEXTO = '" + Texto + "', DATAENVIO = '" + this.MontaStringDateTimeFromDateTime(Dataenvio) + "', STATUS = '" + Status + "', OBSERVACAO = '" + Observacao + "'" + 
                        " WHERE CODIGO = " + Codigo + "";

            return DataBase.Connection.Update(sentenca);
        }

		#endregion Métodos
    }
}
