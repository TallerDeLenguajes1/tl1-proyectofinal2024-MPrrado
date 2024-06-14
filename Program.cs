using System.Collections;
using System.Data;
using Personajes_Y_Estadisticas;
OperacionPersonajes operar = new OperacionPersonajes();
const int CANTIDAD_PERSONAJES_CREAR = 10;
Personaje[] personajes = new Personaje[CANTIDAD_PERSONAJES_CREAR];
System.Console.WriteLine();
System.Console.WriteLine();
Console.WriteLine("---------------Bienvenido al juego Worms World Sharp---------------");
for (int i = 0; i < CANTIDAD_PERSONAJES_CREAR; i++)
{
    personajes[i] = operar.FabricaDePersonajes(i);
}

Console.ForegroundColor = ConsoleColor.Red;
System.Console.WriteLine();
System.Console.WriteLine();
System.Console.WriteLine("----------PERSONAJES----------");
System.Console.WriteLine();
System.Console.WriteLine();
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
