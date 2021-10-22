using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// [CONFIGURACAOEMAIL] Tabela da classe
    /// </summary>
    public class MD_Configuracaoemail 
    {
        #region Atributos e Propriedades

        /// <summary>
        /// DAO que representa a classe
        /// </summary>
        public DAO.MD_Configuracaoemail DAO = null;


        #endregion Atributos e Propriedades

        #region Construtores

        public MD_Configuracaoemail(int codigo)
        {
            Util.CL_Files.WriteOnTheLog("MD_Configuracaoemail()", Util.Global.TipoLog.DETALHADO);
            this.DAO = new DAO.MD_Configuracaoemail( codigo);
        }


        #endregion Construtores

        #region Métodos

        #endregion Métodos

    }
}
