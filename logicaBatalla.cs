using Personajes_Y_Estadisticas;
using Gui;
using Microsoft.VisualBasic;
namespace LogicaBatalla
{
    public static class Batalla 
    {
        public static Personaje SeleccionPersonaje (int nroJugador, List<Personaje> personajes, int cantidadPersonajes)
        {
            Personaje elegido;
            string[] titulo = {$"------->  ⚔️  ⚔️  SELECCIONAR PERSONAJE JUGADOR {nroJugador}  ⚔️  ⚔️  <-------"};
            string[] personajesSeleccionables = new string[cantidadPersonajes];;
            for(int i = 0; i < cantidadPersonajes; i++)
            {
                personajesSeleccionables[i] = personajes[i].Datos.Nombre + " " + personajes[i].Datos.Tipo + $"(💪 {personajes[i].Caracteristicas.Ataque} 🛡️  {personajes[i].Caracteristicas.Armadura})"; 
            }
            Menu menuSeleccionPersonaje = new Menu(titulo, personajesSeleccionables);
            int personajeElegido = menuSeleccionPersonaje.InicializarMenuStandar(12,0);  
            elegido = personajes[personajeElegido-1];
            return elegido;
        }
        public static void Combate(Personaje personaje1, Personaje personaje2, int formaCombate)
        {  
            // Implementación del combate
            Console.Clear();
            int nroJugador=1;
            if(formaCombate == 0) //formaCombate = 0 => combate JcJ
            {
                Textos.ImprimirTextoArray(ImagenesGameplay.Worms);
                string tituloMenu ="-----OPCIONES COMBATE-----";
                string[] opcionesMenu = {"ATACAR", "DEFENDER", "HUIR"};
                Menu menuCombateJcJ = new Menu(tituloMenu, opcionesMenu);
                bool huye1 = false;
                bool huye2 = false;
                do
                {
                    Textos.textoCentrado($"--------TURNO JUGADOR {nroJugador}--------");
                    Console.WriteLine($"\tJUGADOR-1: {personaje1.Datos.Nombre} (SALUD: {personaje1.Caracteristicas.Salud}, ATAQUE: {personaje1.Caracteristicas.Ataque}, ARMADURA: {personaje1.Caracteristicas.Armadura}) ---------- JUGADOR-2: {personaje2.Datos.Nombre} SALUD: {personaje2.Caracteristicas.Salud}, (ATAQUE: {personaje2.Caracteristicas.Ataque}, ARMADURA: {personaje2.Caracteristicas.Armadura})");

                    int opcionElegida = menuCombateJcJ.InicializarMenuCombate(9);

                    int dañoCausado = 0;
                    const int MULTIPLICADOR_DAÑO_BASE = 5;
                    Random esquiva = new Random();

                    if(nroJugador == 1)
                    {
                        switch(opcionElegida)
                        {
                            case 1:
                                //Atacar
                                Textos.seleccionarColorFuente(3);
                                System.Console.WriteLine("JUGADOR 1 ATACA JUGADOR 2...");
                                dañoCausado =   personaje1.Caracteristicas.Ataque * MULTIPLICADOR_DAÑO_BASE - personaje2.Caracteristicas.Armadura/2;
                                break;
                            case 2:
                                //Defender
                                Textos.seleccionarColorFuente(3);
                                System.Console.WriteLine("JUGADOR 1 SE DEFIENDE...");
                                esquiva.Next(1, 100);
                                break;
                            case 3:
                                //Huir
                                Textos.seleccionarColorFuente(3);
                                System.Console.WriteLine("JUGADOR 1 HUYE... JUGADOR 2 GANA LA PELEA");
                                huye1 = true;
                                break;
                        }
                    }else
                    {
                        switch(opcionElegida)
                        {
                            case 1:
                                //Atacar
                                Textos.seleccionarColorFuente(3);
                                System.Console.WriteLine("JUGADOR 2 ATACA JUGADOR 1...");
                                dañoCausado =   personaje2.Caracteristicas.Ataque * MULTIPLICADOR_DAÑO_BASE - personaje1.Caracteristicas.Armadura/2;
                                break;
                            case 2:
                                //Defender
                                Textos.seleccionarColorFuente(3);
                                System.Console.WriteLine("JUGADOR 2 SE DEFIENDE...");
                                esquiva.Next(1, 100);
                                break;
                            case 3:
                                //Huir
                                Textos.seleccionarColorFuente(3);
                                System.Console.WriteLine("JUGADOR 2 HUYE... JUGADOR 1 GANA LA PELEA");
                                huye1 = true;
                                break;
                        }
                    }

                    if(dañoCausado <= 0)
                    {
                        System.Console.WriteLine($"EL ATAQUE DEL JUGADOR {nroJugador} ES NULO...");
                        Textos.seleccionarColorFuente(10);
                        System.Console.WriteLine($"JUGADOR {nroJugador} NO RECIBE DAÑO");
                        Console.ResetColor();
                    }else if(nroJugador == 1)
                    {
                        personaje2.Caracteristicas.Salud -= dañoCausado;
                        Textos.seleccionarColorFuente(12);
                        System.Console.WriteLine($"JUGADOR 1 CAUSA {dañoCausado} DE DAÑO...");
                        Console.ResetColor();
                    }else if(nroJugador == 2)
                    {
                        personaje1.Caracteristicas.Salud -= dañoCausado;
                        Textos.seleccionarColorFuente(12);
                        System.Console.WriteLine($"JUGADOR 2 CAUSA {dañoCausado} DE DAÑO...");
                        Console.ResetColor();
                    }

                    if(nroJugador == 1)
                    {
                        nroJugador = 2;
                    } else
                    {
                        nroJugador = 1;
                    }

                    Console.ResetColor();
                }while(personaje1.Caracteristicas.Salud > 0 && personaje2.Caracteristicas.Salud > 0 && !huye1 && !huye2 );

                if(personaje1.Caracteristicas.Salud <= 0)
                {
                    // Console.Clear();
                    // Textos.ImprimirTextoArray(ImagenesGameplay.GameOver);
                    Textos.seleccionarColorFuente(14);
                    Textos.textoCentrado("🏆  🏆  -------->JUGADOR 2 GANA LA PELEA!<--------  🏆  🏆");
                    Console.ResetColor();
                }else if(personaje2.Caracteristicas.Salud <= 0)
                {
                    Textos.seleccionarColorFuente(14);
                    Textos.textoCentrado("🏆  🏆  -------->JUGADOR 1 GANA LA PELEA!<--------  🏆  🏆");
                    Console.ResetColor();
                }
                
            }
            
        }
    }
}