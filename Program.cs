﻿
using Personajes_Y_Estadisticas;
using Gui;
using APIS;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;

//CONSTANTES
const int CANTIDAD_PERSONAJES_CREAR = 10;

Console.OutputEncoding = Encoding.Unicode;  // For UTF-16 show chinesse chars
//INSTANCIACIONES
NombresPersonajes listaNombresPersonajes = new NombresPersonajes();
// listaNombresPersonajes = await API.Deserializar(); //obtengo de la API mi objeto con la lista de nombres
FabricaDePersonajes FabricaDePersonajes = new FabricaDePersonajes(listaNombresPersonajes.Names); // instanceo la clase de fabrica para poder operar
Personaje[] personajes = new Personaje[CANTIDAD_PERSONAJES_CREAR];

//TITULO
Console.Clear();
Textos.ImprimirTitulos(Gui.Textos.TituloPrincipal);
Menu.MenuPrincipal();
Textos.ImprimirTitulos(Gui.Textos.GenerandoPersonajes);

//GENERACION DE PERSONAJES
// System.Console.WriteLine();
// System.Console.WriteLine();
// Console.ForegroundColor = ConsoleColor.Red;
// Console.WriteLine(Textos.GenerandoPersonajes);
// System.Console.WriteLine();
// System.Console.WriteLine();

// for (int i = 0; i < CANTIDAD_PERSONAJES_CREAR; i++)
// {
//    personajes[i] = FabricaDePersonajes.CrearPersonaje();
// }

// // MUESTRA PERSONAJES PARA CORROBORAR
// int k=1;
// foreach (Personaje personaje in personajes)
// {
//     Console.ForegroundColor = ConsoleColor.Blue;
//     System.Console.WriteLine($"----PERSONAJE [{k}]----");
//     k++;
//     Console.ForegroundColor = ConsoleColor.Green;
//     System.Console.WriteLine($"\t---->{personaje.Datos.Nombre}----");
//     System.Console.WriteLine($"\t---->{personaje.Datos.Tipo}----");
//     Console.ForegroundColor = ConsoleColor.Yellow;
//     System.Console.WriteLine($"\t----Caracteristicas----");
//     System.Console.WriteLine($"\t---->Salud: {personaje.Caracteristicas.Salud}----");
//     System.Console.WriteLine($"\t---->Armadura: {personaje.Caracteristicas.Armadura}----");
//     System.Console.WriteLine($"\t---->Ataque: {personaje.Caracteristicas.Ataque}----");
//     System.Console.WriteLine();
//     System.Console.WriteLine();
// }
// Console.ForegroundColor = ConsoleColor.White;
// API.GuardarEnJson(listaNombresPersonajes);
Console.ReadKey();

