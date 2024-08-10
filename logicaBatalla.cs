using Personajes_Y_Estadisticas;
using Gui;
using Microsoft.VisualBasic;
using System.Threading.Tasks.Dataflow;
using System.Runtime.CompilerServices;
using PersistenciaDelJuego;
using System.Linq.Expressions;
using System.Diagnostics;
using Constantes;
namespace LogicaBatalla
{

    public static class Batalla
    {

        public static Personaje SeleccionPersonaje(int nroJugador, List<Personaje> personajes, int cantidadPersonajes)
        {

            Personaje personajeElegido = null;
            string[] titulo = { $"------->  ⚔️  ⚔️  SELECCIONAR PERSONAJE JUGADOR {nroJugador}  ⚔️  ⚔️  <-------" };
            string[] personajesSeleccionables = new string[cantidadPersonajes + 1]; ;
            for (int i = 0; i < cantidadPersonajes; i++)
            {
                personajesSeleccionables[i] = personajes[i].Datos.Nombre + " " + personajes[i].Datos.Tipo + $"(💪 {personajes[i].Caracteristicas.Ataque} 🛡️  {personajes[i].Caracteristicas.Armadura})";
            }
            personajesSeleccionables[cantidadPersonajes] = "Volver al menú principal";
            Menu menuSeleccionPersonaje = new Menu(titulo, personajesSeleccionables);
            int IndicePersonajeElegido = menuSeleccionPersonaje.InicializarMenuStandar(12);
            if (IndicePersonajeElegido != cantidadPersonajes)
            {
                personajeElegido = personajes[IndicePersonajeElegido];
            }
            else if (IndicePersonajeElegido == cantidadPersonajes)
            {
                return null;
            }
            return personajeElegido;
        }
        public static void Combate(Personaje personaje1, Personaje personaje2, int formaCombate, string escenarioDePelea)
        {
            // Implementación del combate

            //incializamos las variables necesarias
            Console.Clear();
            string nroJugador = "1";
            string[] opcionesMenuCombate = [];
            if (formaCombate == 1)
            {
                opcionesMenuCombate = ["⚔️ ATACAR", "❤️‍🩹 CURAR", "💨 HUIR"];
            }
            else if (formaCombate == 2)
            {
                opcionesMenuCombate = ["⚔️ ATACAR", "❤️‍🩹 CURAR", "💨 HUIR", "💾 SALIR Y GUARDAR"];
            }
            Menu menuCombate = new Menu(opcionesMenuCombate);
            bool huye1 = false;
            bool huye2 = false;
            bool guardado = false;
            bool primeraVuelta = false;
            int esquiva;
            double factorAleatorioAtaque;
            Random random = new Random();
            Console.ResetColor();
            Textos.ImprimirTextoArray(ImagenesGameplay.Worms);

            if (formaCombate == 1) //formaCombate = 1 => combate JcJ
            {
                do
                {

                    PresentarDatosCombate(personaje1, personaje2, escenarioDePelea, out esquiva, out factorAleatorioAtaque, random, formaCombate);
                    int opcionElegida = menuCombate.InicializarMenuCombate(opcionesMenuCombate.Length, nroJugador);

                    if (nroJugador == "1")
                    {
                        EjecutarAccionPelea(personaje1, personaje2, ref nroJugador, ref huye1, ref huye2, ConstData.MULTIPLICADOR_DAMAGE_BASE, esquiva, factorAleatorioAtaque, opcionElegida, ref guardado, formaCombate);
                    }
                    else if (nroJugador == "2")
                    {
                        EjecutarAccionPelea(personaje2, personaje1, ref nroJugador, ref huye1, ref huye2, ConstData.MULTIPLICADOR_DAMAGE_BASE, esquiva, factorAleatorioAtaque, opcionElegida, ref guardado, formaCombate);
                    }
                    Console.ResetColor();
                } while (personaje1.Caracteristicas.Salud > 0 && personaje2.Caracteristicas.Salud > 0 && !huye1 && !huye2 && !guardado);
            }
            else if (formaCombate == 2) //formaCombate = 2 => JcCPU
            {
                do
                {
                    if (!huye1 && !huye2 && !primeraVuelta)
                    {
                        Persistencia.GuardarPartida(personaje1, personaje2, escenarioDePelea);
                        primeraVuelta = true;
                    }

                    PresentarDatosCombate(personaje1, personaje2, escenarioDePelea, out esquiva, out factorAleatorioAtaque, random, formaCombate);

                    if (nroJugador == "1")
                    {
                        int opcionElegida = menuCombate.InicializarMenuCombate(opcionesMenuCombate.Length, nroJugador);
                        EjecutarAccionPelea(personaje1, personaje2, ref nroJugador, ref huye1, ref huye2, ConstData.MULTIPLICADOR_DAMAGE_BASE, esquiva, factorAleatorioAtaque, opcionElegida, ref guardado, formaCombate);
                    }
                    else if (nroJugador == "CPU")
                    {
                        int huyeCPU = random.Next(1, 101);
                        int opcionCPU;
                        if (huyeCPU % ConstData.ENTERO_DERTERMINAR_HUYE_CPU == 0)
                        {
                            opcionCPU = 3;
                        }
                        else
                        {
                            opcionCPU = ObtenerOpcionCpu();
                        }
                        EjecutarAccionPelea(personaje2, personaje1, ref nroJugador, ref huye1, ref huye2, ConstData.MULTIPLICADOR_DAMAGE_BASE, esquiva, factorAleatorioAtaque, opcionCPU, ref guardado, formaCombate);
                    }

                    if (!huye1 && !huye2 && personaje1.Caracteristicas.Salud > 0 && personaje2.Caracteristicas.Salud > 0)
                    {
                        Persistencia.GuardarPartida(personaje1, personaje2, escenarioDePelea);
                    }
                    Console.ResetColor();

                } while (personaje1.Caracteristicas.Salud > 0 && personaje2.Caracteristicas.Salud > 0 && !huye1 && !huye2 && !guardado);
            }
            DeterminarGanador(personaje1, personaje2, huye1, huye2, formaCombate, guardado);
            if (!guardado && formaCombate == 2)
            {
                Persistencia.EliminarDatosPeleaTerminada();
                personaje1.Caracteristicas.Salud = 100;
                personaje2.Caracteristicas.Salud = 100;
            }
        }
        static void PresentarDatosCombate(Personaje personaje1, Personaje personaje2, string escenarioDePelea, out int esquiva, out double factorAleatorioAtaque, Random random, int formaCombate)
        {
            System.Console.WriteLine();
            System.Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Textos.textoCentrado($"--------ESCENARIO DE PELEA: {escenarioDePelea}--------");
            bool condicion = formaCombate == 2 ? true : false;
            Console.ForegroundColor = ConsoleColor.Magenta;
            System.Console.WriteLine();
            System.Console.WriteLine();

            personaje1.Caracteristicas.Salud = Math.Round(personaje1.Caracteristicas.Salud, 2);
            personaje2.Caracteristicas.Salud = Math.Round(personaje2.Caracteristicas.Salud, 2);
            Console.ResetColor();
            Console.WriteLine($"\tJUGADOR 1: {personaje1.Datos.Nombre} (  ❤️ :{personaje1.Caracteristicas.Salud}   🗡️ :{personaje1.Caracteristicas.Ataque}   🪖  :{personaje1.Caracteristicas.Armadura}) ---------- {(condicion ? "CPU" : $"JUGADOR 2")}: {personaje2.Datos.Nombre} (  ❤️ :{personaje2.Caracteristicas.Salud}  🗡️ :{personaje2.Caracteristicas.Ataque}   🪖 :{personaje2.Caracteristicas.Armadura})");
            System.Console.WriteLine();
            esquiva = random.Next(1, 101);
            factorAleatorioAtaque = (double)random.Next(80, 101) / 100;
        }

