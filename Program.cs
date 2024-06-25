
using Personajes_Y_Estadisticas;
using APIS;

//CONSTANTES
const int CANTIDAD_PERSONAJES_CREAR = 10;

//INSTANCIACIONES
OperacionPersonajes operar = new OperacionPersonajes();
Personaje[] personajes = new Personaje[CANTIDAD_PERSONAJES_CREAR];

System.Console.WriteLine();
System.Console.WriteLine();

Console.WriteLine("---------------Bienvenido al juego Worms World Sharp---------------");
for (int i = 0; i < CANTIDAD_PERSONAJES_CREAR; i++)
{
    personajes[i] = await operar.FabricaDePersonajes(i);
}

Console.ForegroundColor = ConsoleColor.Red;
System.Console.WriteLine();
System.Console.WriteLine();
System.Console.WriteLine("----------GENERANDO PERSONAJES----------");
System.Console.WriteLine();
System.Console.WriteLine();

//MUESTRA PERSONAJES PARA CORROBORAR
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
// Console.ReadKey();
