using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SimuladorDeceto1279.Pages
{
    public class SalaryCalculatorModel : PageModel
    {
        // Variables de entrada del simulador
        [BindProperty]
        public decimal ValorPunto { get; set; } = 20895.00M;

        [BindProperty]
        public string TipoPregrado { get; set; } = "Normal";

        [BindProperty]
        public bool TieneEspecializacion { get; set; }

        [BindProperty]
        public bool TieneMaestria { get; set; }

        [BindProperty]
        public bool? TieneDoctorado { get; set; }

        [BindProperty]
        public string Categoria { get; set; } = "Auxiliar";

        [BindProperty]
        public int A�osExperiencia { get; set; } = 0;

        [BindProperty]
        public int A�osExperienciaInvestigacion { get; set; } = 0;

        [BindProperty]
        public int A�osExperienciaDocente { get; set; } = 0;

        [BindProperty]
        public int A�osExperienciaDireccion { get; set; } = 0;

        [BindProperty]
        public int A�osExperienciaProfesional { get; set; } = 0;

        [BindProperty]
        public int ProductividadAcademica { get; set; } = 0;

        [BindProperty]
        public string TieneCargo { get; set; } = "Ninguno";

        [BindProperty]
        public int A�osCargo { get; set; } = 0;

        [BindProperty]
        public decimal Dedicacion { get; set; } = 1.0M;

        [BindProperty]
        public bool Desempe�oDestacado { get; set; }

        [BindProperty]
        public int A�osEspecializacion { get; set; } = 0;

        [BindProperty]
        public int CantidadMaestrias { get; set; } = 0;

        [BindProperty]
        public int CantidadDoctorados { get; set; } = 0;

        [BindProperty]
        public bool? DoctoradoAntesDe1998 { get; set; }

        [BindProperty]
        public int ArticulosA1 { get; set; } = 0;
        [BindProperty]
        public int ArticulosA2 { get; set; } = 0;
        [BindProperty]
        public int ArticulosB { get; set; } = 0;
        [BindProperty]
        public int ArticulosC { get; set; } = 0;

        [BindProperty]
        public List<int> AutoresArticulosA1 { get; set; } = new();
        [BindProperty]
        public List<int> AutoresArticulosA2 { get; set; } = new();
        [BindProperty]
        public List<int> AutoresArticulosB { get; set; } = new();
        [BindProperty]
        public List<int> AutoresArticulosC { get; set; } = new();

        [BindProperty]
        public int PonenciasInternacionales { get; set; } = 0;
        [BindProperty]
        public int PonenciasNacionales { get; set; } = 0;
        [BindProperty]
        public int PonenciasRegionales { get; set; } = 0;

        [BindProperty]
        public int VideosInternacionales { get; set; } = 0;
        [BindProperty]
        public int VideosNacionales { get; set; } = 0;

        [BindProperty]
        public int LibrosInvestigacion { get; set; } = 0;
        [BindProperty]
        public int LibrosTexto { get; set; } = 0;
        [BindProperty]
        public int LibrosEnsayo { get; set; } = 0;

        [BindProperty]
        public int Premios { get; set; } = 0;
        [BindProperty]
        public int Patentes { get; set; } = 0;
        [BindProperty]
        public int TraduccionesLibros { get; set; } = 0;

        [BindProperty]
        public int DireccionTesisMaestria { get; set; } = 0;
        [BindProperty]
        public int DireccionTesisDoctorado { get; set; } = 0;

        [BindProperty]
        public int InnovacionTecnologica { get; set; } = 0;
        [BindProperty]
        public int AdaptacionTecnologica { get; set; } = 0;

        // Variables de salida calculadas
        public int TotalPuntos { get; set; }
        public decimal SalarioMensual { get; set; }

        // Variables de salida calculadas
        public int PuntosTitulos { get; set; }
        public int PuntosCategoria { get; set; }
        public int PuntosExperiencia { get; set; }
        public int PuntosProductividad { get; set; }
        public int PuntosCargo { get; set; }
        public int PuntosDesempe�o { get; set; }
        public int PuntosExperienciaAnual { get; set; }

        public List<ProyeccionSalarial> ProyeccionSalarial { get; set; } = new List<ProyeccionSalarial>();
        public List<EscenarioComparativo> Escenarios { get; set; } = new List<EscenarioComparativo>();

        public void OnGet()
        {
            // Inicializar con valores por defecto
            CalcularSalario();
            GenerarProyeccion();
            GenerarEscenarios();
        }

        public void OnPost()
        {
            Console.WriteLine($"TieneDoctorado: {TieneDoctorado}");
            CalcularSalario();
            GenerarProyeccion();
            GenerarEscenarios();
        }

        private void CalcularSalario()
        {
            // Calcular puntos por t�tulos
            PuntosTitulos = CalcularPuntosTitulos();

            // Calcular puntos por categor�a
            PuntosCategoria = CalcularPuntosCategoria();

            // Calcular puntos por experiencia
            PuntosExperiencia = CalcularPuntosExperiencia();

            // Calcular puntos por productividad
            PuntosProductividad = CalcularPuntosProductividad();

            // Calcular puntos por cargo administrativo
            PuntosCargo = CalcularPuntosCargo();

            // Calcular puntos por desempe�o destacado
            PuntosDesempe�o = Desempe�oDestacado ? CalcularPuntosDesempe�o() : 0;

            // Calcular puntos por experiencia anual (2 puntos por a�o desde 2003)
            PuntosExperienciaAnual = A�osExperiencia * 2;

            // Calcular total de puntos
            TotalPuntos = PuntosTitulos + PuntosCategoria + PuntosExperiencia +
                          PuntosProductividad + PuntosCargo + PuntosDesempe�o + PuntosExperienciaAnual;

            // Calcular salario mensual
            SalarioMensual = TotalPuntos * ValorPunto;// * Dedicacion;
        }

        private int CalcularPuntosTitulos()
        {
            int puntos = 0;

            // Puntos por pregrado
            if (TipoPregrado == "Medicina Humana" || TipoPregrado == "Composici�n Musical")
                puntos += 183;
            else
                puntos += 178;

            // Puntos por t�tulos de posgrado
            int puntosPosgrado = 0;

            // **Especializaci�n**
            if (TieneEspecializacion)
            {
                if (A�osEspecializacion <= 2)
                {
                    puntosPosgrado += A�osEspecializacion * 10; // 10 puntos por a�o, hasta 20
                }
                else
                {
                    puntosPosgrado += 20 + ((A�osEspecializacion - 2) * 10); // 10 puntos por cada a�o adicional, hasta 30
                }
                puntosPosgrado = Math.Min(30, puntosPosgrado); // M�ximo 30 puntos por especializaci�n
            }

            // **Maestr�a**
            int puntosMaestria = 0;
            if (TieneMaestria)
            {
                if (CantidadMaestrias == 1)
                {
                    puntosMaestria = 40; // 40 puntos por una maestr�a
                }
                else if (CantidadMaestrias == 2)
                {
                    puntosMaestria = 60; // 40 puntos por la primera + 20 adicionales por la segunda (m�ximo 60)
                }
            }

            // **Doctorado**
            int puntosDoctorado = 0;

            if (TieneDoctorado == null)
            {
                TieneDoctorado = false;
            }

            if ((bool)TieneDoctorado)
            {
                if (!TieneMaestria)
                {
                    if (CantidadDoctorados == 1)
                    {
                        puntosDoctorado = 120; // Doctorado sin maestr�a
                    }
                    else if (CantidadDoctorados == 2)
                    {
                        puntosDoctorado = 140; // Doctorado sin maestr�a y tiene dos doctorados
                    }
                }
                else
                {
                    if (CantidadDoctorados == 1)
                    {
                        puntosDoctorado = 80; // Doctorado con maestr�a previa
                    }
                    else if (CantidadDoctorados == 2)
                    {
                        puntosDoctorado = 120; // 80 puntos por el primero + 40 adicionales por el segundo
                    }
                }


                if (DoctoradoAntesDe1998 == null)
                {
                    DoctoradoAntesDe1998 = false;
                }

                // **Aplicar beneficio si el doctorado es posterior a 1998**
                if ((bool)!DoctoradoAntesDe1998)
                {
                    puntosDoctorado += 20; // Beneficio adicional para doctorados despu�s de 1998
                }
            }

            // **Reglas de acumulaci�n**
            int puntosEspecializacionYMaestria = puntosMaestria + puntosPosgrado;
            puntosEspecializacionYMaestria = Math.Min(60, puntosEspecializacionYMaestria); // L�mite de 60 puntos para especializaci�n + maestr�a

            int totalPuntosPosgrado = puntosEspecializacionYMaestria + puntosDoctorado;
            totalPuntosPosgrado = Math.Min(140, totalPuntosPosgrado); // L�mite de 140 puntos acumulables

            return puntos + totalPuntosPosgrado;
        }

        private int CalcularPuntosCategoria()
        {
            switch (Categoria)
            {
                case "Auxiliar":
                    return 37;
                case "Asistente":
                    return 58;
                case "Asociado":
                    return 74;
                case "Titular":
                    return 96;
                case "Nacional":
                    return 44;
                default:
                    return 0;
            }
        }
        private int CalcularPuntosExperiencia()
        {
            int puntosExperiencia = (A�osExperienciaInvestigacion * 6) +
                                     (A�osExperienciaDocente * 4) +
                                     (A�osExperienciaDireccion * 4) +
                                     (A�osExperienciaProfesional * 3);

            // Aplicar topes seg�n categor�a
            int topeCategoria = Categoria switch
            {
                "Auxiliar" => 20,
                "Asistente" => 45,
                "Asociado" => 90,
                "Titular" => 120,
                _ => 0
            };

            return Math.Min(puntosExperiencia, topeCategoria);
        }

        private int CalcularPuntosCargo()
        {
            if (TieneCargo == "Ninguno")
                return 0;

            int puntosPorA�o;
            switch (TieneCargo)
            {
                case "Rector":
                    puntosPorA�o = 11;
                    break;
                case "Vicerrector":
                    puntosPorA�o = 9;
                    break;
                case "Decano":
                    puntosPorA�o = 6;
                    break;
                case "Vicedecano":
                    puntosPorA�o = 4;
                    break;
                case "Director de Departamento":
                    puntosPorA�o = 2;
                    break;
                default:
                    puntosPorA�o = 0;
                    break;
            }

            return puntosPorA�o * A�osCargo;
        }

        private int CalcularPuntosDesempe�o()
        {
            switch (Categoria)
            {
                case "Auxiliar":
                    return 2;
                case "Asistente":
                    return 3;
                case "Asociado":
                    return 4;
                case "Titular":
                    return 5;
                default:
                    return 0;
            }
        }

        private int CalcularPuntosProductividad()
        {
            int puntos = 0;
            puntos += CalcularPuntosPorArticulos(ArticulosA1, AutoresArticulosA1, 15);
            puntos += CalcularPuntosPorArticulos(ArticulosA2, AutoresArticulosA2, 12);
            puntos += CalcularPuntosPorArticulos(ArticulosB, AutoresArticulosB, 8);
            puntos += CalcularPuntosPorArticulos(ArticulosC, AutoresArticulosC, 3);

            puntos += (VideosInternacionales * 12) + (VideosNacionales * 7);
            puntos += (LibrosInvestigacion * 20) + (LibrosTexto * 15) + (LibrosEnsayo * 15);
            puntos += (Premios * 15) + (Patentes * 25) + (TraduccionesLibros * 15);
            puntos += (InnovacionTecnologica * 10) + (AdaptacionTecnologica * 8);

            puntos += (PonenciasInternacionales * 84) + (PonenciasNacionales * 48) + (PonenciasRegionales * 24);
            puntos += (DireccionTesisMaestria * 36) + (DireccionTesisDoctorado * 72);



            int topeCategoria = Categoria switch
            {
                "Auxiliar" => 80,
                "Asistente" => 160,
                "Asociado" => 320,
                "Titular" => 540,
                _ => 0
            };
            return Math.Min(puntos, topeCategoria);
        }

        private int CalcularPuntosPorArticulos(int cantidad, List<int> autores, int puntosBase)
        {
            int total = 0;
            for (int i = 0; i < cantidad; i++)
            {
                int numAutores = (i < autores.Count) ? autores[i] : 1;
                if (numAutores <= 3)
                    total += puntosBase;
                else if (numAutores <= 5)
                    total += (puntosBase / 2);
                else
                    total += (puntosBase / (numAutores / 2));
            }
            return total;
        }

        private void GenerarProyeccion()
        {
            ProyeccionSalarial = new List<ProyeccionSalarial>();
            decimal valorPuntoActualizado = ValorPunto;
            int puntosTotalesActualizados = TotalPuntos;

            for (int i = 0; i < 5; i++)
            {
                // Incremento anual en el valor del punto (6%)
                valorPuntoActualizado *= 1.06M;

                // Incremento en puntos (promedio de 3 puntos por a�o)
                puntosTotalesActualizados += 3;

                // Calcular el nuevo salario
                decimal salarioProyectado = puntosTotalesActualizados * valorPuntoActualizado;// * Dedicacion;

                ProyeccionSalarial.Add(new ProyeccionSalarial
                {
                    A�o = DateTime.Now.Year + i + 1,
                    ValorPunto = valorPuntoActualizado,
                    TotalPuntos = puntosTotalesActualizados,
                    SalarioMensual = salarioProyectado
                });
            }
        }

        private void GenerarEscenarios()
        {
            Escenarios = new List<EscenarioComparativo>
        {
            // Escenario 1: Profesor Auxiliar con maestr�a
            new EscenarioComparativo
            {
                Nombre = "Profesor Auxiliar",
                Descripcion = "Profesor reci�n ingresado con t�tulo de pregrado y maestr�a",
                TotalPuntos = 178 + 40 + 37, // Pregrado + Maestr�a + Categor�a Auxiliar
                SalarioMensual = (178 + 40 + 37) * ValorPunto
            },
            
            // Escenario 2: Profesor Asistente con experiencia media
            new EscenarioComparativo
            {
                Nombre = "Profesor Asistente",
                Descripcion = "Profesor con 5 a�os de experiencia, maestr�a y algunas publicaciones",
                TotalPuntos = 178 + 40 + 58 + 20 + 30 + 10, // Pregrado + Maestr�a + Categor�a + Experiencia + Productividad + Exp. Anual
                SalarioMensual = (178 + 40 + 58 + 20 + 30 + 10) * ValorPunto
            },
            
            // Escenario 3: Profesor Asociado con doctorado
            new EscenarioComparativo
            {
                Nombre = "Profesor Asociado",
                Descripcion = "Profesor con doctorado, 10 a�os de experiencia y productividad moderada",
                TotalPuntos = 178 + 120 + 74 + 45 + 60 + 20, // Pregrado + Doctorado + Categor�a + Experiencia + Productividad + Exp. Anual
                SalarioMensual = (178 + 120 + 74 + 45 + 60 + 20) * ValorPunto
            },
            
            // Escenario 4: Profesor Titular con alta productividad
            new EscenarioComparativo
            {
                Nombre = "Profesor Titular",
                Descripcion = "Profesor con doctorado, 20 a�os de experiencia y alta productividad",
                TotalPuntos = 178 + 120 + 96 + 120 + 120 + 5 + 40, // Pregrado + Doctorado + Categor�a + Experiencia + Productividad + Desempe�o + Exp. Anual
                SalarioMensual = (178 + 120 + 96 + 120 + 120 + 5 + 40) * ValorPunto
            },
            
            // Escenario 5: Profesor Titular con productividad extrema (caso m�ximo)
            new EscenarioComparativo
            {
                Nombre = "Caso M�ximo",
                Descripcion = "Profesor Titular con doctorado y productividad al l�mite (escenario extremo)",
                TotalPuntos = 178 + 120 + 96 + 120 + 540 + 5 + 40, // Pregrado + Doctorado + Categor�a + Experiencia + Productividad m�xima + Desempe�o + Exp. Anual
                SalarioMensual = (178 + 120 + 96 + 120 + 540 + 5 + 40) * ValorPunto
            }
        };
        }
    }

    public class ProyeccionSalarial
    {
        public int A�o { get; set; }
        public decimal ValorPunto { get; set; }
        public int TotalPuntos { get; set; }
        public decimal SalarioMensual { get; set; }
    }

    public class EscenarioComparativo
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int TotalPuntos { get; set; }
        public decimal SalarioMensual { get; set; }
    }
}
