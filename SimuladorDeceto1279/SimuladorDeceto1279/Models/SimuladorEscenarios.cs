namespace SimuladorDeceto1279.Models
{
    public class SimuladorEscenarios
    {
        private ModeloSalarial _modelo = new ModeloSalarial();

        public ResultadoSimulacion GenerarEscenario(EscenarioSimulacion escenario)
        {
            switch (escenario)
            {
                case EscenarioSimulacion.Basico:
                    return EscenarioBasico();
                case EscenarioSimulacion.Intermedio:
                    return EscenarioIntermedio();
                case EscenarioSimulacion.Avanzado:
                    return EscenarioAvanzado();
                case EscenarioSimulacion.AltaProductividad:
                    return EscenarioAltaProductividad();
                case EscenarioSimulacion.MultiplesTitulos:
                    return EscenarioMultiplesTitulos();
                case EscenarioSimulacion.Administrativo:
                    return EscenarioAdministrativo();
                default:
                    return EscenarioBasico();
            }
        }

        private ResultadoSimulacion EscenarioBasico()
        {
            // Profesor recién ingresado con maestría
            var tiposPosgrado = new List<string> { "Maestría" };
            var duracionPosgrado = new List<int> { 2 };

            int puntosTitulos = _modelo.CalcularPuntosTitulos(true, "Normal", tiposPosgrado, duracionPosgrado);
            int puntosCategoria = _modelo.CalcularPuntosCategoria("Auxiliar");
            int puntosExperiencia = _modelo.CalcularPuntosExperiencia(0, 1, 0, 0, "Auxiliar");
            int puntosProductividad = _modelo.CalcularPuntosProductividad(0, 1, 0, 0, 0, 0, 0, 0, 0, "Auxiliar");
            int puntosCargos = 0;
            int puntosDesempeño = 0;

            int totalPuntos = _modelo.CalcularTotalPuntos(
                puntosTitulos, puntosCategoria, puntosExperiencia,
                puntosProductividad, puntosCargos, puntosDesempeño);

            decimal salario = _modelo.CalcularRemuneracionMensual(totalPuntos, _modelo.ValorPunto, 1.0M);

            return new ResultadoSimulacion
            {
                Descripcion = "Profesor recién ingresado con título de pregrado y maestría",
                PuntosTitulos = puntosTitulos,
                PuntosCategoria = puntosCategoria,
                PuntosExperiencia = puntosExperiencia,
                PuntosProductividad = puntosProductividad,
                PuntosCargos = puntosCargos,
                PuntosDesempeño = puntosDesempeño,
                TotalPuntos = totalPuntos,
                SalarioMensual = salario
            };
        }

        private ResultadoSimulacion EscenarioIntermedio()
        {
            // Profesor con 5 años de experiencia y algunas publicaciones
            var tiposPosgrado = new List<string> { "Maestría" };
            var duracionPosgrado = new List<int> { 2 };

            int puntosTitulos = _modelo.CalcularPuntosTitulos(true, "Normal", tiposPosgrado, duracionPosgrado);
            int puntosCategoria = _modelo.CalcularPuntosCategoria("Asistente");
            int puntosExperiencia = _modelo.CalcularPuntosExperiencia(2, 5, 0, 0, "Asistente");
            int puntosProductividad = _modelo.CalcularPuntosProductividad(1, 2, 3, 2, 0, 1, 0, 0, 0, "Asistente");
            int puntosCargos = 0;
            int puntosDesempeño = _modelo.CalcularPuntosDesempeño("Asistente", true, false);
            int puntosExperienciaAnual = _modelo.CalcularPuntosExperienciaAnual(5);

            int totalPuntos = _modelo.CalcularTotalPuntos(
                puntosTitulos, puntosCategoria, puntosExperiencia,
                puntosProductividad, puntosCargos, puntosDesempeño) + puntosExperienciaAnual;

            decimal salario = _modelo.CalcularRemuneracionMensual(totalPuntos, _modelo.ValorPunto, 1.0M);

            return new ResultadoSimulacion
            {
                Descripcion = "Profesor con 5 años de experiencia, categoría Asistente y algunas publicaciones",
                PuntosTitulos = puntosTitulos,
                PuntosCategoria = puntosCategoria,
                PuntosExperiencia = puntosExperiencia,
                PuntosProductividad = puntosProductividad,
                PuntosCargos = puntosCargos,
                PuntosDesempeño = puntosDesempeño,
                PuntosExperienciaAnual = puntosExperienciaAnual,
                TotalPuntos = totalPuntos,
                SalarioMensual = salario
            };
        }

        private ResultadoSimulacion EscenarioAvanzado()
        {
            // Profesor titular con doctorado y alta productividad
            var tiposPosgrado = new List<string> { "Maestría", "Doctorado" };
            var duracionPosgrado = new List<int> { 2, 4 };

            int puntosTitulos = _modelo.CalcularPuntosTitulos(true, "Normal", tiposPosgrado, duracionPosgrado);
            int puntosCategoria = _modelo.CalcularPuntosCategoria("Titular");
            int puntosExperiencia = _modelo.CalcularPuntosExperiencia(8, 15, 2, 3, "Titular");
            int puntosProductividad = _modelo.CalcularPuntosProductividad(8, 10, 12, 5, 3, 2, 1, 1, 0, "Titular");
            int puntosCargos = 0;
            int puntosDesempeño = _modelo.CalcularPuntosDesempeño("Titular", true, false);
            int puntosExperienciaAnual = _modelo.CalcularPuntosExperienciaAnual(15);

            int totalPuntos = _modelo.CalcularTotalPuntos(
                puntosTitulos, puntosCategoria, puntosExperiencia,
                puntosProductividad, puntosCargos, puntosDesempeño) + puntosExperienciaAnual;

            decimal salario = _modelo.CalcularRemuneracionMensual(totalPuntos, _modelo.ValorPunto, 1.0M);

            return new ResultadoSimulacion
            {
                Descripcion = "Profesor Titular con doctorado, 15 años de experiencia y alta productividad académica",
                PuntosTitulos = puntosTitulos,
                PuntosCategoria = puntosCategoria,
                PuntosExperiencia = puntosExperiencia,
                PuntosProductividad = puntosProductividad,
                PuntosCargos = puntosCargos,
                PuntosDesempeño = puntosDesempeño,
                PuntosExperienciaAnual = puntosExperienciaAnual,
                TotalPuntos = totalPuntos,
                SalarioMensual = salario
            };
        }

        private ResultadoSimulacion EscenarioAltaProductividad()
        {
            // Profesor con productividad académica extrema
            var tiposPosgrado = new List<string> { "Maestría", "Doctorado" };
            var duracionPosgrado = new List<int> { 2, 4 };

            int puntosTitulos = _modelo.CalcularPuntosTitulos(true, "Normal", tiposPosgrado, duracionPosgrado);
            int puntosCategoria = _modelo.CalcularPuntosCategoria("Titular");
            int puntosExperiencia = _modelo.CalcularPuntosExperiencia(12, 18, 3, 2, "Titular");
            // Productividad al máximo posible
            int puntosProductividad = 540; // Tope para Titular
            int puntosCargos = 0;
            int puntosDesempeño = _modelo.CalcularPuntosDesempeño("Titular", true, false);
            int puntosExperienciaAnual = _modelo.CalcularPuntosExperienciaAnual(18);

            int totalPuntos = _modelo.CalcularTotalPuntos(
                puntosTitulos, puntosCategoria, puntosExperiencia,
                puntosProductividad, puntosCargos, puntosDesempeño) + puntosExperienciaAnual;

            decimal salario = _modelo.CalcularRemuneracionMensual(totalPuntos, _modelo.ValorPunto, 1.0M);

            return new ResultadoSimulacion
            {
                Descripcion = "Profesor Titular con productividad académica extrema (caso máximo)",
                PuntosTitulos = puntosTitulos,
                PuntosCategoria = puntosCategoria,
                PuntosExperiencia = puntosExperiencia,
                PuntosProductividad = puntosProductividad,
                PuntosCargos = puntosCargos,
                PuntosDesempeño = puntosDesempeño,
                PuntosExperienciaAnual = puntosExperienciaAnual,
                TotalPuntos = totalPuntos,
                SalarioMensual = salario
            };
        }

        private ResultadoSimulacion EscenarioMultiplesTitulos()
        {
            // Profesor con múltiples títulos académicos
            var tiposPosgrado = new List<string> {
            "Especialización", "Especialización",
            "Maestría", "Maestría",
            "Doctorado"
        };
            var duracionPosgrado = new List<int> { 1, 2, 2, 2, 4 };

            int puntosTitulos = _modelo.CalcularPuntosTitulos(true, "Normal", tiposPosgrado, duracionPosgrado);
            int puntosCategoria = _modelo.CalcularPuntosCategoria("Asociado");
            int puntosExperiencia = _modelo.CalcularPuntosExperiencia(5, 10, 0, 5, "Asociado");
            int puntosProductividad = _modelo.CalcularPuntosProductividad(4, 6, 8, 10, 1, 1, 0, 0, 0, "Asociado");
            int puntosCargos = 0;
            int puntosDesempeño = _modelo.CalcularPuntosDesempeño("Asociado", true, false);
            int puntosExperienciaAnual = _modelo.CalcularPuntosExperienciaAnual(10);

            int totalPuntos = _modelo.CalcularTotalPuntos(
                puntosTitulos, puntosCategoria, puntosExperiencia,
                puntosProductividad, puntosCargos, puntosDesempeño) + puntosExperienciaAnual;

            decimal salario = _modelo.CalcularRemuneracionMensual(totalPuntos, _modelo.ValorPunto, 1.0M);

            return new ResultadoSimulacion
            {
                Descripcion = "Profesor Asociado con múltiples títulos académicos",
                PuntosTitulos = puntosTitulos,
                PuntosCategoria = puntosCategoria,
                PuntosExperiencia = puntosExperiencia,
                PuntosProductividad = puntosProductividad,
                PuntosCargos = puntosCargos,
                PuntosDesempeño = puntosDesempeño,
                PuntosExperienciaAnual = puntosExperienciaAnual,
                TotalPuntos = totalPuntos,
                SalarioMensual = salario
            };
        }

        private ResultadoSimulacion EscenarioAdministrativo()
        {
            // Profesor que asume cargos administrativos
            var tiposPosgrado = new List<string> { "Maestría", "Doctorado" };
            var duracionPosgrado = new List<int> { 2, 4 };

            int puntosTitulos = _modelo.CalcularPuntosTitulos(true, "Normal", tiposPosgrado, duracionPosgrado);
            int puntosCategoria = _modelo.CalcularPuntosCategoria("Asociado");
            int puntosExperiencia = _modelo.CalcularPuntosExperiencia(3, 8, 0, 2, "Asociado");
            int puntosProductividad = _modelo.CalcularPuntosProductividad(2, 3, 5, 3, 1, 0, 0, 0, 0, "Asociado");
            int puntosCargos = _modelo.CalcularPuntosCargos("Decano", 4);
            int puntosDesempeño = 0; // No aplica para cargos administrativos
            int puntosExperienciaAnual = _modelo.CalcularPuntosExperienciaAnual(8);

            int totalPuntos = _modelo.CalcularTotalPuntos(
                puntosTitulos, puntosCategoria, puntosExperiencia,
                puntosProductividad, puntosCargos, puntosDesempeño) + puntosExperienciaAnual;

            decimal salario = _modelo.CalcularRemuneracionMensual(totalPuntos, _modelo.ValorPunto, 1.0M);

            return new ResultadoSimulacion
            {
                Descripcion = "Profesor Asociado con cargo de Decano durante 4 años",
                PuntosTitulos = puntosTitulos,
                PuntosCategoria = puntosCategoria,
                PuntosExperiencia = puntosExperiencia,
                PuntosProductividad = puntosProductividad,
                PuntosCargos = puntosCargos,
                PuntosDesempeño = puntosDesempeño,
                PuntosExperienciaAnual = puntosExperienciaAnual,
                TotalPuntos = totalPuntos,
                SalarioMensual = salario
            };
        }
    }
}
