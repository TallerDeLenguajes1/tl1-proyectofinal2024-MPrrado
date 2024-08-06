using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
namespace Gui
{
    public static class Textos
    {
        private static string[] tituloPrincipal = [
            @"      __          __                         _____ _                      ",
            @"      \ \        / /                        / ____| |                     ",
            @"       \ \  /\  / ___  _ __ _ __ ___  ___  | (___ | |__   __ _ _ __ _ __  ",
            @"        \ \/  \/ / _ \| '__| '_ ` _ \/ __|  \___ \| '_ \ / _` | '__| '_ \ ",
            @"         \  /\  | (_) | |  | | | | | \__ \  ____) | | | | (_| | |  | |_) |",
            @"          \/  \/ \___/|_|  |_| |_| |_|___/ |_____/|_| |_|\__,_|_|  | .__/ ",
            @"                                                                   | |    ",
            @"                                                                   |_|    ",
            @"MENU PRNCIPAL"];
        private static string[] generandoPersonajes = [
            @" __  ___     ___ __          __  __     __  ___ __  __  __              ___ __  ",
            @"/ _`|__ |\ ||__ |__) /\ |\ ||  \/  \   |__)|__ |__)/__`/  \|\ | /\    ||__ /__` ",
            @"\__>|___| \||___|  \/~~\| \||__/\__/   |   |___|  \.__/\__/| \|/~~\\__/|___.__/ "
        ];
        public static string[] TituloPrincipal { get => tituloPrincipal; }
        public static string[] GenerandoPersonajes { get => generandoPersonajes; }
        public static void seleccionarColorFuente(int color)
        {
            switch (color)
                    {
                        case 0:
                            Console.ForegroundColor = (ConsoleColor) color;
                            break;
                        case 1:
                            Console.ForegroundColor = (ConsoleColor) color;
                            break;
                        case 2:
                            Console.ForegroundColor = (ConsoleColor) color;
                            break;
                        case 3:
                            Console.ForegroundColor = (ConsoleColor) color;
                            break;
                        case 4:
                            Console.ForegroundColor = (ConsoleColor) color;
                            break;
                        case 5:
                            Console.ForegroundColor = (ConsoleColor) color;
                            break;
                        case 6:
                            Console.ForegroundColor = (ConsoleColor) color;
                            break;
                        case 7:
                            Console.ForegroundColor = (ConsoleColor) color;
                            break;
                        case 8:
                            Console.ForegroundColor = (ConsoleColor) color;
                            break;
                        case 9:
                            Console.ForegroundColor = (ConsoleColor) color;
                            break;
                        case 10:
                            Console.ForegroundColor = (ConsoleColor) color;
                            break;
                        case 11:
                            Console.ForegroundColor = (ConsoleColor) color;
                            break;
                        case 12:
                            Console.ForegroundColor = (ConsoleColor) color;
                            break;
                        case 13:
                            Console.ForegroundColor = (ConsoleColor) color;
                            break;
                        case 14:
                            Console.ForegroundColor = (ConsoleColor) color;
                            break;
                        case 15:
                            Console.ForegroundColor = (ConsoleColor) color;
                            break;
                    }
        }

        public static void ImprimirTitulos(string[] Texto)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            var anchoTerminal = Console.WindowWidth; //obtengo el ancho de la terminal
            string tituloCentrado = ""; //creo mi variable la cual sera el texto centrado
            foreach (string linea in Texto)
            {
                int padding = (anchoTerminal - linea.Length) / 2;
                tituloCentrado += new string(' ', padding) + linea + Environment.NewLine; //Enviroment.NewLine remplaza el uso de \r y \n y permite compatibilidad con distintos sistemas operativos

            }
            System.Console.WriteLine(tituloCentrado);
            System.Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void textoCentrado(string texto)
        {
            string textoCentrado; //creo mi variable la cual sera el texto centrado
            var anchoTerminal = Console.WindowWidth; //obtengo el ancho de la terminal
            textoCentrado = new string(' ', (anchoTerminal - texto.Length) / 2) + texto; //creo mi variable la cual sera el texto centrado
            System.Console.WriteLine(textoCentrado);
        }

        public static void TextoCentradoArray(string[] texto)
        {
            string textoCentrado = ""; //creo mi variable la cual sera el texto centrado
            var anchoTerminal = Console.WindowWidth; //obtengo el ancho de la terminal
            foreach (string linea in texto)
            {
                int padding = (anchoTerminal - linea.Length) / 2;
                textoCentrado += new string(' ', padding) + linea + Environment.NewLine; //Enviroment.NewLine remplaza el uso de \r y \n y permite compatibilidad con distintos sistemas operativos
            }
            System.Console.WriteLine(textoCentrado);
            System.Console.WriteLine();
        }

        public static void ImprimirTextoArray(string[] texto)
        {
            foreach(string linea in texto)
            {
                System.Console.WriteLine(linea);
            }
        }
    }

