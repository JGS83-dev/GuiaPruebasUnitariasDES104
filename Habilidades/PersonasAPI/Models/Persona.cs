namespace PersonasAPI.Models
{
    public class Persona
    {
        public int Id { get; set; }
        public string PrimerNombre { get; set; } = String.Empty;
        public string? SegundoNombre { get; set; } = String.Empty;
        public string PrimerApellido { get; set; } = String.Empty;
        public string? SegundoApellido { get; set; } = String.Empty;
        public string Dui { get; set; } = String.Empty;
        public DateTime? FechaNacimiento { get; set; }
    }
}
