namespace SimuladorDeceto1279.Models
{
    public class ModeloSalarial
    {
        // Valor del punto para el cálculo salarial (este valor se actualiza anualmente)
        // Valor inicial del decreto en 2002: $6,435 COP
        // Para 2025 se estima en aproximadamente $20,895 COP
        public decimal ValorPunto { get; set; } = 20895.00M;

        // Cálculo de la remuneración mensual
        public decimal CalcularRemuneracionMensual(int totalPuntos, decimal valorPunto, decimal dedicacion)
        {
            return totalPuntos * valorPunto * dedicacion;
        }

        // Cálculo del total de puntos según los factores del decreto
        public int CalcularTotalPuntos(
            int puntosTitulos,
            int puntosCategoria,
            int puntosExperiencia,
            int puntosProductividad,
            int puntosCargos,
            int puntosDesempeño)
        {
            return puntosTitulos + puntosCategoria + puntosExperiencia +
                   puntosProductividad + puntosCargos + puntosDesempeño;
        }

        // Cálculo de puntos por títulos académicos
        public int CalcularPuntosTitulos(bool tienePregrado, string tipoPregrado,
                                        List<string> tiposPosgrado, List<int> duracionPosgrado)
        {
            int puntos = 0;

            // Puntos por pregrado
            if (tienePregrado)
            {
                if (tipoPregrado == "Medicina Humana" || tipoPregrado == "Composición Musical")
                    puntos += 183;
                else
                    puntos += 178;
            }

            // Puntos por posgrado
            int puntosPosgrado = 0;
            bool tieneDoctorado = false;
            bool tieneMaestria = false;
            int puntosEspecializacion = 0;

            for (int i = 0; i < tiposPosgrado.Count; i++)
            {
                string tipo = tiposPosgrado[i];
                int duracion = duracionPosgrado[i];

                if (tipo == "Doctorado")
                {
                    tieneDoctorado = true;
                    if (!tieneMaestria)
                        puntosPosgrado += 120; // Doctorado sin maestría
                    else
                        puntosPosgrado += 80;  // Doctorado con maestría previa
                }
                else if (tipo == "Maestría")
                {
                    tieneMaestria = true;
                    if (!tieneDoctorado) // Solo suma puntos por maestría si no se contó en doctorado
                        puntosPosgrado += 40;
                }
                else if (tipo == "Especialización")
                {
                    int puntosEsp = 0;
                    if (duracion >= 1 && duracion <= 2)
                        puntosEsp = 20;
                    else if (duracion > 2)
                        puntosEsp = 20 + Math.Min(10, (duracion - 2) * 10); // Máximo 10 puntos adicionales

                    puntosEspecializacion += puntosEsp;
                }
                else if (tipo == "Especialización Clínica")
                {
                    puntosPosgrado += Math.Min(75, duracion * 15); // 15 puntos por año, máximo 75
                }
            }

            // Límite de 30 puntos por especializaciones comunes
            puntosPosgrado += Math.Min(30, puntosEspecializacion);

            // Máximo 140 puntos por títulos de posgrado
            puntos += Math.Min(140, puntosPosgrado);

            return puntos;
        }

        // Cálculo de puntos por categoría en el escalafón
        public int CalcularPuntosCategoria(string categoria)
        {
            switch (categoria)
            {
                case "Auxiliar":
                    return 37;
                case "Asistente":
                    return 58;
                case "Asociado":
                    return 74;
                case "Titular":
                    return 96;
                default:
                    return 0;
            }
        }

        // Cálculo de puntos por experiencia calificada
        public int CalcularPuntosExperiencia(int añosInvestigacion, int añosDocencia,
                                             int añosDireccion, int añosProfesional, string categoria)
        {
            int puntosTotales = (añosInvestigacion * 6) + (añosDocencia * 4) +
                               (añosDireccion * 4) + (añosProfesional * 3);

            // Topes según categoría
            int topePuntos;
            switch (categoria)
            {
                case "Auxiliar":
                    topePuntos = 20;
                    break;
                case "Asistente":
                    topePuntos = 45;
                    break;
                case "Asociado":
                    topePuntos = 90;
                    break;
                case "Titular":
                    topePuntos = 120;
                    break;
                default:
                    topePuntos = 0;
                    break;
            }

            return Math.Min(topePuntos, puntosTotales);
        }

