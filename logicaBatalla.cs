using Personajes_Y_Estadisticas;
using Gui;
namespace LogicaBatalla
{
    public static class Batalla 
    {
        public static Personaje SeleccionPersonaje (int nroJugador, Personaje[]personajes, int cantidadPersonajes)
        {
            Personaje elegido;
            string[] titulo = {$"🐛SELECCIONAR PERSONAJE JUGADOR {nroJugador}🐛"};
            string[] personajesSeleccionables = new string[cantidadPersonajes];;
            for(int i = 0; i < cantidadPersonajes; i++)
            {
                personajesSeleccionables[i] = personajes[i].Datos.Nombre + " " + personajes[i].Datos.Tipo; 
            }
            Menu menuSeleccionPersonaje = new Menu(titulo, personajesSeleccionables);
            int personajeElegido = menuSeleccionPersonaje.Inicializar(12,0);  
            elegido = personajes[personajeElegido];
            return elegido;
        }
        public static void Combate(Personaje personaje1, Personaje personaje2, int formaCombate)
        {  
            ImagenesGameplay imagenPelea = new ImagenesGameplay(1);
            string[] opcionesCombates = {"Atacar", "Defender", "Huir"};
            Menu menuCombate = new Menu(imagenPelea.Worms, opcionesCombates);
            if(formaCombate == 0) //formaCombate = 0 => combate JcJ
            {

                do
                {

                    menuCombate.Inicializar(9,1);
                    Console.WriteLine($"{personaje1.Datos.Nombre} (ATAQUE: {personaje1.Caracteristicas.Ataque}, ARMADURA: {personaje1.Caracteristicas.Armadura}) vs {personaje2.Datos.Nombre} (ATAQUE: {personaje2.Caracteristicas.Ataque}, ARMADURA: {personaje2.Caracteristicas.Armadura})");
                    if(imagenPelea.NroJugador == 1)
                    {
                        imagenPelea.NroJugador = 2;
                    }else
                    {
                        imagenPelea.NroJugador = 1;
                    }

                } while(personaje1.Caracteristicas.Salud > 0 && personaje2.Caracteristicas.Salud > 0);
            }
            
            // Implementación del combate
        }
    }
}