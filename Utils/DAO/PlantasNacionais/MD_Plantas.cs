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
    /// [PLANTAS] Tabela PLANTAS
    /// </summary>
    public class MD_Plantas : MDN_Model
    {
        #region Atributos e Propriedades

        private int id;
        /// <summary>
        /// [ID] ID da planta.
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

        private int idbioma;
        /// <summary>
        /// [IDBIOMA] ID do bioma ao qual a planta faz parte.
        /// <summary>
        public int Idbioma
        {
            get
            {
                return this.idbioma;
            }
            set
            {
                this.idbioma = value;
            }
        }

        private string nomecientifico;
        /// <summary>
        /// [NOMECIENTIFICO] Nome científico da panta.
        /// <summary>
        public string Nomecientifico
        {
            get
            {
                return this.nomecientifico;
            }
            set
            {
                this.nomecientifico = value;
            }
        }

        private string nomepopular;
        /// <summary>
        /// [NOMEPOPULAR] Nome popular da planta
        /// <summary>
        public string Nomepopular
        {
            get
            {
                return this.nomepopular;
            }
            set
            {
                this.nomepopular = value;
            }
        }

        private string habitate;
        /// <summary>
        /// [HABITATE] Habitade da planta
        /// <summary>
        public string Habitate
        {
            get
            {
                return this.habitate;
            }
            set
            {
                this.habitate = value;
            }
        }

        private string folha;
        /// <summary>
        /// [FOLHA] Tipo da folha.
        /// <summary>
        public string Folha
        {
            get
            {
                return this.folha;
            }
            set
            {
                this.folha = value;
            }
        }

        private string flor;
        /// <summary>
        /// [FLOR] Tipo e descrição da flor
        /// <summary>
        public string Flor
        {
            get
            {
                return this.flor;
            }
            set
            {
                this.flor = value;
            }
        }

        private string fruto;
        /// <summary>
        /// [FRUTO] Descrição do fruto
        /// <summary>
        public string Fruto
        {
            get
            {
                return this.fruto;
            }
            set
            {
                this.fruto = value;
            }
        }

        private string familia;
        /// <summary>
        /// [FAMILIA] Família da planta.
        /// <summary>
        public string Familia
        {
            get
            {
                return this.familia;
            }
            set
            {
                this.familia = value;
            }
        }

        private string tribo;
        /// <summary>
        /// [TRIBO] Tribo da planta
        /// <summary>
        public string Tribo
        {
            get
            {
                return this.tribo;
            }
            set
            {
                this.tribo = value;
            }
        }

        private int idusuario;
        /// <summary>
        /// [IDUSUARIO] Id que identifica o usuário que cadastrou a planta
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
        public MD_Plantas()
            : base()
        {
            base.table = new MDN_Table("PLANTAS");
            this.table.Fields_Table.Add(new MDN_Campo("ID", false, Util.Enumerator.DataType.INT, 0, true, false, 0, 0));
            this.table.Fields_Table.Add(new MDN_Campo("IDBIOMA", true, Util.Enumerator.DataType.INT, 0, false, false, 0, 0));
            this.table.Fields_Table.Add(new MDN_Campo("NOMECIENTIFICO", true, Util.Enumerator.DataType.STRING, "", false, false, 200, 0));
            this.table.Fields_Table.Add(new MDN_Campo("NOMEPOPULAR", true, Util.Enumerator.DataType.STRING, "", false, false, 200, 0));
            this.table.Fields_Table.Add(new MDN_Campo("HABITATE", false, Util.Enumerator.DataType.STRING, "", false, false, 1000, 0));
            this.table.Fields_Table.Add(new MDN_Campo("FOLHA", false, Util.Enumerator.DataType.STRING, "", false, false, 8000, 0));
            this.table.Fields_Table.Add(new MDN_Campo("FLOR", false, Util.Enumerator.DataType.STRING, "", false, false, 8000, 0));
            this.table.Fields_Table.Add(new MDN_Campo("FRUTO", false, Util.Enumerator.DataType.STRING, "", false, false, 8000, 0));
            this.table.Fields_Table.Add(new MDN_Campo("FAMILIA", false, Util.Enumerator.DataType.STRING, "", false, false, 8000, 0));
            this.table.Fields_Table.Add(new MDN_Campo("TRIBO", false, Util.Enumerator.DataType.STRING, "", false, false, 200, 0));
            this.table.Fields_Table.Add(new MDN_Campo("IDUSUARIO", true, Util.Enumerator.DataType.INT, 0, false, false, 0, 0));

        }

		/// <summary>
        /// Construtor Secundário da classe
        /// </summary>
        /// <param name="ID">ID da planta.
        public MD_Plantas(int id)
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
            Util.CL_Files.WriteOnTheLog("MD_Plantas.Load()", Util.Global.TipoLog.DETALHADO);

            string sentenca = base.table.CreateCommandSQLTable() + " WHERE ID = " + Id + "";
            DbDataReader reader = DataBase.Connection.Select(sentenca);

            if (reader == null)
            {
                this.Empty = true;
            }
            else if (reader.Read())
            {
                this.Idbioma = int.Parse(reader["IDBIOMA"].ToString());
                this.Nomecientifico = reader["NOMECIENTIFICO"].ToString();
                this.Nomepopular = reader["NOMEPOPULAR"].ToString();
                this.Habitate = reader["HABITATE"].ToString();
                this.Folha = reader["FOLHA"].ToString();
                this.Flor = reader["FLOR"].ToString();
                this.Fruto = reader["FRUTO"].ToString();
                this.Familia = reader["FAMILIA"].ToString();
                this.Tribo = reader["TRIBO"].ToString();
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
                              " VALUES (" + this.id + ", " + this.idbioma + ",  '" + this.nomecientifico + "',  '" + this.nomepopular + "',  '" + this.habitate + "',  '" + this.folha + "',  '" + this.flor + "',  '" + this.fruto + "',  '" + this.familia + "',  '" + this.tribo + "', " + this.idusuario + ")";
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
                        "ID = " + Id + ", IDBIOMA = " + Idbioma + ", NOMECIENTIFICO = '" + Nomecientifico + "', NOMEPOPULAR = '" + Nomepopular + "', HABITATE = '" + Habitate + "', FOLHA = '" + Folha + "', FLOR = '" + Flor + "', FRUTO = '" + Fruto + "', FAMILIA = '" + Familia + "', TRIBO = '" + Tribo + "', IDUSUARIO = " + Idusuario + "" + 
                        " WHERE ID = " + Id + "";

            return DataBase.Connection.Update(sentenca);
        }

		#endregion Métodos
    }
}
