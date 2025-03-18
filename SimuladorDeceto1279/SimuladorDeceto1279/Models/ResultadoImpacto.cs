namespace SimuladorDeceto1279.Models
{
    public class ResultadoImpacto
    {
        public int TotalProfesores { get; set; }
        public decimal CostoMensualBasico { get; set; }
        public decimal CostoMensualIntermedio { get; set; }
        public decimal CostoMensualAvanzado { get; set; }
        public decimal CostoMensualAltaProductividad { get; set; }
        public decimal CostoMensualMultiplesTitulos { get; set; }
        public decimal CostoMensualAdministrativo { get; set; }
        public decimal CostoMensualTotal { get; set; }
        public decimal CostoAnualTotal { get; set; }
        public List<decimal> ProyeccionAnual { get; set; }
    }
}
