using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// [EMAIL] Tabela da classe
    /// </summary>
    public class MD_Email 
    {
        #region Atributos e Propriedades

        /// <summary>
        /// DAO que representa a classe
        /// </summary>
        public DAO.MD_Email DAO = null;


        #endregion Atributos e Propriedades

        #region Construtores

        public MD_Email(int codigo)
        {
            this.DAO = new DAO.MD_Email( codigo);
        }


        #endregion Construtores

        #region Métodos

        /// <summary>
        /// Método que envia email
        /// </summary>
        /// <param name="configuracao">Configuração com os dados de envio e de servidor de email</param>
        /// <param name="dados">Dados do email para ser enviado</param>
        /// <returns>True = Sucesso; False = Erro</returns>
        public static bool Enviar(MD_Configuracaoemail configuracao, MD_Email dados, bool isHtml, System.Diagnostics.EventLog eventLog)
        {
            eventLog.WriteEntry("Enviar");
            bool retorno = true;

            try
            {
                MailMessage mail = new MailMessage();

                mail.From = new MailAddress(configuracao.DAO.Emailremetente);
                mail.To.Add(dados.DAO.Destinatario);
                mail.Subject = dados.DAO.Assunto;
                mail.Body = dados.DAO.Texto;
                mail.IsBodyHtml = isHtml;

                using (var smtp = new SmtpClient())
                {
                    smtp.Host = configuracao.DAO.Smtpclient;
                    smtp.EnableSsl = true; // GMail requer SSL
                    smtp.Port = configuracao.DAO.Porta;       // porta para SSL
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network; // modo de envio
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(configuracao.DAO.Emailcredencial, configuracao.DAO.Senhacredencial);

                    smtp.Send(mail);
                    smtp.Dispose();
                    dados.DAO.Status = "1";
                }
            }
            catch(SmtpException e)
            {
                Util.CL_Files.WriteOnTheLog("Error: " + e.Message, Util.Global.TipoLog.SIMPLES);
                dados.DAO.Status = "2";
                dados.DAO.Observacao += "| Erro ao enviar. Erro: " + e.Message;
                eventLog.WriteEntry("Erro: " + e.Message);
                retorno = false;
            }
            catch (Exception e)
            {
                Util.CL_Files.WriteOnTheLog("Error: " + e.Message, Util.Global.TipoLog.SIMPLES);
                dados.DAO.Status = "2";
                dados.DAO.Observacao += "| Erro ao enviar. Erro: " + e.Message;
                eventLog.WriteEntry("Erro: " + e.Message);
                retorno = false;
            }

            dados.DAO.Update();
            return retorno;
        }

        public static void ReenviaEmailsComFalha(Model.MD_Configuracaoemail conf, System.Diagnostics.EventLog eventLog)
        {
            eventLog.WriteEntry("ReenviaEmailsComFalha");
            string sentenca = "SELECT CODIGO FROM EMAIL WHERE STATUS IN ('0', '2')";
            eventLog.WriteEntry(sentenca);

            DbDataReader reader = DataBase.Connection.Select(sentenca);
            List<int> codigos = new List<int>();
            while (reader.Read())
            {
                int codigo = int.Parse(reader["CODIGO"].ToString());
                codigos.Add(codigo);
                eventLog.WriteEntry("Pegou o email " + codigo.ToString());
            }
            reader.Close();

            foreach(int codigo in codigos)
            {
                MD_Email email = new MD_Email(codigo);

                if (string.IsNullOrEmpty(email.DAO.Destinatario))
                {
                    eventLog.WriteEntry("Destinatário vazio");
                    email.DAO.Observacao += "Destinatario vazio";
                    email.DAO.Update();
                    continue;
                }

                Enviar(conf, email, true, eventLog);
                eventLog.WriteEntry("enviou");
            }
        }

        #endregion Métodos

    }
}
