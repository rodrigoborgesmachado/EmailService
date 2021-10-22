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
    /// [PROVA] Tabela PROVA
    /// </summary>
    public class MD_Prova : MDN_Model
    {
        #region Atributos e Propriedades

        private int codigo;
        /// <summary>
        /// [CODIGO] Código
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

        private string nomeprova;
        /// <summary>
        /// [NOMEPROVA] Nome da prova
        /// <summary>
        public string Nomeprova
        {
            get
            {
                return this.nomeprova;
            }
            set
            {
                this.nomeprova = value;
            }
        }

        private string local;
        /// <summary>
        /// [LOCAL] Local onde a prova foi aplicada
        /// <summary>
        public string Local
        {
            get
            {
                return this.local;
            }
            set
            {
                this.local = value;
            }
        }

        private string tipoprova;
        /// <summary>
        /// [TIPOPROVA] Tipo da prova (tipo 1; azul)
        /// <summary>
        public string Tipoprova
        {
            get
            {
                return this.tipoprova;
            }
            set
            {
                this.tipoprova = value;
            }
        }

        private string dataaplicacao;
        /// <summary>
        /// [DATAAPLICACAO] Data de aplicação da prova (Ano e semestre)
        /// <summary>
        public string Dataaplicacao
        {
            get
            {
                return this.dataaplicacao;
            }
            set
            {
                this.dataaplicacao = value;
            }
        }

        private string prova;
        /// <summary>
        /// [PROVA] Arquivo da prova
        /// <summary>
        public string Prova
        {
            get
            {
                return this.prova;
            }
            set
            {
                this.prova = value;
            }
        }

        private string gabarito;
        /// <summary>
        /// [GABARITO] Arquivo de gabarito
        /// <summary>
        public string Gabarito
        {
            get
            {
                return this.gabarito;
            }
            set
            {
                this.gabarito = value;
            }
        }

        private string observacaoprova;
        /// <summary>
        /// [OBSERVACAOPROVA] Observacao sobre a prova
        /// <summary>
        public string Observacaoprova
        {
            get
            {
                return this.observacaoprova;
            }
            set
            {
                this.observacaoprova = value;
            }
        }

        private string observacaogabarito;
        /// <summary>
        /// [OBSERVACAOGABARITO] Observação sobre o gabarito
        /// <summary>
        public string Observacaogabarito
        {
            get
            {
                return this.observacaogabarito;
            }
            set
            {
                this.observacaogabarito = value;
            }
        }

        private DateTime dataRegistro = DateTime.Now;
        /// <summary>
        /// [DATAREGISTRO] Data de registro do objeto
        /// <summary>
        public DateTime DataRegistro
        {
            get
            {
                return this.dataRegistro;
            }
            set
            {
                this.dataRegistro = value;
            }
        }

        #endregion Atributos e Propriedades

        #region Construtores

        /// <summary>
        /// Construtor Principal da classe
        /// </summary>
        public MD_Prova()
            : base()
        {
            base.table = new MDN_Table("PROVA");
            this.table.Fields_Table.Add(new MDN_Campo("CODIGO", false, Util.Enumerator.DataType.INT, 0, true, false, 0, 0));
            this.table.Fields_Table.Add(new MDN_Campo("NOMEPROVA", true, Util.Enumerator.DataType.STRING, "", false, false, 500, 0));
            this.table.Fields_Table.Add(new MDN_Campo("LOCAL", true, Util.Enumerator.DataType.STRING, "", false, false, 500, 0));
            this.table.Fields_Table.Add(new MDN_Campo("TIPOPROVA", true, Util.Enumerator.DataType.STRING, "", false, false, 100, 0));
            this.table.Fields_Table.Add(new MDN_Campo("DATAAPLICACAO", true, Util.Enumerator.DataType.STRING, "", false, false, 10, 0));
            this.table.Fields_Table.Add(new MDN_Campo("PROVA", false, Util.Enumerator.DataType.STRING, "", false, false, 0, 0));
            this.table.Fields_Table.Add(new MDN_Campo("GABARITO", true, Util.Enumerator.DataType.STRING, "", false, false, 8000, 0));
            this.table.Fields_Table.Add(new MDN_Campo("OBSERVACAOPROVA", false, Util.Enumerator.DataType.STRING, "", false, false, 3000, 0));
            this.table.Fields_Table.Add(new MDN_Campo("OBSERVACAOGABARITO", true, Util.Enumerator.DataType.STRING, "", false, false, 2000, 0));
            this.table.Fields_Table.Add(new MDN_Campo("DATAREGISTRO", true, Util.Enumerator.DataType.DATE, DateTime.Now, false, false, 10, 0));
        }

        /// <summary>
        /// Construtor Secundário da classe
        /// </summary>
        /// <param name="CODIGO">Código
        public MD_Prova(int codigo)
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
            Util.CL_Files.WriteOnTheLog("MD_Prova.Load()", Util.Global.TipoLog.DETALHADO);

            string sentenca = base.table.CreateCommandSQLTable() + " WHERE CODIGO = " + Codigo + "";
            DbDataReader reader = DataBase.Connection.Select(sentenca);

            if (reader == null)
            {
                this.Empty = true;
            }
            else if (reader.Read())
            {
                this.Nomeprova = reader["NOMEPROVA"].ToString();
                this.Local = reader["LOCAL"].ToString();
                this.Tipoprova = reader["TIPOPROVA"].ToString();
                this.Dataaplicacao = reader["DATAAPLICACAO"].ToString();
                this.Prova = reader["PROVA"].ToString();
                this.Gabarito = reader["GABARITO"].ToString();
                this.Observacaoprova = reader["OBSERVACAOPROVA"].ToString();
                this.Observacaogabarito = reader["OBSERVACAOGABARITO"].ToString();
                this.DataRegistro = DateTime.Parse(reader["OBSERVACAOGABARITO"].ToString());

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
                              " VALUES (" + this.codigo + ",  '" + this.nomeprova + "',  '" + this.local + "',  '" + this.tipoprova + "',  '" + this.dataaplicacao + "',  CAST('" + this.prova + "' AS VARBINARY(MAX)),  CAST('" + this.gabarito + "' AS VARBINARY(MAX)),  '" + this.observacaoprova + "',  '" + this.observacaogabarito + "', '" + this.MontaStringDateTimeFromDateTime(this.DataRegistro) + "')";
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
                        " NOMEPROVA = '" + Nomeprova + "', LOCAL = '" + Local + "', TIPOPROVA = '" + Tipoprova + "', DATAAPLICACAO = '" + Dataaplicacao + "', OBSERVACAOPROVA = '" + Observacaoprova + "', OBSERVACAOGABARITO = '" + Observacaogabarito + "', DATAREGISTRO = '" + this.MontaStringDateTimeFromDateTime(this.DataRegistro) + "' " +
                        " WHERE CODIGO = " + Codigo + "";

            return DataBase.Connection.Update(sentenca);
        }

		#endregion Métodos
    }
}
