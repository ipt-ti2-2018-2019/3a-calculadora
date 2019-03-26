using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Calculadora.Controllers {
   public class HomeController : Controller {

      // GET: Home
      public ActionResult Index() {

         // inicializar o código da ViewBag
         ViewBag.Resposta = "0";
         // inicilizar as variáveis de sessão
         Session["primeiroOperando"] = "";
         Session["operador"] = "";
         Session["limpaVisor"] = true;

         return View();
      }

      // POST: Home
      [HttpPost]
      public ActionResult Index(string visor, string bt) {
         // vars. auxiliares
         string resposta = visor;

         // avaliar o valor do botão q foi carregado
         switch(bt) {
            case "1":
            case "2":
            case "3":
            case "4":
            case "5":
            case "6":
            case "7":
            case "8":
            case "9":
            case "0":
               // determinar se o VISOR tem apenas o 'zero'
               if(resposta == "0" || (bool)Session["limpaVisor"]) {   //  resposta.Equals("0")
                  resposta = bt;
                  Session["limpaVisor"] = false;
               }
               else {
                  resposta += bt; // resposta=resposta+bt
               }
               break;

            // processar a escolha de um operador
            case "+":
            case "-":
            case "x":
            case ":":
               if((string)Session["operador"] == "") {
                  // se entrei aqui, é pq é a 1ª vez que se escolheu um 'operador'
                  // guardar os dados da calculadora, para se poder executar mais tarde
                  Session["primeiroOperando"] = visor;
                  Session["operador"] = bt;
                  Session["limpaVisor"] = true;
               }
               else{
                  // agora já posso fazer a operação aritmética

                  // recuperar os dados anteriormente guardados
                  double operando1 = Convert.ToDouble(Session["primeiroOperando"]);
                  string operador = (string)Session["operador"];

                  // efetuar a operação aritmética
                  switch (operador){
                     case "+":
                        resposta = operando1 + Convert.ToDouble(visor) + "";
                        break;
                     case "-":
                        resposta = operando1 - Convert.ToDouble(visor) + "";
                        break;
                     case "x":
                        resposta = operando1 * Convert.ToDouble(visor) + "";
                        break;
                     case ":":
                        resposta = operando1 / Convert.ToDouble(visor) + "";
                        break;
                  }
                  // guardar os dados gerados para a nova operação
                  Session["primeiroOperando"] = resposta;
                  Session["operador"] = bt;
                  Session["limpaVisor"] = true;
               }
               break;
         } // switch

         // devolver a 'resposta' para o visor do ecrã
         ViewBag.Resposta = resposta;

         return View();
      }


   }
}