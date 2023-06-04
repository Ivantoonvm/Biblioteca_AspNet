namespace Proyecto_biblioteca.Models;

    public class Persona
    {
        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public string Codigo { get; set; }
        public TipoPersona oTipoPersona { get; set; }
        public bool Estado { get; set; }
    }
