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
    /// [USUARIOCONCURSANDO] Tabela USUARIOCONCURSANDO
    /// </summary>
    public class MD_Usuarioconcursando : MDN_Model
    {
        #region Atributos e Propriedades

        private int codigo;
        /// <summary>
        /// [CODIGO] Código do usuário
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

        private string email;
        /// <summary>
        /// [EMAIL] Email do usuário
        /// <summary>
        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                this.email = value;
            }
        }

        private string password;
        /// <summary>
        /// [PASSWORD] Senha do usuário para acessar o sistema
        /// <summary>
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }

        private DateTime datanascimento;
        /// <summary>
        /// [DATANASCIMENTO] Data de nascimento do usuário
        /// <summary>
        public DateTime Datanascimento
        {
            get
            {
                return this.datanascimento;
            }
            set
            {
                this.datanascimento = value;
            }
        }

        private string admin = "0";
        /// <summary>
        /// [ADMIN] Identifica se o usuário é administrador
        /// <summary>
        public string Admin
        {
            get
            {
                return this.admin;
            }
            set
            {
                this.admin = value;
            }
        }

        #endregion Atributos e Propriedades

        #region Construtores

        /// <summary>
        /// Construtor Principal da classe
        /// </summary>
        public MD_Usuarioconcursando()
            : base()
        {
            base.table = new MDN_Table("USUARIOCONCURSANDO");
            this.table.Fields_Table.Add(new MDN_Campo("CODIGO", true, Util.Enumerator.DataType.INT, 0, true, false, 0, 0));
            this.table.Fields_Table.Add(new MDN_Campo("NOME", false, Util.Enumerator.DataType.STRING, "", false, false, 200, 0));
            this.table.Fields_Table.Add(new MDN_Campo("EMAIL", true, Util.Enumerator.DataType.STRING, "", false, false, 300, 0));
            this.table.Fields_Table.Add(new MDN_Campo("PASSWORD", false, Util.Enumerator.DataType.STRING, "", false, false, 100, 0));
            this.table.Fields_Table.Add(new MDN_Campo("DATANASCIMENTO", true, Util.Enumerator.DataType.DATE, "", false, false, 0, 0));
            this.table.Fields_Table.Add(new MDN_Campo("ADMIN", false, Util.Enumerator.DataType.STRING, "0", false, false, 1, 0));
        }

        /// <summary>
        /// Construtor Secundário da classe
        /// </summary>
        /// <param name="codigo">Código do usuário
        public MD_Usuarioconcursando(int codigo)
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
            Util.CL_Files.WriteOnTheLog("MD_Usuarioconcursando.Load()", Util.Global.TipoLog.DETALHADO);

            string sentenca = base.table.CreateCommandSQLTable() + " WHERE CODIGO = " + Codigo + "";
            DbDataReader reader = DataBase.Connection.Select(sentenca);

            if (reader == null)
            {
                this.Empty = true;
            }
            else if (reader.Read())
            {
                this.Nome = reader["NOME"].ToString();
                this.Email = reader["EMAIL"].ToString();
                this.Password = reader["PASSWORD"].ToString();
                this.Datanascimento = DateTime.Parse(reader["DATANASCIMENTO"].ToString());
                this.Admin = reader["ADMIN"].ToString();

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
                              " VALUES (" + this.Codigo + ",  '" + this.nome + "',  '" + this.email + "',  '" + this.password + "',  '" + MontaStringDateFromDateTime(this.datanascimento) + "', '" + this.admin + "')";
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
                        " NOME = '" + Nome + "', EMAIL = '" + Email + "', PASSWORD = '" + Password + "', DATANASCIMENTO = '" + MontaStringDateFromDateTime(Datanascimento) + "', ADMIN = '" + admin + "'" + 
                        " WHERE CODIGO = " + Codigo + "";

            return DataBase.Connection.Update(sentenca);
        }

		#endregion Métodos
    }
}