    public class Menu
    {
        private string[] tituloArray;
        private string titulo;
        private string[] opciones;
        private int indiceOpcion;
        public Menu(string[] tituloMenu, string[] opcionesMenu)
        {
            tituloArray = tituloMenu;
            opciones = opcionesMenu;
            indiceOpcion = 0;
        }
        public Menu(string tituloMenu, string[] opcionesMenu)
        {
            titulo = tituloMenu;
            opciones = opcionesMenu;
            indiceOpcion = 0;
        }
        public void MostrarOpcionesStandar(int colorTitulo)
        {
            Textos.seleccionarColorFuente(colorTitulo);
            Textos.TextoCentradoArray(tituloArray);
            Console.ResetColor();
            string decorador;
            for (int i = 0; i < opciones.Length; i++)
            {
                string opcionSeleccionada = opciones[i];
                if (i == indiceOpcion)
                {
                    decorador = "🐛 ";
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    decorador = " ";
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Textos.textoCentrado($"{decorador} <<{opcionSeleccionada}>>");

            }
            Console.ResetColor();
        }

        public int InicializarMenuStandar(int colorTitulo, int tipoMenu) //color del 0-15, justificacion: [0] centrado [1]: justify left
        {
            ConsoleKey tecla; //creamos nuestra variable del tipo ConsoleKey para poder almacenar y luego comparar con la tecla que se presiono
            Console.CursorVisible = false;
            do
            {
                if(tipoMenu == 0)
                {
                    Console.Clear();
                }
                MostrarOpcionesStandar(colorTitulo); //mostramos el menu

                ConsoleKeyInfo teclaPresionada = Console.ReadKey(true); //leemos la tecla presionada
                tecla = teclaPresionada.Key; //asiganmos a nuestra variable la tecla presionada

                if (tecla == ConsoleKey.UpArrow)
                {
                    indiceOpcion--;
                    if (indiceOpcion < 0)
                    {
                        indiceOpcion = opciones.Length - 1;
                    }
                }
                else
                {
                    indiceOpcion++;
                    if (indiceOpcion >= opciones.Length)
                    {
                        indiceOpcion = 0;
                    }
                }
            } while (tecla != ConsoleKey.Enter);
            Console.CursorVisible = true;
            return indiceOpcion;
        }

        public void MostrarOpcionesCombate(int colorTitulo)
        {
            Textos.seleccionarColorFuente(colorTitulo);
            Textos.textoCentrado(titulo);
            Console.ResetColor();
            string decorador=" ";
            int indiceOpcion;
            for (int i = 0; i < opciones.Length; i++)
            {
                indiceOpcion = i;
                switch(opciones[i])
                {
                    case "ATACAR":
                        decorador = "⚔️";
                        break;
                    case "DEFENDER":
                        decorador = "🛡️";
                        break;
                    case "HUIR":
                        decorador = "💨";
                        break;
                }
                Textos.textoCentrado($"[{indiceOpcion++}]]{decorador} <<{opciones[i]}>>");
            }
        }

        public int InicializarMenuCombate(int colorTitulo)
        {
            bool sale = false;
            int opcionElegida;
            do
            {
                MostrarOpcionesCombate(colorTitulo);
                System.Console.WriteLine("SELECCIONE SU ACCION: ");
                bool parseo = int.TryParse(Console.ReadLine(), out opcionElegida);
                if(!parseo || opcionElegida < 1 || opcionElegida >3)
                {
                    Textos.seleccionarColorFuente(12);
                    Textos.textoCentrado("ERROR INGRESE UNA OPCION VALIDA");
                    Console.ResetColor();
                }else
                {
                    sale = true;
                }
            } while(!sale);

            return opcionElegida;
        }
    }

    public static class ImagenesGameplay
    {
        private static string[] worms  =[
            @"              ▓▓▓▓▓▓▓▓▓▓▓▓                                                                        ▓▓▓▓▓▓▓▓▓▓▓▓          ",
            @"             ▓▓▒▒▒▒▒▒▒▒▒▒▒▓▓                                                                    ▓▓▒▒▒▒▒▒▒▒▒▒▒▓▓         ",
            @"            ▓▓▒▒▒▒▒▓███▒▓██▓▓                                                                  ▓▓██▓▒███▓▒▒▒▒▒▓▓        ",
            @"            ▓▓▒▒▒▒▒░░░░▒░░▒▓▓                                                                  ▓▓▒░░▒░  ░▒▒▒▒▒▓▓        ",
            @"            ▓▓▒▒▒▒░ ▒▓░░▒▓░▓▓                                                                   ▓░▓▒░░▓▒░░▒▒▒▒▓▓        ",
            @"            ▓▓▒▒▒▒░░██░░██▓▓                                                                    ▓▓██░░██░░▒▒▒▒▓▓        ",
            @"             ▓▓▒▒▒▒░░░░░▒▓▓                                                                      ▓▓▒░░░░░▒▒▒▒▓▓         ",
            @"              ▓▒▒▒▒▒░░▒░▓▓                                                                        ▓▓░▒░░▒▒▒▒▒▓          ",
            @"          ▓▓▓▓▓▒▒▒▒▒▒▒▒▒▓▓                                                                        ▓▓▒▒▒▒▒▒▒▒▒▓▓▓▓▓      ",
            @"         ▓▓▒▒▒▒▓▒▒▒▒▓▓▓▓▓                                                                          ▓▓▓▓▓▒▒▒▒▓▒▒▒▒▓▓     ",
            @"        ▓▓▒▒▒▒▒▒▒▒▒▒▒▒▓▓                                                                            ▓▓▒▒▒▒▒▒▒▒▒▒▒▒▓▓    ",
            @"      ▓▓▓▓▒▒▒▒▒▒▒▒▒▒▒▒▓▓                                                                            ▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓  ",
            @"     ▓▓▒▒▒▒▒▓▓▒▒▒▒▒▒▒▒▓▓                                                                            ▓▓▒▒▒▒▒▒▒▒▓▓▒▒▒▒▒▓▓ ",
            @"      ▓▓▒▒▒▓▓▓▓▓▓▒▒▒▓▓▓                                                                              ▓▓▓▒▒▒▓▓▓▓▓▓▒▒▒▓▓  "
            ];
        
        public static string[] Worms { get => worms; set => worms = value; }
    }
}
