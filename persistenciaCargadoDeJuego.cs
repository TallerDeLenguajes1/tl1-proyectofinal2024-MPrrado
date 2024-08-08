using Personajes_Y_Estadisticas;
using System.Text.Json;
namespace PersistenciaDelJuego
{
    public static class Persistencia
    {
        public static void GuardarPartida(Personaje personaje, string nombreArchivo) 
        {
            // Implementación para guardar la partida en un archivo
             //obtengo el directorio actual, de esta forma donde sea que se clone el repo se podra ejecutar este metodo correctamente
            string directorioActual = Directory.GetCurrentDirectory(); 
            //combino la ruta con el nombre que tendra la carpeta
            string carpeta = Path.Combine(directorioActual, "Json_src");
            //determino el nombre con el que se guardara el archivo .json
            string archivoJson = Path.Combine(carpeta, nombreArchivo);

            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            if (!File.Exists(archivoJson))
            {
                using (File.Create(archivoJson)) { } // Crear el archivo vacío
            }
            
            string estadoPersonaje = JsonSerializer.Serialize(personaje);
            File.WriteAllText(archivoJson, estadoPersonaje); // Reemplazar el contenido existente
        }        
    }
}