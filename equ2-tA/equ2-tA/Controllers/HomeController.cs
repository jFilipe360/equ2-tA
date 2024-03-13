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
        /// Este m�todo � a "porta de entrada" no programa
        /// </summary>
        /// <param name="A">Coeficiente do par�metro x^2</param>
        /// <param name="B">Coeficiente do par�metro x</param>
        /// <param name="C">Coeficiente do par�metro independente</param>
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
             * 1� - Determinar se os par�metros fornecidos s�o n�meros
             *  Se sim:
             *      2� - Determinar se A =/= 0 (A<>0) (A!=0)
             *          Se sim:
             *              3� - Calcular as ra�zes:
             *              x1,x2 = (-b +-sqrt(b^2-4*a*c))/(2*a)
             *                  4� - Calcular as ra�zes reais
             *                  5� - Calcular as ra�zes complexas, se existirem
             *                  6� - Enviar resposta para o utilizador
             *          Se n�o:
             *              3� - Enviar mensagem de erro para o utilizador 
             *  Se n�o:
             *      2� - Enviar mensagem de erro ao utilizador
             * 
             */

            //------Declara��o de vari�veis--------------

            //delta ser� o radicando da equa��o
            double delta;

            //vari�veis auxiliares para guardar o valor do c�lculo
            string x1, x2;
            //-------------------------------------------


            //verificar se A, B e C s�o valores num�ricos
            if (double.TryParse(A, out double dA))
            {
                if (double.TryParse(B, out double dB))
                {
                    if (double.TryParse(C, out double dC))
                    {
                        //verificar se A � diferente de 0
                        if(dA != 0)
                        {
                            //b^2-4*a*c
                            delta = Math.Pow(dB, 2) - (4 * dA * dC);

                            /* 
                             * Verifica se o delta � positivo ou nulo.
                             * Se n�o for, as ra�zes ser�o n�meros complexos.
                             */
                            if(delta >= 0)
                            {
                                //c�lculo da raiz, (-b +-delta)/(2*a)
                                x1 = Math.Round((-dB + Math.Sqrt(delta)) / (2 * dA),3) + "";
                                x2 = Math.Round((-dB - Math.Sqrt(delta)) / (2 * dA),3) + "";

                                //guardar o resultado numa viewBag para enviar os resultados para a view 
                                ViewBag.Res1 = x1;
                                ViewBag.Res2 = x2;

                                return View();
                            }
                            else
                            {
                                //c�lculo da raiz para n�meros complexos
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
                        //mensagem de erro para C != valor num�rico
                        return BadRequest("C n�o � um n�mero!");
                    }
                }
                else
                {
                    //mensagem de erro para B != valor num�rico
                    return BadRequest("B n�o � um n�mero!");
                }
            }
            else
            {
                //mensagem de erro para A != valor num�rico
                return BadRequest("A n�o � um n�mero!");
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
