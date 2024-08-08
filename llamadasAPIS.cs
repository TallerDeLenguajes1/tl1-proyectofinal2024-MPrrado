namespace APIS
{
    using System.Text.Json.Serialization;
    using System.Text.Json;

    public class NombresApi
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("names")]
        public List<string> Names { get; set; }
    }


    public static class API
    {
        public static async Task<NombresApi> Deserializar(int tipoApi)
        {
            HttpClient client = new HttpClient();
            Random random = new Random();
            string url= "";
            if(tipoApi == 1)
            {
                switch (random.Next(3)) //genero un valor aleatorio el cual me dara una url con distintos parametros, pues mi API es parametrizada
                {
                    case 0:
                        url = "https://names.ironarachne.com/race/dragonborn/male/15";
                        break;
                    case 1:
                        url = "https://names.ironarachne.com/race/elf/family/10";
                        break;
                    case 2:
                        url = "https://names.ironarachne.com/race/dwarf/female/20";
                        break;
                };
            }else if(tipoApi == 2)
            {
                url = "https://names.ironarachne.com/race/dwarf/town/10";
            }
            HttpResponseMessage respuesta = await client.GetAsync(url);
            respuesta.EnsureSuccessStatusCode();
            string responseBody = await respuesta.Content.ReadAsStringAsync();
            // List<Root> listclima = JsonSerializer.Deserialize<List<Root>>(responseBody); //solo para el caso en el que la API nos devuelva una lista. Esta linea es un ejemplo
            return JsonSerializer.Deserialize<NombresApi>(responseBody);//en este caso la API nos devuelve un objeto con los datos relevantes del bitcoin
        }
        public static void GuardarEnJson(NombresApi listadoAPI, string nombreArchivo)
        {
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

            string textoAPI = JsonSerializer.Serialize(listadoAPI);
            File.WriteAllText(archivoJson, textoAPI); // Reemplazar el contenido existente
        }
    }

}