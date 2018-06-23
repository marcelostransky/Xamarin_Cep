using App_Cep.Servico.Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;



namespace App_Cep.Servico
{
    public class ViaCepServiso
    {
        private static string Url = "http://viacep.com.br/ws/{0}/json/";

        public static Endereco BuscarEnderecoPorCep(string cep)
        {
            var NovaUrl = string.Format(Url, cep);
            var Wc = new WebClient();
            var Conteudo = Wc.DownloadString(NovaUrl);
            var EndercoReturn = JsonConvert.DeserializeObject<Endereco>(Conteudo);
            return EndercoReturn.Cep == null ? null : EndercoReturn;
        }
    }
}
