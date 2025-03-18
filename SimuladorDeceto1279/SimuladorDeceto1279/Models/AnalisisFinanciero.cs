namespace SimuladorDeceto1279.Models
{
    public class AnalisisFinanciero
    {
        public SimuladorEscenarios _simulador = new SimuladorEscenarios();
        public ModeloSalarial _modelo = new ModeloSalarial();

        // Calcular el impacto en la nómina de una universidad
        public ResultadoImpacto CalcularImpactoNomina(
            int numProfesorBasico,
            int numProfesorIntermedio,
            int numProfesorAvanzado,
            int numProfesorAltaProductividad,
            int numProfesorMultiplesTitulos,
            int numProfesorAdministrativo)
        {
            var escenarioBasico = _simulador.GenerarEscenario(EscenarioSimulacion.Basico);
            var escenarioIntermedio = _simulador.GenerarEscenario(EscenarioSimulacion.Intermedio);
            var escenarioAvanzado = _simulador.GenerarEscenario(EscenarioSimulacion.Avanzado);
            var escenarioAltaProductividad = _simulador.GenerarEscenario(EscenarioSimulacion.AltaProductividad);
            var escenarioMultiplesTitulos = _simulador.GenerarEscenario(EscenarioSimulacion.MultiplesTitulos);
            var escenarioAdministrativo = _simulador.GenerarEscenario(EscenarioSimulacion.Administrativo);

            decimal costoMensualBasico = escenarioBasico.SalarioMensual * numProfesorBasico;
            decimal costoMensualIntermedio = escenarioIntermedio.SalarioMensual * numProfesorIntermedio;
            decimal costoMensualAvanzado = escenarioAvanzado.SalarioMensual * numProfesorAvanzado;
            decimal costoMensualAltaProductividad = escenarioAltaProductividad.SalarioMensual * numProfesorAltaProductividad;
            decimal costoMensualMultiplesTitulos = escenarioMultiplesTitulos.SalarioMensual * numProfesorMultiplesTitulos;
            decimal costoMensualAdministrativo = escenarioAdministrativo.SalarioMensual * numProfesorAdministrativo;

            int totalProfesores = numProfesorBasico + numProfesorIntermedio + numProfesorAvanzado +
                                 numProfesorAltaProductividad + numProfesorMultiplesTitulos + numProfesorAdministrativo;

            decimal costoMensualTotal = costoMensualBasico + costoMensualIntermedio + costoMensualAvanzado +
                                      costoMensualAltaProductividad + costoMensualMultiplesTitulos + costoMensualAdministrativo;

            decimal costoAnual = costoMensualTotal * 12;

            // Proyección a 5 años con un incremento anual del valor del punto de 6%
            List<decimal> proyeccionAnual = new List<decimal>();
            decimal valorPuntoProyectado = _modelo.ValorPunto;

            for (int i = 0; i < 5; i++)
            {
                valorPuntoProyectado *= 1.06M; // 6% de incremento anual
                decimal factorIncremento = valorPuntoProyectado / _modelo.ValorPunto;

                decimal costoProyectado = costoAnual * factorIncremento;
                // Adicional 3% por incremento de puntos (productividad, ascensos, etc.)
                costoProyectado *= (1 + (0.03M * (i + 1)));

                proyeccionAnual.Add(costoProyectado);
            }

            return new ResultadoImpacto
            {
                TotalProfesores = totalProfesores,
                CostoMensualBasico = costoMensualBasico,
                CostoMensualIntermedio = costoMensualIntermedio,
                CostoMensualAvanzado = costoMensualAvanzado,
                CostoMensualAltaProductividad = costoMensualAltaProductividad,
                CostoMensualMultiplesTitulos = costoMensualMultiplesTitulos,
                CostoMensualAdministrativo = costoMensualAdministrativo,
                CostoMensualTotal = costoMensualTotal,
                CostoAnualTotal = costoAnual,
                ProyeccionAnual = proyeccionAnual
            };
        }

        // Evaluar el riesgo financiero de diferentes distribuciones de profesores
        public ResultadoRiesgo EvaluarRiesgoFinanciero(decimal presupuestoAnualUniversidad)
        {
            // Escenario normal (base)
            var impactoBase = CalcularImpactoNomina(40, 30, 20, 5, 3, 2);

            // Escenario de riesgo 1: Alta concentración de profesores titulares
            var impactoRiesgo1 = CalcularImpactoNomina(10, 20, 40, 20, 5, 5);

            // Escenario de riesgo 2: Producción académica masiva
            var impactoRiesgo2 = CalcularImpactoNomina(20, 20, 20, 30, 5, 5);

            // Escenario de riesgo 3: Múltiples títulos y alta categoría
            var impactoRiesgo3 = CalcularImpactoNomina(10, 15, 25, 20, 25, 5);

            decimal porcentajePresupuestoBase = impactoBase.CostoAnualTotal / presupuestoAnualUniversidad * 100;
            decimal porcentajePresupuestoRiesgo1 = impactoRiesgo1.CostoAnualTotal / presupuestoAnualUniversidad * 100;
            decimal porcentajePresupuestoRiesgo2 = impactoRiesgo2.CostoAnualTotal / presupuestoAnualUniversidad * 100;
            decimal porcentajePresupuestoRiesgo3 = impactoRiesgo3.CostoAnualTotal / presupuestoAnualUniversidad * 100;

            // Proyecciones a 5 años
            decimal porcentajePresupuestoBaseAño5 = impactoBase.ProyeccionAnual[4] / (presupuestoAnualUniversidad * (decimal)Math.Pow(1.04, 5)) * 100;
            decimal porcentajePresupuestoRiesgo1Año5 = impactoRiesgo1.ProyeccionAnual[4] / (presupuestoAnualUniversidad * (decimal)Math.Pow(1.04, 5)) * 100;
            decimal porcentajePresupuestoRiesgo2Año5 = impactoRiesgo2.ProyeccionAnual[4] / (presupuestoAnualUniversidad * (decimal)Math.Pow(1.04, 5)) * 100;
            decimal porcentajePresupuestoRiesgo3Año5 = impactoRiesgo3.ProyeccionAnual[4] / (presupuestoAnualUniversidad * (decimal)Math.Pow(1.04, 5)) * 100;

            return new ResultadoRiesgo
            {
                CostoBaseAnual = impactoBase.CostoAnualTotal,
                CostoRiesgo1Anual = impactoRiesgo1.CostoAnualTotal,
                CostoRiesgo2Anual = impactoRiesgo2.CostoAnualTotal,
                CostoRiesgo3Anual = impactoRiesgo3.CostoAnualTotal,
                PorcentajePresupuestoBase = porcentajePresupuestoBase,
                PorcentajePresupuestoRiesgo1 = porcentajePresupuestoRiesgo1,
                PorcentajePresupuestoRiesgo2 = porcentajePresupuestoRiesgo2,
                PorcentajePresupuestoRiesgo3 = porcentajePresupuestoRiesgo3,
                PorcentajePresupuestoBaseAño5 = porcentajePresupuestoBaseAño5,
                PorcentajePresupuestoRiesgo1Año5 = porcentajePresupuestoRiesgo1Año5,
                PorcentajePresupuestoRiesgo2Año5 = porcentajePresupuestoRiesgo2Año5,
                PorcentajePresupuestoRiesgo3Año5 = porcentajePresupuestoRiesgo3Año5
            };
        }
    }
}
