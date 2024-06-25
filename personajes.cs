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

        public Datos Datos { get => datos; set => datos = value; }
        public Caracteristicas Caracteristicas { get => caracteristicas; set => caracteristicas = value; }
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
        public string Nombre { get => nombre;}
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
        public Personaje FabricaDePersonajes(List<string> listadoNombres) 
        {
            Random randomTipo = new Random();
            Random randomAtaque_Armadura= new Random();
            Random randomNombre = new Random();

            Personaje personaje = new Personaje //esta es una forma simplificada de instancias clases dentro de otra(?)
            {
                Caracteristicas = new Caracteristicas(),
                Datos = new Datos((Tipo)randomTipo.Next(0, 4), listadoNombres[randomNombre.Next(0,50)])
            };

            personaje.Caracteristicas.Armadura = randomAtaque_Armadura.Next(1,10);
            personaje.Caracteristicas.Ataque = randomAtaque_Armadura.Next(1,10);
            personaje.Caracteristicas.Salud = 100;
            // API.GuardarEnJson(listadoNombres);
            return personaje;
        }
    }
}