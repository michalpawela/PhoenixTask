namespace Common.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public int Lp { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public decimal Zarobki { get; set; }
        public string PoziomStanowiska { get; set; }
        public string MiejsceZamieszkania { get; set; }
    }
}
