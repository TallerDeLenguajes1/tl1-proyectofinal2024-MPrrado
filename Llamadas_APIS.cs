namespace APIS
{
    using System.Text.Json.Serialization;
    using System.Text.Json;

    public class NombresPersonajes 
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("names")]
        public List<string> Names { get; set; }
    }


    public static class API
    {
        public static async Task<NombresPersonajes> Deserializar()
        {
            HttpClient client = new HttpClient();
            var url = "https://names.ironarachne.com/race/dragonborn/male/10";
            HttpResponseMessage respuesta = await client.GetAsync(url);
            respuesta.EnsureSuccessStatusCode();
            string responseBody = await respuesta.Content.ReadAsStringAsync();
            // List<Root> listclima = JsonSerializer.Deserialize<List<Root>>(responseBody); //solo para el caso en el que la API nos devuelva una lista
            return JsonSerializer.Deserialize<NombresPersonajes>(responseBody);//en este caso la API nos devuelve un objeto con los datos relevantes del bitcoin
        }
        public static void GuardarEnJson(NombresPersonajes listadoAPI)
        {
            string directorioActual = Directory.GetCurrentDirectory();
            string carpeta = Path.Combine(directorioActual, "Json_src");
            string archivoJson = Path.Combine(carpeta, "NombresPersonajes.json");

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