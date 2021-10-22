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
    /// [RESULTADOSSOLETRANDO] Tabela RESULTADOSSOLETRANDO
    /// </summary>
    public class MD_Resultadossoletrando : MDN_Model
    {
        #region Atributos e Propriedades

        private int id;
        /// <summary>
        /// [ID] ID do nome
        /// <summary>
        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        private string nome;
        /// <summary>
        /// [NOME] Nome do usuário
        /// <summary>
        public string Nome
        {
            get
            {
                return this.nome;
            }
            set
            {
                this.nome = value;
            }
        }

        private int numeroacertos;
        /// <summary>
        /// [NUMEROACERTOS] Número de questões acertadas
        /// <summary>
        public int Numeroacertos
        {
            get
            {
                return this.numeroacertos;
            }
            set
            {
                this.numeroacertos = value;
            }
        }


		#endregion Atributos e Propriedades

        #region Construtores

		/// <summary>
        /// Construtor Principal da classe
        /// </summary>
        public MD_Resultadossoletrando()
            : base()
        {
            base.table = new MDN_Table("RESULTADOSSOLETRANDO");
            this.table.Fields_Table.Add(new MDN_Campo("ID", false, Util.Enumerator.DataType.INT, 0, true, false, 0, 0));
            this.table.Fields_Table.Add(new MDN_Campo("NOME", true, Util.Enumerator.DataType.STRING, "", false, false, 200, 0));
            this.table.Fields_Table.Add(new MDN_Campo("NUMEROACERTOS", true, Util.Enumerator.DataType.INT, 0, false, false, 0, 0));
        }

		/// <summary>
        /// Construtor Secundário da classe
        /// </summary>
        /// <param name="ID">ID do nome
        public MD_Resultadossoletrando(int id)
            :this()
        {
            this.id = id;
            this.Load();
        }


		#endregion Construtores

        #region Métodos

		/// <summary>
        /// Método que faz o load da classe
        /// </summary>
        public override void Load()
        {
            Util.CL_Files.WriteOnTheLog("MD_Resultadossoletrando.Load()", Util.Global.TipoLog.DETALHADO);

            string sentenca = base.table.CreateCommandSQLTable() + " WHERE ID = " + Id + "";
            DbDataReader reader = DataBase.Connection.Select(sentenca);

            if (reader == null)
            {
                this.Empty = true;
            }
            else if (reader.Read())
            {
                this.Nome = reader["NOME"].ToString();
                this.Numeroacertos = int.Parse(reader["NUMEROACERTOS"].ToString());

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
            string sentenca = "DELETE FROM " + this.table.Table_Name + " WHERE ID = " + Id + "";
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
                              " VALUES (" + this.id + ",  '" + this.nome + "', " + this.numeroacertos + ")";
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
                        "ID = " + Id + ", NOME = '" + Nome + "', NUMEROACERTOS = " + Numeroacertos + "" + 
                        " WHERE ID = " + Id + "";

            return DataBase.Connection.Update(sentenca);
        }

		#endregion Métodos
    }
}
