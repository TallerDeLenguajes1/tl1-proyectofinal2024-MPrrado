
using Personajes_Y_Estadisticas;
using ArteASCII;
using APIS;

//CONSTANTES
const int CANTIDAD_PERSONAJES_CREAR = 10;

//INSTANCIACIONES
NombresPersonajes listaNombresPersonajes = new NombresPersonajes();
listaNombresPersonajes = await API.Deserializar(); //obtengo de la API mi objeto con la lista de nombres
FabricaDePersonajes FabricaDePersonajes = new FabricaDePersonajes(listaNombresPersonajes.Names); // instanceo la clase de fabrica para poder operar
Personaje[] personajes = new Personaje[CANTIDAD_PERSONAJES_CREAR];

System.Console.WriteLine();
System.Console.WriteLine();
Console.BackgroundColor = ConsoleColor.Red;
Console.ForegroundColor = ConsoleColor.Black;
for(int i = 0 ; i <Titulos.TituloPrincipal.Length; i++)
{
    // Console.SetCursorPosition((Console.WindowWidth - Titulos.TituloPrincipal[i].Length) / 2, Console.CursorTop); //linea que nos sirve para centrar texto en la consola depende del ancho de la misma (o eso creo)
    Console.WriteLine(Titulos.TituloPrincipal[i]);
}
System.Console.WriteLine();
System.Console.WriteLine();
Console.BackgroundColor = ConsoleColor.Black;

//GENERACION DE PERSONAJES
System.Console.WriteLine();
System.Console.WriteLine();
Console.ForegroundColor = ConsoleColor.Red;
System.Console.WriteLine("----------GENERANDO PERSONAJES----------");
System.Console.WriteLine();
System.Console.WriteLine();

for (int i = 0; i < CANTIDAD_PERSONAJES_CREAR; i++)
{
   personajes[i] = FabricaDePersonajes.CrearPersonaje();
}

// MUESTRA PERSONAJES PARA CORROBORAR
int k=1;
foreach (Personaje personaje in personajes)
{
    Console.ForegroundColor = ConsoleColor.Blue;
    System.Console.WriteLine($"----PERSONAJE [{k}]----");
    k++;
    Console.ForegroundColor = ConsoleColor.Green;
    System.Console.WriteLine($"\t---->{personaje.Datos.Nombre}----");
    System.Console.WriteLine($"\t---->{personaje.Datos.Tipo}----");
    Console.ForegroundColor = ConsoleColor.Yellow;
    System.Console.WriteLine($"\t----Caracteristicas----");
    System.Console.WriteLine($"\t---->Salud: {personaje.Caracteristicas.Salud}----");
    System.Console.WriteLine($"\t---->Armadura: {personaje.Caracteristicas.Armadura}----");
    System.Console.WriteLine($"\t---->Ataque: {personaje.Caracteristicas.Ataque}----");
    System.Console.WriteLine();
    System.Console.WriteLine();
}
Console.ForegroundColor = ConsoleColor.White;
API.GuardarEnJson(listaNombresPersonajes);
// Console.ReadKey();
