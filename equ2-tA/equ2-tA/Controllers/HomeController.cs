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
            double delta;
            string x1, x2;

            if (double.TryParse(A, out double dA))
            {
                if (double.TryParse(B, out double dB))
                {
                    if (double.TryParse(C, out double dC))
                    {
                        if(dA != 0)
                        {
                            delta = Math.Pow(dB, 2) - (4 * dA * dC);

                            if(delta >= 0)
                            {
                                x1 = (-dB + Math.Sqrt(delta)) / (2 * dA) + "";
                                x2 = (-dB - Math.Sqrt(delta)) / (2 * dA) + "";

                                ViewBag.Res1 = x1;
                                ViewBag.Res2 = x2;

                                return View();
                            }
                            else
                            {
                                x1 = Math.Round((-dB) / 2 / dA, 3) + " + " + Math.Round(Math.Sqrt(-delta) / 2 / dA, 3) + " i";
                                x2 = Math.Round((-dB) / 2 / dA, 3) + " - " + Math.Round(Math.Sqrt(-delta) / 2 / dA, 3) + " i";

                                ViewBag.Res1 = x1;
                                ViewBag.Res2 = x2;

                                return View();
                            }
                        }
                        else
                        {
                            return BadRequest("A tem de ser diferente de 0!");
                        }
                    }
                    else
                    {
                        return BadRequest("C não é um número!");
                    }
                }
                else
                {
                    return BadRequest("B não é um número!");
                }
            }
            else
            {
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
