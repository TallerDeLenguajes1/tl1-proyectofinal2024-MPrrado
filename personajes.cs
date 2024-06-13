namespace Personajes_Y_Estadisticas
{
    public enum Tipo
    {
        KAMIKAZE,
        MEDICO,
        BOMBARDERO,
        GUERRERO
    }
    public class Personaje
    {
        private Datos? datos;
        private Caracteristicas? caracteristicas;

        public Datos Datos { get => datos; set => datos = value; }
        public Caracteristicas? Caracteristicas { get => caracteristicas; set => caracteristicas = value; }
    }

    public class Datos
    {
        private Tipo tipo;
        private string? nombre;
        public Datos(Tipo tipo, string? nombre)
        {
            this.tipo = tipo;
            this.nombre = nombre;
        }
        // private frase celebre con API?
    }

    public class Caracteristicas
    {
        private int ataque;
        private int armadura;
        private int salud;
    }
}