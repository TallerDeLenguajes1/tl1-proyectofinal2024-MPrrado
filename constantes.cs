namespace Constantes
{
    public static class ConstData
    {
        //CONSTANTES Program.cs
        public const int API_NOMBRES_PERSONAJES = 1;
        public const int API_NOMBRES_CAMPO_DE_BATALLA = 2;
        public static readonly string[] MENSAJE_DESPEDIDA = { "HASTA PRONTO!!! 👋 👋 👋", "GRACIAS POR JUGAR!!!", "(copyright® PRADO MATIAS SANTIAGO 2024)"}; //utilizo el readonly porque las constantes deben conocerce durante el tiempo de compilacion, por lo que si trato de hacer una constante del tipo arreglo hay fallo de compilacion porque para C# declarar un tipo como arreglo no es constante. Si bien la expresion de esta linea no es constante podriamos considerarlo pues es solo de lectura.

        //CONSTANTES llamadasAPIS.cs
        public const string NOMBRE_CARPETA_RECURSOS_JSON = "json_src";

        //CONSTANTES persistenciaCargadoDeJuego
        public const int FORMA_COMBATE_PARTIDA_CARGADA = 2;
        public const string NOMBRE_ARCHIVO_PERSONAJE_1 = "datosPersonaje1.json";
        public const string NOMBRE_ARCHIVO_PERSONAJE_2 = "datosPersonaje2.json";
        public const string NOMBRE_ARCHIVO_ESCENARIO = "datosEscenario.txt";
        public const string NOMBRE_ARCHIVO_ESCENARIOS_BACKUP = "datosEscenariosOffline.json";
        public const string NOMBRE_ARCHIVO_PERSONAJES_BACKUP = "datosPersonajesOffline.json";
        public static string[] tituloMenuCargaPartida = { "ESTÁ A PUNTO DE CARGAR UNA PARTIDA QUE FUE GUARDADA", "SI NO SE ENCUENTRAN ARCHIVOS SALDRÁ UN MENSAJE DE ERROR", "DESEA CONTINUAR?" };

        //CONSTANTES logicaBatalla.cs
        public const int MULTIPLICADOR_DAMAGE_BASE = 3;
        public const int ENTERO_DERTERMINAR_HUYE_CPU = 37;


        //CONSTANTES delay
        public const int DELAY1500 = 1500;
        public const int DELAY2000=2000;
        public const int DELAY3000=3000;
    }
}