        // Cálculo de puntos por productividad académica
        public int CalcularPuntosProductividad(
            int articulosA1, int articulosA2, int articulosB, int articulosC,
            int librosInvestigacion, int librosTexto, int librosEnsayo,
            int patentes, int obrasArtisticas, string categoria)
        {
            int puntosTotales = (articulosA1 * 15) + (articulosA2 * 12) + (articulosB * 8) + (articulosC * 3) +
                               (librosInvestigacion * 20) + (librosTexto * 15) + (librosEnsayo * 15) +
                               (patentes * 25) + (obrasArtisticas * 14);

            // Topes según categoría
            int topePuntos;
            switch (categoria)
            {
                case "Auxiliar":
                    topePuntos = 80;
                    break;
                case "Asistente":
                    topePuntos = 160;
                    break;
                case "Asociado":
                    topePuntos = 320;
                    break;
                case "Titular":
                    topePuntos = 540;
                    break;
                default:
                    topePuntos = 0;
                    break;
            }

            return Math.Min(topePuntos, puntosTotales);
        }

        // Cálculo de puntos por cargos académico-administrativos
        public int CalcularPuntosCargos(string cargo, int años)
        {
            int puntosPorAño;
            switch (cargo)
            {
                case "Rector":
                    puntosPorAño = 11;
                    break;
                case "Vicerrector":
                case "Secretario General":
                    puntosPorAño = 9;
                    break;
                case "Decano":
                case "Director de División":
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

            return puntosPorAño * años;
        }

        // Cálculo de puntos por desempeño destacado
        public int CalcularPuntosDesempeño(string categoria, bool destacadoDocencia, bool destacadoExtension)
        {
            if (!destacadoDocencia && !destacadoExtension)
                return 0;

            int puntosPorCategoria;
            switch (categoria)
            {
                case "Auxiliar":
                    puntosPorCategoria = 2;
                    break;
                case "Asistente":
                    puntosPorCategoria = 3;
                    break;
                case "Asociado":
                    puntosPorCategoria = 4;
                    break;
                case "Titular":
                    puntosPorCategoria = 5;
                    break;
                default:
                    puntosPorCategoria = 0;
                    break;
            }

            // Solo se puede recibir reconocimiento por uno de los dos conceptos
            return puntosPorCategoria;
        }

        // Cálculo de puntos por experiencia calificada anual (2 puntos por año desde 2003)
        public int CalcularPuntosExperienciaAnual(int añosServicio)
        {
            return añosServicio * 2;
        }

        // Proyección del salario a futuro basado en incrementos anuales
        public List<ProyeccionSalarial> ProyectarSalarioFuturo(
            int totalPuntos, decimal valorPuntoActual, decimal dedicacion,
            int años, decimal incrementoAnualPunto, int puntosAdicionalsPorAño)
        {
            var proyecciones = new List<ProyeccionSalarial>();
            decimal valorPuntoActualizado = valorPuntoActual;
            int puntosTotalesActualizados = totalPuntos;

            for (int i = 0; i < años; i++)
            {
                // Incremento anual en el valor del punto
                valorPuntoActualizado *= (1 + incrementoAnualPunto);

                // Incremento en puntos (experiencia, productividad, etc.)
                puntosTotalesActualizados += puntosAdicionalsPorAño;

                // Cálculo del nuevo salario
                decimal salarioProyectado = CalcularRemuneracionMensual(
                    puntosTotalesActualizados, valorPuntoActualizado, dedicacion);

                proyecciones.Add(new ProyeccionSalarial
                {
                    Año = DateTime.Now.Year + i + 1,
                    ValorPunto = valorPuntoActualizado,
                    TotalPuntos = puntosTotalesActualizados,
                    SalarioMensual = salarioProyectado
                });
            }

            return proyecciones;
        }
    }

}
