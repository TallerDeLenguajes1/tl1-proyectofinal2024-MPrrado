using Personajes_Y_Estadisticas;
using Gui;
namespace LogicaBatalla
{
    public static class Batalla 
    {
        public static Personaje SeleccionPersonaje (int nroJugador, List<Personaje> personajes, int cantidadPersonajes)
        {
            Personaje elegido;
            string[] titulo = {$"🐛SELECCIONAR PERSONAJE JUGADOR {nroJugador}🐛"};
            string[] personajesSeleccionables = new string[cantidadPersonajes];;
            for(int i = 0; i < cantidadPersonajes; i++)
            {
                personajesSeleccionables[i] = personajes[i].Datos.Nombre + " " + personajes[i].Datos.Tipo + $"({personajes[i].Caracteristicas.Ataque})"; 
            }
            Menu menuSeleccionPersonaje = new Menu(titulo, personajesSeleccionables);
            int personajeElegido = menuSeleccionPersonaje.InicializarMenuStandar(12,0);  
            elegido = personajes[personajeElegido];
            return elegido;
        }
        public static void Combate(Personaje personaje1, Personaje personaje2, int formaCombate)
        {  
            Console.Clear();
            int nroJugador=1;
            if(formaCombate == 0) //formaCombate = 0 => combate JcJ
            {
                Textos.ImprimirTextoArray(ImagenesGameplay.Worms);
                string tituloMenu ="-----OPCIONES COMBATE-----";
                string[] opcionesMenu = {"ATACAR", "DEFENDER", "HUIR"};
                Menu menuCombateJcJ = new Menu(tituloMenu, opcionesMenu);
                Textos.textoCentrado($"--------TURNO JUGADOR {nroJugador}--------");
                Console.WriteLine($"\tJUGADOR-1: {personaje1.Datos.Nombre} (SALUD: {personaje1.Caracteristicas.Salud}, ATAQUE: {personaje1.Caracteristicas.Ataque}, ARMADURA: {personaje1.Caracteristicas.Armadura}) ---------- JUGADOR-2: {personaje2.Datos.Nombre} SALUD: {personaje2.Caracteristicas.Salud}, (ATAQUE: {personaje2.Caracteristicas.Ataque}, ARMADURA: {personaje2.Caracteristicas.Armadura})");
                int opcionElegida = menuCombateJcJ.InicializarMenuCombate(9);
                if(nroJugador == 1)
                {
                    switch(opcionElegida)
                    {
                        case 1:
                            //Atacar
                            Textos.seleccionarColorFuente(3);
                            System.Console.WriteLine("JUGADOR 1 ATACA JUGADOR 2");
                            break;
                        case 2:
                            //Defender
                            Textos.seleccionarColorFuente(3);
                            System.Console.WriteLine("JUGADOR 1 SE DEFIENDE");
                            break;
                        case 3:
                            //Huir
                            Textos.seleccionarColorFuente(3);
                            System.Console.WriteLine("JUGADOR 1 HUYE, JUGADOR 2 GANA LA PELEA");
                            break;
                    }
                    nroJugador =2;
                }else
                {
                    switch(opcionElegida)
                    {
                        case 1:
                            //Atacar
                            Textos.seleccionarColorFuente(3);
                            System.Console.WriteLine("JUGADOR 2 ATACA JUGADOR 1");
                            break;
                        case 2:
                            //Defender
                            Textos.seleccionarColorFuente(3);
                            System.Console.WriteLine("JUGADOR 2 SE DEFIENDE");
                            break;
                        case 3:
                            //Huir
                            Textos.seleccionarColorFuente(3);
                            System.Console.WriteLine("JUGADOR 2 HUYE, JUGADOR 1 GANA LA PELEA");
                            break;
                    }
                    nroJugador =1;
                }
                // Console.Clear();
            }
            
            // Implementación del combate
        }
    }
}