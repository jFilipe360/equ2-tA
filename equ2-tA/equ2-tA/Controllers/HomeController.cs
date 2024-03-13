using equ2_tA.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace equ2_tA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Este método é a "porta de entrada" no programa
        /// </summary>
        /// <param name="A">Coeficiente do parâmetro x^2</param>
        /// <param name="B">Coeficiente do parâmetro x</param>
        /// <param name="C">Coeficiente do parâmetro independente</param>
        /// <returns></returns>
        /// 
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Resultado(string A, string B, string C)
        {
            /*
             * ALGORITMO
             * 1º - Determinar se os parâmetros fornecidos são números
             *  Se sim:
             *      2º - Determinar se A =/= 0 (A<>0) (A!=0)
             *          Se sim:
             *              3º - Calcular as raízes:
             *              x1,x2 = (-b +-sqrt(b^2-4*a*c))/(2*a)
             *                  4º - Calcular as raízes reais
             *                  5º - Calcular as raízes complexas, se existirem
             *                  6º - Enviar resposta para o utilizador
             *          Se não:
             *              3º - Enviar mensagem de erro para o utilizador 
             *  Se não:
             *      2º - Enviar mensagem de erro ao utilizador
             * 
             */

            //------Declaração de variáveis--------------

            //delta será o radicando da equação
            double delta;

            //variáveis auxiliares para guardar o valor do cálculo
            string x1, x2;
            //-------------------------------------------


            //verificar se A, B e C são valores numéricos
            if (double.TryParse(A, out double dA))
            {
                if (double.TryParse(B, out double dB))
                {
                    if (double.TryParse(C, out double dC))
                    {
                        //verificar se A é diferente de 0
                        if(dA != 0)
                        {
                            //b^2-4*a*c
                            delta = Math.Pow(dB, 2) - (4 * dA * dC);

                            /* 
                             * Verifica se o delta é positivo ou nulo.
                             * Se não for, as raízes serão números complexos.
                             */
                            if(delta >= 0)
                            {
                                //cálculo da raiz, (-b +-delta)/(2*a)
                                x1 = Math.Round((-dB + Math.Sqrt(delta)) / (2 * dA),3) + "";
                                x2 = Math.Round((-dB - Math.Sqrt(delta)) / (2 * dA),3) + "";

                                //guardar o resultado numa viewBag para enviar os resultados para a view 
                                ViewBag.Res1 = x1;
                                ViewBag.Res2 = x2;

                                return View();
                            }
                            else
                            {
                                //cálculo da raiz para números complexos
                                x1 = Math.Round((-dB) / 2 / dA, 3) + " + " + Math.Round(Math.Sqrt(-delta) / 2 / dA, 3) + " i";
                                x2 = Math.Round((-dB) / 2 / dA, 3) + " - " + Math.Round(Math.Sqrt(-delta) / 2 / dA, 3) + " i";

                                //guardar o resultado numa viewBag para enviar os resultados para a view 
                                ViewBag.Res1 = x1;
                                ViewBag.Res2 = x2;

                                return View();
                            }
                        }
                        else
                        {
                            //mensagem de erro para A!=0
                            return BadRequest("A tem de ser diferente de 0 (zero)!");
                        }
                    }
                    else
                    {
                        //mensagem de erro para C != valor numérico
                        return BadRequest("C não é um número!");
                    }
                }
                else
                {
                    //mensagem de erro para B != valor numérico
                    return BadRequest("B não é um número!");
                }
            }
            else
            {
                //mensagem de erro para A != valor numérico
                return BadRequest("A não é um número!");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
