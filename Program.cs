
using Personajes_Y_Estadisticas;
using Gui;
using APIS;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;
using System.Runtime.CompilerServices;
using LogicaBatalla;
//CONSTANTES
const int CANTIDAD_PERSONAJES_CREAR = 10;

Console.OutputEncoding = Encoding.Unicode;  // para caracteres UTF-16 (caracteres especiales)

//INSTANCIACIONES
NombresPersonajes listaNombresPersonajes = new NombresPersonajes();
listaNombresPersonajes = await API.Deserializar(); //obtengo de la API mi objeto con la lista de nombres
FabricaDePersonajes FabricaDePersonajes = new FabricaDePersonajes(listaNombresPersonajes.Names); // instanceo la clase de fabrica para poder operar
List<Personaje> personajes = new List<Personaje>();

var tituloMenuPrincipal = Textos.TituloPrincipal;
string[] opcionesMenuPrincipal = {"JcJ","JcCPU", "CONTINUAR PARTIDA", "SALIR DEL JUEGO"};
Menu menuPrincipal = new Menu(tituloMenuPrincipal, opcionesMenuPrincipal);
//TITULO
Console.Clear();
int opcionElegida = menuPrincipal.InicializarMenuStandar(12,0);



for (int i = 0; i < CANTIDAD_PERSONAJES_CREAR; i++)
{
   personajes.Add(FabricaDePersonajes.CrearPersonaje(i));  
}
if(opcionElegida == 1)
{
    Console.Clear();
    Personaje jugador1 = Batalla.SeleccionPersonaje(1, personajes, CANTIDAD_PERSONAJES_CREAR);
    Personaje jugador2 = Batalla.SeleccionPersonaje(2, personajes, CANTIDAD_PERSONAJES_CREAR);
    Batalla.Combate(jugador1,jugador2,0);
}

// API.GuardarEnJson(listaNombresPersonajes);
Console.ResetColor();
System.Console.WriteLine("presiona enter para salir....");
Console.ReadKey(true);

