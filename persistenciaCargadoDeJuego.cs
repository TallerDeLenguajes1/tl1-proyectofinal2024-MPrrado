using Personajes_Y_Estadisticas;
using System.Text.Json;
using LogicaBatalla;
using APIS;
using Gui;
using Constantes;
namespace PersistenciaDelJuego
{
    public static class Persistencia
    {
        private static string directorioActual = Directory.GetCurrentDirectory(); //obtengo el directorio actual, de esta forma donde sea que se clone el repo se podra ejecutar este metodo correctamente
        private static string[] tituloMenuCargaPartida = { "ESTÁ A PUNTO DE CARGAR UNA PARTIDA QUE FUE GUARDADA", "SI NO SE ENCUENTRAN ARCHIVOS SALDRÁ UN MENSAJE DE ERROR", "DESEA CONTINUAR?" ,"**NOTA** Si es la primera vez en el juego no existiran datos de una partida. Tambien solo es posible guardar la partida estando en el modo JcCPU"};
        private static string[] opcionesMenuCargaPartida = { "SI", "NO" };


        public static void GuardarPartida(Personaje personaje1, Personaje personaje2, string escenario)
        {

            //combino la ruta con el nombre que tendra la carpeta
            string carpeta = Path.Combine(directorioActual, ConstData.NOMBRE_CARPETA_RECURSOS_JSON);
            //determino el nombre con el que se guardara el archivo .json
            string archivoJsonPersonaje1 = Path.Combine(carpeta, ConstData.NOMBRE_ARCHIVO_PERSONAJE_1);
            string archivoJsonPersonaje2 = Path.Combine(carpeta, ConstData.NOMBRE_ARCHIVO_PERSONAJE_2);
            string archivoJsonEscenario = Path.Combine(carpeta, ConstData.NOMBRE_ARCHIVO_ESCENARIO);

            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            if (!File.Exists(archivoJsonPersonaje1))
            {
                using (File.Create(archivoJsonPersonaje1)) { } // Crear el archivo vacío de "datosPersonaje1.json"
            }

            if (!File.Exists(archivoJsonPersonaje2))
            {
                using (File.Create(archivoJsonPersonaje2)) { } // Crear el archivo vacío de "datosPersonaje2.json"
            }

            if (!File.Exists(archivoJsonEscenario))
            {
                using (File.Create(archivoJsonEscenario)) { } // Crear el archivo vacío de "datosEscenario.json"
            }

            string estadoPersonaje1 = JsonSerializer.Serialize(personaje1);
            string estadoPersonaje2 = JsonSerializer.Serialize(personaje2);
            File.WriteAllText(archivoJsonPersonaje1, estadoPersonaje1); //sobreescribe lo que tenga en el archivo
            File.WriteAllText(archivoJsonPersonaje2, estadoPersonaje2);
            File.WriteAllText(archivoJsonEscenario, escenario);
        }

