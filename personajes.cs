using APIS;

namespace Personajes_Y_Estadisticas
{
    public enum Tipo //enum del tipo de personaje 
    {
        KAMIKAZE = 0,
        MEDICO = 1,
        BOMBARDERO = 2,
        GUERRERO = 3
    }
    public class Personaje
    {
        private Caracteristicas caracteristicas;
        private Datos datos;

        public Personaje(Tipo tipo, string nombre, double ataque, double armadura, double salud)
        {
            caracteristicas = new Caracteristicas(ataque, armadura, salud);
            datos = new Datos(tipo, nombre);
        }

        public Caracteristicas Caracteristicas { get => caracteristicas; }
        public Datos Datos { get => datos; }
    }

    public class Datos
    {
        private Tipo tipo;
        private string nombre;
        public Datos(Tipo tipo, string nombre)
        {
            this.tipo = tipo;
            this.nombre = nombre;
        }

        public Tipo Tipo { get => tipo; }
        public string Nombre { get => nombre; }
        // private frase celebre con API?
    }

    public class Caracteristicas
    {
        private double ataque;
        private double armadura;
        private double salud;

        public Caracteristicas(double ataque, double armadura, double salud)
        {
            this.ataque = ataque;
            this.armadura = armadura;
            this.salud = salud;
        }

        public double Ataque { get => ataque; }
        public double Armadura { get => armadura; }
        public double Salud { get => salud; set => salud = value;}
    }

    public class FabricaDePersonajes
    {
        private readonly List<string> listadoNombres;

        public FabricaDePersonajes(List<string> listadoNombres)
        {
            this.listadoNombres = listadoNombres;
        }

        public Personaje CrearPersonaje(int indiceNombre)
        {
            Random randomCaracteristicas = new Random();
            int ataque = randomCaracteristicas.Next(5, 20);
            int armadura = randomCaracteristicas.Next(10, 50);
            int salud = 100;
            Tipo tipo = (Tipo)randomCaracteristicas.Next(0, 3);
            string nombre = listadoNombres[indiceNombre];
            Personaje personaje = new Personaje(tipo, nombre, ataque, armadura, salud);
            return personaje;
        }
    }
}