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
    /// [RESULTADOS] Tabela RESULTADOS
    /// </summary>
    public class MD_Resultados : MDN_Model
    {
        #region Atributos e Propriedades

        private string nome;
        /// <summary>
        /// [NOME] Nome do jogador
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

        private string tempo;
        /// <summary>
        /// [TEMPO] Tempo para concluir o jogo
        /// <summary>
        public string Tempo
        {
            get
            {
                return this.tempo;
            }
            set
            {
                this.tempo = value;
            }
        }

        private string numeroacertos;
        /// <summary>
        /// [NUMEROACERTOS] Número de questões que foram acertadas
        /// <summary>
        public string Numeroacertos
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

        private string dataInsersao;
        /// <summary>
        /// [DATAINSERSAO] Data de inserção
        /// <summary>
        public string DataInsersao
        {
            get
            {
                return this.dataInsersao;
            }
            set
            {
                this.dataInsersao = value;
            }
        }


        #endregion Atributos e Propriedades

        #region Construtores

        /// <summary>
        /// Construtor Principal da classe
        /// </summary>
        public MD_Resultados()
            : base()
        {
            base.table = new MDN_Table("RESULTADOS");
            this.table.Fields_Table.Add(new MDN_Campo("NOME", true, Util.Enumerator.DataType.STRING, "", false, false, 100, 0));
            this.table.Fields_Table.Add(new MDN_Campo("TEMPO", true, Util.Enumerator.DataType.STRING, "", false, false, 20, 0));
            this.table.Fields_Table.Add(new MDN_Campo("NUMEROACERTOS", true, Util.Enumerator.DataType.STRING, "20", false, false, 10, 0));
            this.table.Fields_Table.Add(new MDN_Campo("DATAINSERCAO", false, Util.Enumerator.DataType.STRING, "", false, false, 10, 0));

            //if (!base.table.ExistsTable())
                //base.table.CreateTable(false);

            //base.table.VerificaColunas();
        }

		/// <summary>
        /// Construtor Secundário da classe
        /// </summary>
        public MD_Resultados(string nome)
            :this()
        {
            this.nome = nome;
            this.Load();
        }


		#endregion Construtores

        #region Métodos

		/// <summary>
        /// Método que faz o load da classe
        /// </summary>
        public override void Load()
        {
            Util.CL_Files.WriteOnTheLog("MD_Resultados.Load()", Util.Global.TipoLog.DETALHADO);

            string sentenca = base.table.CreateCommandSQLTable() + " WHERE NOME = '" + this.Nome + "'";
            DbDataReader reader = DataBase.Connection.Select(sentenca);

            if (reader == null)
            {
                this.Empty = true;
            }
            else if (reader.Read())
            {
                this.Nome = reader["NOME"].ToString();
                this.Tempo = reader["TEMPO"].ToString();
                this.Numeroacertos = reader["NUMEROACERTOS"].ToString();
                this.DataInsersao = reader["DATAINSERSAO"].ToString();

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
            throw new Exception("Não é permitido excluir!");
        }

        /// <summary>
        /// Método que faz o insert da classe
        /// </summary>
        /// <returns></returns>
        public override bool Insert()
        {
            string sentenca = string.Empty;

            sentenca = "INSERT INTO " + table.Table_Name + " (" + table.TodosCampos() + ")" + 
                              " VALUES ( '" + this.nome + "',  '" + this.tempo + "',  '" + this.numeroacertos + "', '" + this.dataInsersao + "')";
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
            throw new Exception("Não é possível atualizar dados dessa tabela");
        }

		#endregion Métodos
    }
}
