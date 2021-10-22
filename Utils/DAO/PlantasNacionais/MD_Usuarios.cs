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
    /// [USUARIOS] Tabela USUARIOS
    /// </summary>
    public class MD_Usuarios : MDN_Model
    {
        #region Atributos e Propriedades

        private int id;
        /// <summary>
        /// [ID] Id que representa o usuário
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

        private string login;
        /// <summary>
        /// [LOGIN] Login do usuário para acessar o sistema.
        /// <summary>
        public string Login
        {
            get
            {
                return this.login;
            }
            set
            {
                this.login = value;
            }
        }

        private string pass;
        /// <summary>
        /// [PASS] Senha do usuário
        /// <summary>
        public string Pass
        {
            get
            {
                return this.pass;
            }
            set
            {
                this.pass = value;
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


		#endregion Atributos e Propriedades

        #region Construtores

		/// <summary>
        /// Construtor Principal da classe
        /// </summary>
        public MD_Usuarios()
            : base()
        {
            base.table = new MDN_Table("USUARIOS");
            this.table.Fields_Table.Add(new MDN_Campo("ID", false, Util.Enumerator.DataType.INT, 0, true, false, 0, 0));
            this.table.Fields_Table.Add(new MDN_Campo("LOGIN", true, Util.Enumerator.DataType.CHAR, "", false, true, 50, 0));
            this.table.Fields_Table.Add(new MDN_Campo("PASS", true, Util.Enumerator.DataType.STRING, "", false, false, 12, 0));
            this.table.Fields_Table.Add(new MDN_Campo("NOME", true, Util.Enumerator.DataType.STRING, "", false, false, 50, 0));
        }

		/// <summary>
        /// Construtor Secundário da classe
        /// </summary>
        /// <param name="ID">Id que representa o usuário
        public MD_Usuarios(int id)
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
            Util.CL_Files.WriteOnTheLog("MD_Usuarios.Load()", Util.Global.TipoLog.DETALHADO);

            string sentenca = base.table.CreateCommandSQLTable() + " WHERE ID = " + Id + "";
            DbDataReader reader = DataBase.Connection.Select(sentenca);

            if (reader == null)
            {
                this.Empty = true;
            }
            else if (reader.Read())
            {
                this.Login = reader["LOGIN"].ToString();
                this.Pass = reader["PASS"].ToString();
                this.Nome = reader["NOME"].ToString();

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
                              " VALUES (" + this.id + ",  '" + this.login + "',  '" + this.pass + "',  '" + this.nome + "')";
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
                        "ID = " + Id + ", LOGIN = '" + Login + "', PASS = '" + Pass + "', NOME = '" + Nome + "'" + 
                        " WHERE ID = " + Id + "";

            return DataBase.Connection.Update(sentenca);
        }

		#endregion Métodos
    }
}
