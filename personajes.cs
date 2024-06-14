namespace Personajes_Y_Estadisticas
{
    public enum Tipo
    {
        KAMIKAZE = 0,
        MEDICO = 1,
        BOMBARDERO = 2,
        GUERRERO = 3
    }
    public class Personaje
    {
        // private Datos? datos;
        // private Caracteristicas? caracteristicas;
        private Caracteristicas? caracteristicas; 
        private Datos? datos;

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

        public Tipo Tipo { get => tipo; }
        public string? Nombre { get => nombre;}
        // private frase celebre con API?
    }

    public class Caracteristicas
    {
        private int ataque;
        private int armadura;
        private int salud;

        public int Ataque { get => ataque; set => ataque = value; }
        public int Armadura { get => armadura; set => armadura = value; }
        public int Salud { get => salud; set => salud = value; }
    }

    public class OperacionPersonajes
    {
        public Personaje FabricaDePersonajes(int numeroPersonaje)
        {
            Personaje personaje = new Personaje();
            personaje.Caracteristicas = new Caracteristicas();
            Random randomTipo = new Random();
            Random randomAtaque_Armadura= new Random();
            // Random randomSalud = new Random();
            // Random randomNombre = new Random();
            // personaje.Datos = new Datos(Tipo.KAMIKAZE, $"nombre_API_{i}");
            personaje.Datos = new Datos((Tipo)randomTipo.Next(0,4), $"nombre_API_{numeroPersonaje+1}");
            personaje.Caracteristicas.Armadura = randomAtaque_Armadura.Next(1,10);
            personaje.Caracteristicas.Ataque = randomAtaque_Armadura.Next(1,10);
            personaje.Caracteristicas.Salud = 100;

            return personaje;
        }
    }
}