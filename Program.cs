
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

//MIS VARIABLES
Random random = new Random();


//TITULO
Console.Clear();
int opcionElegida = menuPrincipal.InicializarMenuStandar(12)+1;



for (int i = 0; i < CANTIDAD_PERSONAJES_CREAR; i++)
{
   personajes.Add(FabricaDePersonajes.CrearPersonaje(i));  
}

Console.Clear();
Personaje jugador1;
Personaje jugador2;
switch(opcionElegida)
{
    case 1:
        jugador1 = Batalla.SeleccionPersonaje(1, personajes, CANTIDAD_PERSONAJES_CREAR);
        jugador2 = Batalla.SeleccionPersonaje(2, personajes, CANTIDAD_PERSONAJES_CREAR);
        IniciarPelea(jugador1, jugador2, opcionElegida);
        break;
    case 2:
        jugador1 = Batalla.SeleccionPersonaje(1, personajes, CANTIDAD_PERSONAJES_CREAR);
        jugador2 = personajes[random.Next(0,CANTIDAD_PERSONAJES_CREAR)];
        IniciarPelea(jugador1, jugador2, opcionElegida);
        break;
}



// API.GuardarEnJson(listaNombresPersonajes);
Console.ResetColor();
System.Console.WriteLine("presiona enter para salir....");
Console.ReadKey(true);

//FUNCIONES

static void IniciarPelea(Personaje jugador1, Personaje jugador2, int tipoCombate)
{
    if (jugador1 == jugador2)
    {
        Personaje jugador2Nuevo = new Personaje(jugador2.Datos.Tipo, jugador2.Datos.Nombre, jugador2.Caracteristicas.Ataque, jugador2.Caracteristicas.Armadura, jugador2.Caracteristicas.Salud);
        Batalla.Combate(jugador1, jugador2Nuevo, tipoCombate);
    }
    else
    {
        Batalla.Combate(jugador1, jugador2, tipoCombate);
    }
}