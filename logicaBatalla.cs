using Personajes_Y_Estadisticas;
using Gui;
using Microsoft.VisualBasic;
using System.Threading.Tasks.Dataflow;
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
            int personajeElegido = menuSeleccionPersonaje.InicializarMenuStandar(12);  
            elegido = personajes[personajeElegido];
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
                string[] opcionesMenu = {"ATACAR", "CURAR", "HUIR"};
                Menu menuCombateJcJ = new Menu(tituloMenu, opcionesMenu);
                bool huye1 = false;
                bool huye2 = false;
                const int MULTIPLICADOR_DAÑO_BASE = 3;
                double damageCausado = 0;
                int esquiva = 1;
                double factorAleatorioAtaque;
                Random random = new Random();
                do
                {
                    Textos.textoCentrado($"--------TURNO JUGADOR {nroJugador}--------");
                    Console.WriteLine($"\tJUGADOR-1: {personaje1.Datos.Nombre} (SALUD: {personaje1.Caracteristicas.Salud}, ATAQUE: {personaje1.Caracteristicas.Ataque}, ARMADURA: {personaje1.Caracteristicas.Armadura}) ---------- JUGADOR-2: {personaje2.Datos.Nombre} SALUD: {personaje2.Caracteristicas.Salud}, (ATAQUE: {personaje2.Caracteristicas.Ataque}, ARMADURA: {personaje2.Caracteristicas.Armadura})");

                    int opcionElegida = menuCombateJcJ.InicializarMenuCombate(9);
                    esquiva = random.Next(1,100); //algo con este que no me gusta
                    factorAleatorioAtaque = (double)random.Next(80,100)/100;

                    if(nroJugador == 1)
                    {
                        switch(opcionElegida)
                        {
                            case 1: //Atacar
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                System.Console.WriteLine("\t\tJUGADOR 1 ATACA JUGADOR 2...");
                                damageCausado =   (personaje1.Caracteristicas.Ataque * MULTIPLICADOR_DAÑO_BASE - personaje2.Caracteristicas.Armadura/4 )*factorAleatorioAtaque;
                                if(damageCausado <= 0.00)
                                {
                                    System.Console.WriteLine($"\t\tEL ATAQUE DEL JUGADOR {nroJugador} ES NULO...");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    System.Console.WriteLine($"\t\tJUGADOR 2 NO RECIBE DAÑO");
                                    Console.ResetColor();
                                }else if(esquiva %3 == 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    System.Console.WriteLine($"\t\tJUGADOR 2 LOGRO ESQUIVAR...");
                                    Console.ResetColor();
                                }
                                else
                                {
                                    personaje2.Caracteristicas.Salud -= damageCausado;
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    System.Console.WriteLine($"\t\tJUGADOR 1 CAUSA {Math.Round(damageCausado, 2)} DE DAÑO...");
                                    Console.ResetColor();
                                }
                                break;
                            case 2: //Curar

                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                System.Console.WriteLine("\t\tJUGADOR 1 SE CURA...");
                                if(personaje1.Caracteristicas.Salud < 100 && personaje1.Caracteristicas.Salud >= 80)
                                {
                                    personaje1.Caracteristicas.Salud +=5;
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    if(personaje1.Caracteristicas.Salud > 100)
                                    {
                                        personaje1.Caracteristicas.Salud = 100;
                                    }
                                    System.Console.WriteLine($"\t\tJUGADOR 1 AUMENTA SU SALUD EN 5HP");
                                    Console.ResetColor();
                                }
                                else if(personaje1.Caracteristicas.Salud < 80 && personaje1.Caracteristicas.Salud >=50){
                                    personaje1.Caracteristicas.Salud +=10;
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    System.Console.WriteLine($"\t\tJUGADOR 1 AUMENTA SU SALUD EN 10HP");
                                    Console.ResetColor();
                                }else if(personaje1.Caracteristicas.Salud < 50)
                                {
                                     personaje1.Caracteristicas.Salud +=20;
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    System.Console.WriteLine($"\t\tJUGADOR 1 AUMENTA SU SALUD EN 20HP");
                                    Console.ResetColor();
                                }
                                break;
                            case 3: //Huir

                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                System.Console.WriteLine("\t\tJUGADOR 1 HUYE... JUGADOR 2 GANA LA PELEA");
                                huye1 = true;
                                break;
                        }
                    }else if(nroJugador == 2)
                    {
                        switch(opcionElegida)
                        {
                            case 1: //Atacar

                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                System.Console.WriteLine("\t\tJUGADOR 2 ATACA JUGADOR 1...");
                                damageCausado =   (personaje2.Caracteristicas.Ataque * MULTIPLICADOR_DAÑO_BASE - personaje1.Caracteristicas.Armadura/4)*factorAleatorioAtaque;
                                if(damageCausado <= 0)
                                {
                                    System.Console.WriteLine($"\t\tEL ATAQUE DEL JUGADOR {nroJugador} ES NULO...");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    System.Console.WriteLine($"\t\tJUGADOR 1 NO RECIBE DAÑO");
                                    Console.ResetColor();
                                }
                                else if(esquiva %3 == 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    System.Console.WriteLine($"\t\tJUGADOR 1 LOGRO ESQUIVAR...");
                                    Console.ResetColor();
                                }
                                else
                                {
                                    personaje1.Caracteristicas.Salud -= damageCausado;
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    System.Console.WriteLine($"\t\tJUGADOR 2 CAUSA {Math.Round(damageCausado, 2)} DE DAÑO...");
                                    Console.ResetColor();
                                }
                                break;
                            case 2: //Curar

                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                System.Console.WriteLine("\t\tJUGADOR 2 SE CURA...");
                                if(personaje2.Caracteristicas.Salud < 100 && personaje2.Caracteristicas.Salud >= 80)
                                {
                                    personaje2.Caracteristicas.Salud +=5;
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    if(personaje2.Caracteristicas.Salud > 100)
                                    {
                                        personaje2.Caracteristicas.Salud = 100;
                                    }
                                    System.Console.WriteLine($"\t\tJUGADOR 2 AUMENTA SU SALUD EN 5HP");
                                    Console.ResetColor();
                                }else if(personaje2.Caracteristicas.Salud < 80 && personaje2.Caracteristicas.Salud >=50){
                                    personaje2.Caracteristicas.Salud +=10;
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    System.Console.WriteLine($"\t\tJUGADOR 2 AUMENTA SU SALUD EN 10HP");
                                    Console.ResetColor();
                                }else if(personaje2.Caracteristicas.Salud < 50)
                                {
                                     personaje2.Caracteristicas.Salud +=20;
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    System.Console.WriteLine($"\t\tJUGADOR 2 AUMENTA SU SALUD EN 20HP");
                                    Console.ResetColor();
                                }
                                break;
                            case 3: //Huir

                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                System.Console.WriteLine("\t\tJUGADOR 2 HUYE... JUGADOR 1 GANA LA PELEA");
                                huye2 = true;
                                break;
                        }
                    }

                    if(nroJugador == 1)
                    {
                        nroJugador = 2;
                    } else
                    {
                        nroJugador = 1;
                    }
                    personaje1.Caracteristicas.Salud = Math.Round(personaje1.Caracteristicas.Salud,2);
                    personaje2.Caracteristicas.Salud = Math.Round(personaje2.Caracteristicas.Salud,2);

                    Console.ResetColor();
                }while(personaje1.Caracteristicas.Salud > 0 && personaje2.Caracteristicas.Salud > 0 && !huye1 && !huye2 );

                if(personaje1.Caracteristicas.Salud <= 0 || huye1)
                {
                    // Console.Clear();
                    // Textos.ImprimirTextoArray(ImagenesGameplay.GameOver);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Textos.textoCentrado("🏆  🏆  -------->JUGADOR 2 GANA LA PELEA!<--------  🏆  🏆");
                    Console.ResetColor();
                }else if(personaje2.Caracteristicas.Salud <= 0 || huye2)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Textos.textoCentrado("🏆  🏆  -------->JUGADOR 1 GANA LA PELEA!<--------  🏆  🏆");
                    Console.ResetColor();
                }
                
            }
            
        }
    }
}