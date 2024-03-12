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
            double dA, dB, dC;
            double x1, x2;

            if (double.TryParse(A, out dA))
            {
                if (double.TryParse(B, out dB))
                {
                    if (double.TryParse(C, out dC))
                    {
                        if(dA != 0)
                        {
                            x1 = (-dB + Math.Sqrt(Math.Pow(dB, 2) - (4 * dA * dC))) / (2 * dA);
                            x2 = (-dB - Math.Sqrt(Math.Pow(dB, 2) - (4 * dA * dC))) / (2 * dA);

                            ViewBag.X1 = x1;
                            ViewBag.X2 = x2;

                            return View("Resultado");
                        }
                        else
                        {
                            return BadRequest("A tem de ser diferente de 0!");
                        }
                    }
                    else
                    {
                        return BadRequest("C n�o � um n�mero!");
                    }
                }
                else
                {
                    return BadRequest("B n�o � um n�mero!");
                }
            }
            else
            {
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
