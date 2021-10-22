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
    /// [BIOMAS] Tabela BIOMAS
    /// </summary>
    public class MD_Biomas : MDN_Model
    {
        #region Atributos e Propriedades

        private int id;
        /// <summary>
        /// [ID] Id do bioma
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
        /// [NOME] Nome do biome
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

        private string distribuicao;
        /// <summary>
        /// [DISTRIBUICAO] Distribuição do bioma
        /// <summary>
        public string Distribuicao
        {
            get
            {
                return this.distribuicao;
            }
            set
            {
                this.distribuicao = value;
            }
        }

        private string caracteristicas;
        /// <summary>
        /// [CARACTERISTICAS] Características do bioma.
        /// <summary>
        public string Caracteristicas
        {
            get
            {
                return this.caracteristicas;
            }
            set
            {
                this.caracteristicas = value;
            }
        }

        private string fitofisionomia;
        /// <summary>
        /// [FITOFISIONOMIA] Fitofisionomia do bioma.
        /// <summary>
        public string Fitofisionomia
        {
            get
            {
                return this.fitofisionomia;
            }
            set
            {
                this.fitofisionomia = value;
            }
        }

        private string observacao;
        /// <summary>
        /// [OBSERVACAO] Observação do bioma.
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

        private int idusuario;
        /// <summary>
        /// [IDUSUARIO] Id do usuário que cadastrou o bioma.
        /// <summary>
        public int Idusuario
        {
            get
            {
                return this.idusuario;
            }
            set
            {
                this.idusuario = value;
            }
        }


		#endregion Atributos e Propriedades

        #region Construtores

		/// <summary>
        /// Construtor Principal da classe
        /// </summary>
        public MD_Biomas()
            : base()
        {
            base.table = new MDN_Table("BIOMAS");
            this.table.Fields_Table.Add(new MDN_Campo("ID", false, Util.Enumerator.DataType.INT, 0, true, false, 0, 0));
            this.table.Fields_Table.Add(new MDN_Campo("NOME", true, Util.Enumerator.DataType.STRING, "", false, false, 200, 0));
            this.table.Fields_Table.Add(new MDN_Campo("DISTRIBUICAO", true, Util.Enumerator.DataType.STRING, "", false, false, 200, 0));
            this.table.Fields_Table.Add(new MDN_Campo("CARACTERISTICAS", true, Util.Enumerator.DataType.STRING, "", false, false, 8000, 0));
            this.table.Fields_Table.Add(new MDN_Campo("FITOFISIONOMIA", true, Util.Enumerator.DataType.STRING, "", false, false, 8000, 0));
            this.table.Fields_Table.Add(new MDN_Campo("OBSERVACAO", true, Util.Enumerator.DataType.STRING, "", false, false, 8000, 0));
            this.table.Fields_Table.Add(new MDN_Campo("IDUSUARIO", true, Util.Enumerator.DataType.INT, 0, false, false, 0, 0));
        }

		/// <summary>
        /// Construtor Secundário da classe
        /// </summary>
        /// <param name="ID">Id do bioma
        public MD_Biomas(int id)
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
            Util.CL_Files.WriteOnTheLog("MD_Biomas.Load()", Util.Global.TipoLog.DETALHADO);

            string sentenca = base.table.CreateCommandSQLTable() + " WHERE ID = " + Id + "";
            DbDataReader reader = DataBase.Connection.Select(sentenca);

            if (reader == null)
            {
                this.Empty = true;
            }
            else if (reader.Read())
            {
                this.Nome = reader["NOME"].ToString();
                this.Distribuicao = reader["DISTRIBUICAO"].ToString();
                this.Caracteristicas = reader["CARACTERISTICAS"].ToString();
                this.Fitofisionomia = reader["FITOFISIONOMIA"].ToString();
                this.Observacao = reader["OBSERVACAO"].ToString();
                this.Idusuario = int.Parse(reader["IDUSUARIO"].ToString());

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
                              " VALUES ( " + this.id + ", '" + this.nome + "',  '" + this.distribuicao + "',  '" + this.caracteristicas + "',  '" + this.fitofisionomia + "',  '" + this.observacao + "', " + this.idusuario + ")";
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
                        "ID = " + Id + ", NOME = '" + Nome + "', DISTRIBUICAO = '" + Distribuicao + "', CARACTERISTICAS = '" + Caracteristicas + "', FITOFISIONOMIA = '" + Fitofisionomia + "', OBSERVACAO = '" + Observacao + "', IDUSUARIO = " + Idusuario + "" + 
                        " WHERE ID = " + Id + "";

            return DataBase.Connection.Update(sentenca);
        }

		#endregion Métodos
    }
}
