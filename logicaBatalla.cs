using Personajes_Y_Estadisticas;
using Gui;
using Microsoft.VisualBasic;
using System.Threading.Tasks.Dataflow;
namespace LogicaBatalla
{
    public static class Batalla
    {
        public static Personaje SeleccionPersonaje(int nroJugador, List<Personaje> personajes, int cantidadPersonajes)
        {
            Personaje elegido;
            string[] titulo = { $"------->  ⚔️  ⚔️  SELECCIONAR PERSONAJE JUGADOR {nroJugador}  ⚔️  ⚔️  <-------" };
            string[] personajesSeleccionables = new string[cantidadPersonajes]; ;
            for (int i = 0; i < cantidadPersonajes; i++)
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

            //incializamos las variables necesarias
            Console.Clear();
            string nroJugador = "1";
            string tituloMenu = "-----OPCIONES COMBATE-----";
            string[] opcionesMenu = { "ATACAR", "CURAR", "HUIR" };
            Menu menuCombateJcJ = new Menu(tituloMenu, opcionesMenu);
            bool huye1 = false;
            bool huye2 = false;
            const int MULTIPLICADOR_DAMAGE_BASE = 3;
            int esquiva;
            double factorAleatorioAtaque;
            Random random = new Random();

            Textos.ImprimirTextoArray(ImagenesGameplay.Worms);

            if (formaCombate == 1) //formaCombate = 1 => combate JcJ
            {
                do
                {
                    System.Console.WriteLine();
                    System.Console.WriteLine();
                    Textos.textoCentrado($"--------TURNO JUGADOR {nroJugador}--------");
                    System.Console.WriteLine();
                    System.Console.WriteLine();

                    personaje1.Caracteristicas.Salud = Math.Round(personaje1.Caracteristicas.Salud, 2);
                    personaje2.Caracteristicas.Salud = Math.Round(personaje2.Caracteristicas.Salud, 2);

                    Console.WriteLine($"\tJUGADOR-1: {personaje1.Datos.Nombre} (SALUD: {personaje1.Caracteristicas.Salud}, ATAQUE: {personaje1.Caracteristicas.Ataque}, ARMADURA: {personaje1.Caracteristicas.Armadura}) ---------- JUGADOR-2: {personaje2.Datos.Nombre} SALUD: {personaje2.Caracteristicas.Salud}, (ATAQUE: {personaje2.Caracteristicas.Ataque}, ARMADURA: {personaje2.Caracteristicas.Armadura})");

                    int opcionElegida = menuCombateJcJ.InicializarMenuCombate();
                    esquiva = random.Next(1, 101);
                    factorAleatorioAtaque = (double)random.Next(80, 101) / 100;

                    if (nroJugador == "1")
                    {
                        EjecutarAccionPelea(personaje1, personaje2, nroJugador, ref huye1, ref huye2, MULTIPLICADOR_DAMAGE_BASE, esquiva, factorAleatorioAtaque, opcionElegida);
                        DeterminarGanador(personaje1, personaje2, huye1, huye2, formaCombate);
                        nroJugador = "2";
                    }
                    else if (nroJugador == "2")
                    {
                        EjecutarAccionPelea(personaje2, personaje1, nroJugador, ref huye1, ref huye2, MULTIPLICADOR_DAMAGE_BASE, esquiva, factorAleatorioAtaque, opcionElegida);
                        DeterminarGanador(personaje1, personaje2, huye1, huye2, formaCombate);
                        nroJugador = "1";
                    }
                    Console.ResetColor();
                } while (personaje1.Caracteristicas.Salud > 0 && personaje2.Caracteristicas.Salud > 0 && !huye1 && !huye2);

            }
            else if (formaCombate == 2) //formaCombate = 2 => JcCPU
            {
                do
                {
                    System.Console.WriteLine();
                    System.Console.WriteLine();
                    Textos.textoCentrado($"--------TURNO JUGADOR {nroJugador}--------");
                    System.Console.WriteLine();
                    System.Console.WriteLine();

                    personaje1.Caracteristicas.Salud = Math.Round(personaje1.Caracteristicas.Salud, 2);
                    personaje2.Caracteristicas.Salud = Math.Round(personaje2.Caracteristicas.Salud, 2);

                    Console.WriteLine($"\tJUGADOR-1: {personaje1.Datos.Nombre} (SALUD: {personaje1.Caracteristicas.Salud}, ATAQUE: {personaje1.Caracteristicas.Ataque}, ARMADURA: {personaje1.Caracteristicas.Armadura}) ---------- CPU: {personaje2.Datos.Nombre} SALUD: {personaje2.Caracteristicas.Salud}, (ATAQUE: {personaje2.Caracteristicas.Ataque}, ARMADURA: {personaje2.Caracteristicas.Armadura})");

                    esquiva = random.Next(1, 101);
                    factorAleatorioAtaque = (double)random.Next(80, 101) / 100;

                    if (nroJugador == "1")
                    {
                        int opcionElegida = menuCombateJcJ.InicializarMenuCombate();
                        EjecutarAccionPelea(personaje1, personaje2, nroJugador, ref huye1, ref huye2, MULTIPLICADOR_DAMAGE_BASE, esquiva, factorAleatorioAtaque, opcionElegida);
                        DeterminarGanador(personaje1, personaje2, huye1, huye2, formaCombate);
                        nroJugador = "CPU";
                    }
                    else
                    {
                        EjecutarAccionPelea(personaje2, personaje1, nroJugador, ref huye1, ref huye2, MULTIPLICADOR_DAMAGE_BASE, esquiva, factorAleatorioAtaque, OpcionCpu());
                        DeterminarGanador(personaje1, personaje2, huye1, huye2, formaCombate);
                        nroJugador = "1";
                    }
                    Console.ResetColor();
                } while (personaje1.Caracteristicas.Salud > 0 && personaje2.Caracteristicas.Salud > 0 && !huye1 && !huye2);

            }

        }
        static void EjecutarAccionPelea(Personaje personajeAtacante, Personaje personajeDefensor, string nroJugador, ref bool huye1, ref bool huye2, int MULTIPLICADOR_DAÑO_BASE, int esquiva, double factorAleatorioAtaque, int opcionElegida) //genero una fucion local para encapsular el comportamiento a ejecutarse segun la accion elegida por el jugador para la batalla
        {

            switch (opcionElegida)
            {
                case 1: //Atacar
                    Atacar(personajeAtacante, personajeDefensor, nroJugador, MULTIPLICADOR_DAÑO_BASE, esquiva, factorAleatorioAtaque);
                    break;
                case 2: //Curar
                    Curar(personajeAtacante, nroJugador);
                    break;
                case 3: //Huir
                    if (nroJugador == "1")
                    {
                        huye1 = Huir(nroJugador);
                    }
                    else if (nroJugador == "2" || nroJugador == "CPU")
                    {
                        huye2 = Huir(nroJugador);
                    }
                    break;
            }
        }
        static void Atacar(Personaje personajeAtacante, Personaje personajeDefensor, string nroJugador, int MULTIPLICADOR_DAÑO_BASE, int esquiva, double factorAleatorioAtaque)
        {
            double damageCausado;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            System.Console.WriteLine($"\t\tJUGADOR {nroJugador} ATACA...");
            damageCausado = (personajeAtacante.Caracteristicas.Ataque * MULTIPLICADOR_DAÑO_BASE - personajeDefensor.Caracteristicas.Armadura / 4) * factorAleatorioAtaque;
            if (damageCausado <= 0)
            {
                System.Console.WriteLine($"\t\tEL ATAQUE DEL JUGADOR {nroJugador} ES NULO...");
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine($"\t\t SU OPONENTE NO RECIBE DAÑO");
            }
            else if (esquiva % 3 == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"\t\tSU OPONETE LOGRA ESQUIVAR...");
            }
            else
            {
                personajeDefensor.Caracteristicas.Salud -= damageCausado;
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"\t\tJUGADOR {nroJugador} CAUSA {Math.Round(damageCausado, 2)} DE DAÑO...");
            }
            Console.ResetColor();
        }

        static void Curar(Personaje personaje, string nroJugador)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            System.Console.WriteLine($"\t\tJUGADOR {nroJugador} SE CURA...");
            if (personaje.Caracteristicas.Salud < 100 && personaje.Caracteristicas.Salud >= 80)
            {
                personaje.Caracteristicas.Salud += 5;
                Console.ForegroundColor = ConsoleColor.Red;
                if (personaje.Caracteristicas.Salud > 100)
                {
                    personaje.Caracteristicas.Salud = 100;
                }
                System.Console.WriteLine($"\t\tJUGADOR {nroJugador} AUMENTA SU SALUD EN 5HP");
            }
            else if (personaje.Caracteristicas.Salud < 80 && personaje.Caracteristicas.Salud >= 50)
            {
                personaje.Caracteristicas.Salud += 10;
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"\t\tJUGADOR {nroJugador} AUMENTA SU SALUD EN 10HP");
            }
            else if (personaje.Caracteristicas.Salud < 50)
            {
                personaje.Caracteristicas.Salud += 20;
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"\t\tJUGADOR {nroJugador} AUMENTA SU SALUD EN 20HP");
            }
            Console.ResetColor();
        }

        static bool Huir(string nroJugador)
        {
            bool huye;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            System.Console.WriteLine($"\t\tJUGADOR {nroJugador} HUYE... SU OPONENTE GANA LA PELEA");
            huye = true;
            return huye;
        }

        static void DeterminarGanador(Personaje personaje1, Personaje personaje2, bool huye1, bool huye2, int formaCombate)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            if (huye2 || personaje2.Caracteristicas.Salud <= 0)
            {
                Textos.textoCentrado("🏆  🏆  -------->JUGADOR 1 GANA LA PELEA!<--------  🏆  🏆");
            }
            else if (huye1 || personaje1.Caracteristicas.Salud <= 0)
            {
                if (formaCombate == 1) //combate JcJ
                {
                    Textos.textoCentrado("🏆  🏆  -------->JUGADOR 2 GANA LA PELEA!<--------  🏆  🏆");
                }
                else if (formaCombate == 2) // combate JcCPU 
                {
                    Textos.textoCentrado("🏆  🏆  -------->JUGADOR CPU GANA LA PELEA!<--------  🏆  🏆");
                }
            }
            Console.ResetColor();

        }

        static int OpcionCpu()
        {
            int[] arreglo = new int[100];
            Random random = new Random();

            for (int i = 0; i < arreglo.Length; i++)
            {
                // Generamos un número aleatorio entre 1 y 3 (ambos inclusive)
                int valor = random.Next(1, 4);
                arreglo[i] = valor;
            }

            // Ahora puedes acceder a cualquier componente del arreglo para obtener un valor aleatorio
            int indiceAleatorio = random.Next(arreglo.Length);
            int valorAleatorio = arreglo[indiceAleatorio];
            return valorAleatorio;
        }
    }
}