        public static void CargarPartida()
        {
            // Implementación para cargar la partida desde un archivo
            string carpetaArchivos = Path.Combine(directorioActual, ConstData.NOMBRE_CARPETA_RECURSOS_JSON); /// obtengo ruta de la carpeta json_src para comprobar la existencia de los archivos de una partida guardada
            string archivo1 = Path.Combine(carpetaArchivos, ConstData.NOMBRE_ARCHIVO_PERSONAJE_1);
            string archivo2 = Path.Combine(carpetaArchivos, ConstData.NOMBRE_ARCHIVO_PERSONAJE_2);
            string archivoEscenario = Path.Combine(carpetaArchivos, ConstData.NOMBRE_ARCHIVO_ESCENARIO);
            Menu MenuCargarPartida = new Menu(tituloMenuCargaPartida, opcionesMenuCargaPartida);
            int opcionElegida = MenuCargarPartida.InicializarMenuStandar(9) + 1;
            if (opcionElegida == 1)
            {
                if (!File.Exists(archivo1) || !File.Exists(archivo2) || !File.Exists(archivoEscenario))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("ERROR NO HAY DATOS DE UNA PARTIDA GUARDADOS");
                    System.Console.WriteLine("Regresando al Menú Principal...");
                    Console.ResetColor();
                    Thread.Sleep(ConstData.DELAY2000);
                }
                else
                {
                    string datosPersonaje1 = File.ReadAllText(archivo1);
                    string datosPersonaje2 = File.ReadAllText(archivo2);
                    string datosEscenario = File.ReadAllText(archivoEscenario);
                    Personaje personaje1 = JsonSerializer.Deserialize<Personaje>(datosPersonaje1);
                    Personaje personaje2 = JsonSerializer.Deserialize<Personaje>(datosPersonaje2);
                    Batalla.Combate(personaje1, personaje2, ConstData.FORMA_COMBATE_PARTIDA_CARGADA, datosEscenario,2,1);

                }
            }
            else if (opcionElegida == 2)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine("Regresando al Menú Principal");
                Console.ResetColor();
                Thread.Sleep(ConstData.DELAY2000);
            }
        }

        public static void EliminarDatosPeleaTerminada()
        {
            // Implementación para eliminar los archivos de datos de una partida terminada
            string carpetaArchivos = Path.Combine(directorioActual, ConstData.NOMBRE_CARPETA_RECURSOS_JSON);
            string archivo1 = Path.Combine(carpetaArchivos, ConstData.NOMBRE_ARCHIVO_PERSONAJE_1);
            string archivo2 = Path.Combine(carpetaArchivos, ConstData.NOMBRE_ARCHIVO_PERSONAJE_2);
            string archivoEscenario = Path.Combine(carpetaArchivos, ConstData.NOMBRE_ARCHIVO_ESCENARIO);
            if (File.Exists(archivo1) && File.Exists(archivo2) && File.Exists(archivoEscenario))
            {
                File.Delete(archivo1);
                File.Delete(archivo2);
                File.Delete(archivoEscenario);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("ERROR NO HAY DATOS DE UNA PARTIDA TERMINADA GUARDADOS");
                Thread.Sleep(ConstData.DELAY2000);
                Console.ResetColor();
            }
        }

        public static void CargarDatosInicioBackup(ref NombresApi listadoNombresPersonajes, ref NombresApi listadoNombresEscenarios)
        {
                
                string carpetaArchivos = Path.Combine(directorioActual, ConstData.NOMBRE_CARPETA_RECURSOS_JSON);
                //obtengo la ruta de los archivos de la ultima sesion
                string archivoPersonajesBackup = Path.Combine(carpetaArchivos, ConstData.NOMBRE_ARCHIVO_PERSONAJES_BACKUP);
                string archivoEscenariosBackup = Path.Combine(carpetaArchivos, ConstData.NOMBRE_ARCHIVO_ESCENARIOS_BACKUP);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Textos.textoCentrado($"El juego se ejecutara con los datos de backup");
                Console.ResetColor();
                Thread.Sleep(ConstData.DELAY2000);
                //si los archivos existen, cargo los datos a la lista de nombres
                if (File.Exists(archivoPersonajesBackup) && File.Exists(archivoEscenariosBackup))
                {
                    string datosPersonajesBackup = File.ReadAllText(archivoPersonajesBackup);
                    string datosEscenariosBackup = File.ReadAllText(archivoEscenariosBackup);
                    listadoNombresPersonajes = JsonSerializer.Deserialize<NombresApi>(datosPersonajesBackup);
                    listadoNombresEscenarios = JsonSerializer.Deserialize<NombresApi>(datosEscenariosBackup);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Textos.textoCentrado("DATOS CARGADOS CON EXITO!!");
                    Thread.Sleep(ConstData.DELAY2000);
                }
                else //Si no existen muestro mensaje de erro y el juego no se podrá ejecutar
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Textos.textoCentrado("ERROR!!! LOS DATOS DEL BACKUP NO ESTAN DISPONIBLES");
                    Textos.textoCentrado("LAMENTABLEMENTE NO SE PODRA EJECUTAR EL JUEGO");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(ConstData.DELAY2000);
                    Environment.Exit(1);
                }
        }
    }
}