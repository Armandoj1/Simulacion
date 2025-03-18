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
        public int AñosExperiencia { get; set; } = 0;

        [BindProperty]
        public int AñosExperienciaInvestigacion { get; set; } = 0;

        [BindProperty]
        public int AñosExperienciaDocente { get; set; } = 0;

        [BindProperty]
        public int AñosExperienciaDireccion { get; set; } = 0;

        [BindProperty]
        public int AñosExperienciaProfesional { get; set; } = 0;

        [BindProperty]
        public int ProductividadAcademica { get; set; } = 0;

        [BindProperty]
        public string TieneCargo { get; set; } = "Ninguno";

        [BindProperty]
        public int AñosCargo { get; set; } = 0;

        [BindProperty]
        public decimal Dedicacion { get; set; } = 1.0M;

        [BindProperty]
        public bool DesempeñoDestacado { get; set; }

        [BindProperty]
        public int AñosEspecializacion { get; set; } = 0;

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
        public int PuntosDesempeño { get; set; }
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
            // Calcular puntos por títulos
            PuntosTitulos = CalcularPuntosTitulos();

            // Calcular puntos por categoría
            PuntosCategoria = CalcularPuntosCategoria();

            // Calcular puntos por experiencia
            PuntosExperiencia = CalcularPuntosExperiencia();

            // Calcular puntos por productividad
            PuntosProductividad = CalcularPuntosProductividad();

            // Calcular puntos por cargo administrativo
            PuntosCargo = CalcularPuntosCargo();

            // Calcular puntos por desempeño destacado
            PuntosDesempeño = DesempeñoDestacado ? CalcularPuntosDesempeño() : 0;

            // Calcular puntos por experiencia anual (2 puntos por año desde 2003)
            PuntosExperienciaAnual = AñosExperiencia * 2;

            // Calcular total de puntos
            TotalPuntos = PuntosTitulos + PuntosCategoria + PuntosExperiencia +
                          PuntosProductividad + PuntosCargo + PuntosDesempeño + PuntosExperienciaAnual;

            // Calcular salario mensual
            SalarioMensual = TotalPuntos * ValorPunto;// * Dedicacion;
        }

        private int CalcularPuntosTitulos()
        {
            int puntos = 0;

            // Puntos por pregrado
            if (TipoPregrado == "Medicina Humana" || TipoPregrado == "Composición Musical")
                puntos += 183;
            else
                puntos += 178;

            // Puntos por títulos de posgrado
            int puntosPosgrado = 0;

            // **Especialización**
            if (TieneEspecializacion)
            {
                if (AñosEspecializacion <= 2)
                {
                    puntosPosgrado += AñosEspecializacion * 10; // 10 puntos por año, hasta 20
                }
                else
                {
                    puntosPosgrado += 20 + ((AñosEspecializacion - 2) * 10); // 10 puntos por cada año adicional, hasta 30
                }
                puntosPosgrado = Math.Min(30, puntosPosgrado); // Máximo 30 puntos por especialización
            }

            // **Maestría**
            int puntosMaestria = 0;
            if (TieneMaestria)
            {
                if (CantidadMaestrias == 1)
                {
                    puntosMaestria = 40; // 40 puntos por una maestría
                }
                else if (CantidadMaestrias == 2)
                {
                    puntosMaestria = 60; // 40 puntos por la primera + 20 adicionales por la segunda (máximo 60)
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
                        puntosDoctorado = 120; // Doctorado sin maestría
                    }
                    else if (CantidadDoctorados == 2)
                    {
                        puntosDoctorado = 140; // Doctorado sin maestría y tiene dos doctorados
                    }
                }
                else
                {
                    if (CantidadDoctorados == 1)
                    {
                        puntosDoctorado = 80; // Doctorado con maestría previa
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
                    puntosDoctorado += 20; // Beneficio adicional para doctorados después de 1998
                }
            }

            // **Reglas de acumulación**
            int puntosEspecializacionYMaestria = puntosMaestria + puntosPosgrado;
            puntosEspecializacionYMaestria = Math.Min(60, puntosEspecializacionYMaestria); // Límite de 60 puntos para especialización + maestría

            int totalPuntosPosgrado = puntosEspecializacionYMaestria + puntosDoctorado;
            totalPuntosPosgrado = Math.Min(140, totalPuntosPosgrado); // Límite de 140 puntos acumulables

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
            int puntosExperiencia = (AñosExperienciaInvestigacion * 6) +
                                     (AñosExperienciaDocente * 4) +
                                     (AñosExperienciaDireccion * 4) +
                                     (AñosExperienciaProfesional * 3);

            // Aplicar topes según categoría
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

            int puntosPorAño;
            switch (TieneCargo)
            {
                case "Rector":
                    puntosPorAño = 11;
                    break;
                case "Vicerrector":
                    puntosPorAño = 9;
                    break;
                case "Decano":
                    puntosPorAño = 6;
                    break;
                case "Vicedecano":
                    puntosPorAño = 4;
                    break;
                case "Director de Departamento":
                    puntosPorAño = 2;
                    break;
                default:
                    puntosPorAño = 0;
                    break;
            }

            return puntosPorAño * AñosCargo;
        }

        private int CalcularPuntosDesempeño()
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

                // Incremento en puntos (promedio de 3 puntos por año)
                puntosTotalesActualizados += 3;

                // Calcular el nuevo salario
                decimal salarioProyectado = puntosTotalesActualizados * valorPuntoActualizado;// * Dedicacion;

                ProyeccionSalarial.Add(new ProyeccionSalarial
                {
                    Año = DateTime.Now.Year + i + 1,
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
            // Escenario 1: Profesor Auxiliar con maestría
            new EscenarioComparativo
            {
                Nombre = "Profesor Auxiliar",
                Descripcion = "Profesor recién ingresado con título de pregrado y maestría",
                TotalPuntos = 178 + 40 + 37, // Pregrado + Maestría + Categoría Auxiliar
                SalarioMensual = (178 + 40 + 37) * ValorPunto
            },
            
            // Escenario 2: Profesor Asistente con experiencia media
            new EscenarioComparativo
            {
                Nombre = "Profesor Asistente",
                Descripcion = "Profesor con 5 años de experiencia, maestría y algunas publicaciones",
                TotalPuntos = 178 + 40 + 58 + 20 + 30 + 10, // Pregrado + Maestría + Categoría + Experiencia + Productividad + Exp. Anual
                SalarioMensual = (178 + 40 + 58 + 20 + 30 + 10) * ValorPunto
            },
            
            // Escenario 3: Profesor Asociado con doctorado
            new EscenarioComparativo
            {
                Nombre = "Profesor Asociado",
                Descripcion = "Profesor con doctorado, 10 años de experiencia y productividad moderada",
                TotalPuntos = 178 + 120 + 74 + 45 + 60 + 20, // Pregrado + Doctorado + Categoría + Experiencia + Productividad + Exp. Anual
                SalarioMensual = (178 + 120 + 74 + 45 + 60 + 20) * ValorPunto
            },
            
            // Escenario 4: Profesor Titular con alta productividad
            new EscenarioComparativo
            {
                Nombre = "Profesor Titular",
                Descripcion = "Profesor con doctorado, 20 años de experiencia y alta productividad",
                TotalPuntos = 178 + 120 + 96 + 120 + 120 + 5 + 40, // Pregrado + Doctorado + Categoría + Experiencia + Productividad + Desempeño + Exp. Anual
                SalarioMensual = (178 + 120 + 96 + 120 + 120 + 5 + 40) * ValorPunto
            },
            
            // Escenario 5: Profesor Titular con productividad extrema (caso máximo)
            new EscenarioComparativo
            {
                Nombre = "Caso Máximo",
                Descripcion = "Profesor Titular con doctorado y productividad al límite (escenario extremo)",
                TotalPuntos = 178 + 120 + 96 + 120 + 540 + 5 + 40, // Pregrado + Doctorado + Categoría + Experiencia + Productividad máxima + Desempeño + Exp. Anual
                SalarioMensual = (178 + 120 + 96 + 120 + 540 + 5 + 40) * ValorPunto
            }
        };
        }
    }

    public class ProyeccionSalarial
    {
        public int Año { get; set; }
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
