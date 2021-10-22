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
    /// [ACAOUSUARIO] Tabela ACAOUSUARIO
    /// </summary>
    public class MD_Acaousuario : MDN_Model
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

        private int codigousuario;
        /// <summary>
        /// [CODIGOUSUARIO] 
        /// <summary>
        public int Codigousuario
        {
            get
            {
                return this.codigousuario;
            }
            set
            {
                this.codigousuario = value;
            }
        }

        private string acao;
        /// <summary>
        /// [ACAO] 
        /// <summary>
        public string Acao
        {
            get
            {
                return this.acao;
            }
            set
            {
                this.acao = value;
            }
        }

        private DateTime dataregistro;
        /// <summary>
        /// [DATAREGISTRO] 
        /// <summary>
        public DateTime Dataregistro
        {
            get
            {
                return this.dataregistro;
            }
            set
            {
                this.dataregistro = value;
            }
        }


		#endregion Atributos e Propriedades

        #region Construtores

		/// <summary>
        /// Construtor Principal da classe
        /// </summary>
        public MD_Acaousuario()
            : base()
        {
            base.table = new MDN_Table("ACAOUSUARIO");
            this.table.Fields_Table.Add(new MDN_Campo("CODIGO", false, Util.Enumerator.DataType.INT, 0, true, false, 0, 0));
            this.table.Fields_Table.Add(new MDN_Campo("CODIGOUSUARIO", true, Util.Enumerator.DataType.INT, 0, false, false, 0, 0));
            this.table.Fields_Table.Add(new MDN_Campo("ACAO", true, Util.Enumerator.DataType.STRING, "", false, false, 200, 0));
            this.table.Fields_Table.Add(new MDN_Campo("DATAREGISTRO", true, Util.Enumerator.DataType.DATE, "", false, false, 0, 0));
        }

		/// <summary>
        /// Construtor Secundário da classe
        /// </summary>
        /// <param name="CODIGO">
        public MD_Acaousuario(int codigo)
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
            Util.CL_Files.WriteOnTheLog("MD_Acaousuario.Load()", Util.Global.TipoLog.DETALHADO);

            string sentenca = base.table.CreateCommandSQLTable() + " WHERE CODIGO = " + Codigo + "";
            DbDataReader reader = DataBase.Connection.Select(sentenca);

            if (reader == null)
            {
                this.Empty = true;
            }
            else if (reader.Read())
            {
                this.Codigousuario = int.Parse(reader["CODIGOUSUARIO"].ToString());
                this.Acao = reader["ACAO"].ToString();
                this.Dataregistro = DateTime.Parse(reader["DATAREGISTRO"].ToString());

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
                              " VALUES (" + this.codigo + ", " + this.codigousuario + ",  '" + this.acao + "',  '" + this.MontaStringDateTimeFromDateTime(this.dataregistro) + "')";
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
                        " CODIGOUSUARIO = " + Codigousuario + ", ACAO = '" + Acao + "', DATAREGISTRO = '" + this.MontaStringDateTimeFromDateTime(this.dataregistro) + "'" + 
                        " WHERE CODIGO = " + Codigo + "";

            return DataBase.Connection.Update(sentenca);
        }

		#endregion Métodos
    }
}