        static void EjecutarAccionPelea(Personaje personajeAtacante, Personaje personajeDefensor, ref string nroJugador, ref bool huye1, ref bool huye2, int MULTIPLICADOR_DAÑO_BASE, int esquiva, double factorAleatorioAtaque, int opcionElegida, ref bool guardado, int formaCombate) //genero una fucion local para encapsular el comportamiento a ejecutarse segun la accion elegida por el jugador para la batalla
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
                        Thread.Sleep(ConstData.DELAY2000);
                    }
                    else if (nroJugador == "2" || nroJugador == "CPU")
                    {
                        huye2 = Huir(nroJugador);
                        Thread.Sleep(ConstData.DELAY2000);
                    }
                    break;
                case 4:
                    guardado = GuardarSalir(guardado);
                    break;
            }
            switch (formaCombate)
            {
                case 1:
                    if (opcionElegida != 4)
                    {
                        if (nroJugador == "1")
                        {
                            nroJugador = "2";
                        }
                        else if (nroJugador == "2")
                        {
                            nroJugador = "1";
                        }
                    }
                    break;
                case 2:
                    if (opcionElegida != 4)
                    {
                        if (nroJugador == "1")
                        {
                            nroJugador = "CPU";
                        }
                        else if (nroJugador == "CPU")
                        {
                            nroJugador = "1";
                        }
                    }
                    break;
            }

        }
        static bool GuardarSalir(bool guardado)
        {
            string[] tituloMenuGuardado = { "SEGURO DESEA SALIR Y GUARDAR?" };
            string[] opcionesMenuGuardado = { "SI", "NO" };
            Menu menuGuardado = new Menu(tituloMenuGuardado, opcionesMenuGuardado);
            int opcion = menuGuardado.InicializarMenuStandar(10) + 1;
            if (opcion == 1)
            {
                guardado = true;
                Console.ForegroundColor = ConsoleColor.Cyan;
                System.Console.WriteLine($"\t\tGUARDANDO LA PARTIDA");
                Thread.Sleep(ConstData.DELAY2000);
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine($"\t\tSaliendo del combate...");
                System.Console.WriteLine($"\t\tRegresando al Menú pricipal...");
                System.Console.WriteLine();
                Console.ResetColor();
                Thread.Sleep(ConstData.DELAY2000);
            }
            Console.Clear();
            return guardado;
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

        static void DeterminarGanador(Personaje personaje1, Personaje personaje2, bool huye1, bool huye2, int formaCombate, bool guardado)
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
            if(!guardado) 
            {
                Thread.Sleep(ConstData.DELAY2000);
                Console.ForegroundColor = ConsoleColor.Green;
                Textos.textoCentrado("-------Regresando al Menú Princial-------");
                Thread.Sleep(ConstData.DELAY2000);
            }
            Console.ResetColor();

        }

        static int ObtenerOpcionCpu()
        {
            int[] arreglo = new int[100];
            Random random = new Random();

            for (int i = 0; i < arreglo.Length; i++)
            {
                // Generamos un número aleatorio entre 1 y 3 (ambos inclusive)
                int valor = random.Next(1, 3);
                arreglo[i] = valor;
            }

            // Ahora puedes acceder a cualquier componente del arreglo para obtener un valor aleatorio
            int indiceAleatorio = random.Next(arreglo.Length);
            int valorAleatorio = arreglo[indiceAleatorio];
            return valorAleatorio;
        }
    }
}
