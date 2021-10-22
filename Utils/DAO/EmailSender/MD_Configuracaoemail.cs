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
    /// [CONFIGURACAOEMAIL] Tabela CONFIGURACAOEMAIL
    /// </summary>
    public class MD_Configuracaoemail : MDN_Model
    {
        #region Atributos e Propriedades

        private int codigo;
        /// <summary>
        /// [CODIGO] 
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

        private string emailremetente;
        /// <summary>
        /// [EMAILREMETENTE] Email remetente dos emails
        /// <summary>
        public string Emailremetente
        {
            get
            {
                return this.emailremetente;
            }
            set
            {
                this.emailremetente = value;
            }
        }

        private string smtpclient;
        /// <summary>
        /// [SMTPCLIENT] Smtp de envio
        /// <summary>
        public string Smtpclient
        {
            get
            {
                return this.smtpclient;
            }
            set
            {
                this.smtpclient = value;
            }
        }

        private int porta;
        /// <summary>
        /// [PORTA] Porta de conexão
        /// <summary>
        public int Porta
        {
            get
            {
                return this.porta;
            }
            set
            {
                this.porta = value;
            }
        }

        private string emailcredencial;
        /// <summary>
        /// [EMAILCREDENCIAL] Email de credencial
        /// <summary>
        public string Emailcredencial
        {
            get
            {
                return this.emailcredencial;
            }
            set
            {
                this.emailcredencial = value;
            }
        }

        private string senhacredencial;
        /// <summary>
        /// [SENHACREDENCIAL] Senha da credencial de conexão
        /// <summary>
        public string Senhacredencial
        {
            get
            {
                return this.senhacredencial;
            }
            set
            {
                this.senhacredencial = value;
            }
        }


		#endregion Atributos e Propriedades

        #region Construtores

		/// <summary>
        /// Construtor Principal da classe
        /// </summary>
        public MD_Configuracaoemail()
            : base()
        {
            base.table = new MDN_Table("CONFIGURACAOEMAIL");
            this.table.Fields_Table.Add(new MDN_Campo("CODIGO", false, Util.Enumerator.DataType.INT, 0, true, false, 0, 0));
            this.table.Fields_Table.Add(new MDN_Campo("EMAILREMETENTE", true, Util.Enumerator.DataType.STRING, "", false, false, 300, 0));
            this.table.Fields_Table.Add(new MDN_Campo("SMTPCLIENT", false, Util.Enumerator.DataType.STRING, "smtp.gmail.com", false, false, 100, 0));
            this.table.Fields_Table.Add(new MDN_Campo("PORTA", true, Util.Enumerator.DataType.INT, 587, false, false, 0, 0));
            this.table.Fields_Table.Add(new MDN_Campo("EMAILCREDENCIAL", true, Util.Enumerator.DataType.STRING, "", false, false, 300, 0));
            this.table.Fields_Table.Add(new MDN_Campo("SENHACREDENCIAL", true, Util.Enumerator.DataType.STRING, "", false, false, 200, 0));
        }

		/// <summary>
        /// Construtor Secundário da classe
        /// </summary>
        /// <param name="CODIGO">
        public MD_Configuracaoemail(int codigo)
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
            Util.CL_Files.WriteOnTheLog("MD_Configuracaoemail.Load()", Util.Global.TipoLog.DETALHADO);

            string sentenca = base.table.CreateCommandSQLTable() + " WHERE CODIGO = " + Codigo + "";
            DbDataReader reader = DataBase.Connection.Select(sentenca);

            if (reader == null)
            {
                this.Empty = true;
            }
            else if (reader.Read())
            {
                this.Emailremetente = reader["EMAILREMETENTE"].ToString();
                this.Smtpclient = reader["SMTPCLIENT"].ToString();
                this.Porta = int.Parse(reader["PORTA"].ToString());
                this.Emailcredencial = reader["EMAILCREDENCIAL"].ToString();
                this.Senhacredencial = reader["SENHACREDENCIAL"].ToString();

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
                              " VALUES (" + this.codigo + ",  '" + this.emailremetente + "',  '" + this.smtpclient + "', " + this.porta + ",  '" + this.emailcredencial + "',  '" + this.senhacredencial + "')";
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
                        "CODIGO = " + Codigo + ", EMAILREMETENTE = '" + Emailremetente + "', SMTPCLIENT = '" + Smtpclient + "', PORTA = " + Porta + ", EMAILCREDENCIAL = '" + Emailcredencial + "', SENHACREDENCIAL = '" + Senhacredencial + "'" + 
                        " WHERE CODIGO = " + Codigo + "";

            return DataBase.Connection.Update(sentenca);
        }

		#endregion Métodos
    }
}
