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
    /// [PALAVRAS] Tabela PALAVRAS
    /// </summary>
    public class MD_Palavras : MDN_Model
    {
        #region Atributos e Propriedades

        private int id;
        /// <summary>
        /// [ID] ID da tabela
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

        private string palavra;
        /// <summary>
        /// [PALAVRA] Palavra
        /// <summary>
        public string Palavra
        {
            get
            {
                return this.palavra;
            }
            set
            {
                this.palavra = value;
            }
        }

        private int nivel;
        /// <summary>
        /// [NIVEL] Nivel de dificuldade da palavra
        /// <summary>
        public int Nivel
        {
            get
            {
                return this.nivel;
            }
            set
            {
                this.nivel = value;
            }
        }


		#endregion Atributos e Propriedades

        #region Construtores

		/// <summary>
        /// Construtor Principal da classe
        /// </summary>
        public MD_Palavras()
            : base()
        {
            base.table = new MDN_Table("PALAVRAS");
            this.table.Fields_Table.Add(new MDN_Campo("ID", false, Util.Enumerator.DataType.INT, 0, true, false, 0, 0));
            this.table.Fields_Table.Add(new MDN_Campo("PALAVRA", true, Util.Enumerator.DataType.STRING, "", false, true, 100, 0));
            this.table.Fields_Table.Add(new MDN_Campo("NIVEL", true, Util.Enumerator.DataType.INT, 0, false, false, 0, 0));
        }

		/// <summary>
        /// Construtor Secundário da classe
        /// </summary>
        /// <param name="ID">ID da tabela
        public MD_Palavras(int id)
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
            Util.CL_Files.WriteOnTheLog("MD_Palavras.Load()", Util.Global.TipoLog.DETALHADO);

            string sentenca = base.table.CreateCommandSQLTable() + " WHERE ID = " + Id + "";
            DbDataReader reader = DataBase.Connection.Select(sentenca);

            if (reader == null)
            {
                this.Empty = true;
            }
            else if (reader.Read())
            {
                this.Palavra = reader["PALAVRA"].ToString();
                this.Nivel = int.Parse(reader["NIVEL"].ToString());

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
                              " VALUES (" + this.id + ",  '" + this.palavra + "', " + this.nivel + ")";
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
                        "ID = " + Id + ", PALAVRA = '" + Palavra + "', NIVEL = " + Nivel + "" + 
                        " WHERE ID = " + Id + "";

            return DataBase.Connection.Update(sentenca);
        }

		#endregion Métodos
    }
}
