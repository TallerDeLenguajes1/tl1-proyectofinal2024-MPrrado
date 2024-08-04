using System.Collections.Generic;
using System.Net.NetworkInformation;
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
            @"                                                                   |_|    "];
        private static string[] generandoPersonajes = [
            @" __  ___     ___ __          __  __     __  ___ __  __  __              ___ __  ",
            @"/ _`|__ |\ ||__ |__) /\ |\ ||  \/  \   |__)|__ |__)/__`/  \|\ | /\    ||__ /__` ",
            @"\__>|___| \||___|  \/~~\| \||__/\__/   |   |___|  \.__/\__/| \|/~~\\__/|___.__/ "
        ];
        public static string[] TituloPrincipal { get => tituloPrincipal; }
        public static string[] GenerandoPersonajes { get => generandoPersonajes; }

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
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public static void textoCentrado(string texto)
        {
            var anchoTerminal = Console.WindowWidth; //obtengo el ancho de la terminal
            string textoCentrado = new string(' ', (anchoTerminal - texto.Length) / 2) + texto; //creo mi variable la cual sera el texto centrado
            System.Console.WriteLine(textoCentrado);
        }
    }

    public static class Menu
    {
        public static int MenuPrincipal()
        {
            Textos.textoCentrado("MENÚ\n");
            (int left, int top) = Console.GetCursorPosition(); //obtenemos la posicion del cursor actual, notacion par recibir dos valores
            var opcion = 1;
            var color = " 🐛 \u001b[32m";
            ConsoleKeyInfo key; //creamos esta variable la cual guarda informacio de la tecla presionada
            bool selecciona = false;

            while (!selecciona)
            {
                Console.SetCursorPosition(left, top);
                Textos.textoCentrado($"{(opcion == 1 ? color:"")}Option 1\u001b[0m");
                Textos.textoCentrado($"{(opcion == 2 ? color:"")}Option 2\u001b[0m");
                Textos.textoCentrado($"{(opcion == 3 ? color:"")}Option 3\u001b[0m");

                key = Console.ReadKey(false); //se limpia el buffer de las teclas presionadas para evitar conflicto

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        opcion = opcion == 1 ? 3 : opcion - 1;
                        break;

                    case ConsoleKey.DownArrow:
                        opcion = opcion == 3 ? 1 : opcion + 1;
                        break;

                    case ConsoleKey.Enter:
                        selecciona = true;
                        break;
                }
            }
            Textos.textoCentrado($"{color} La opcion elegida es: {opcion}\u001b[0m");
            return opcion;
        }
    }

}
