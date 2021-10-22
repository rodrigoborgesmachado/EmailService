using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// [EMAILANEXOS] Tabela da classe
    /// </summary>
    public class MD_Emailanexos 
    {
        #region Atributos e Propriedades

        /// <summary>
        /// DAO que representa a classe
        /// </summary>
        public DAO.MD_Emailanexos DAO = null;


        #endregion Atributos e Propriedades

        #region Construtores

        public MD_Emailanexos(int codigo)
        {
            Util.CL_Files.WriteOnTheLog("MD_Emailanexos()", Util.Global.TipoLog.DETALHADO);
            this.DAO = new DAO.MD_Emailanexos( codigo);
        }


        #endregion Construtores

        #region Métodos

        #endregion Métodos

    }
}
