
using Personajes_Y_Estadisticas;
using Gui;
using APIS;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;
using System.Runtime.CompilerServices;
using LogicaBatalla;
using System.Net.Http.Headers;

Console.OutputEncoding = Encoding.Unicode;  // para caracteres UTF-16 (caracteres especiales)

//CONSTANTES
const int API_NOMBRES_PERSONAJES = 1;
const int API_NOMBRES_CAMPO_DE_BATALLA = 2;


//INSTANCIACIONES
NombresApi listaNombresPersonajes = new NombresApi();
NombresApi listaNombresEscenarios = new NombresApi();
listaNombresPersonajes = await API.Deserializar(API_NOMBRES_PERSONAJES); //obtengo de la API mi objeto con la lista de nombres
listaNombresEscenarios = await API.Deserializar(API_NOMBRES_CAMPO_DE_BATALLA); //obtengo de la API mi objeto con la lista de nombres
FabricaDePersonajes FabricaDePersonajes = new FabricaDePersonajes(listaNombresPersonajes.Names); // instanceo la clase de fabrica para poder operar
List<Personaje> personajes = new List<Personaje>();

var tituloMenuPrincipal = Textos.TituloPrincipal;
string[] opcionesMenuPrincipal = {"JcJ","JcCPU", "CONTINUAR PARTIDA", "SALIR DEL JUEGO"};
Menu menuPrincipal = new Menu(tituloMenuPrincipal, opcionesMenuPrincipal);

//MIS VARIABLES
Random random = new Random();
bool guardaPartida = false;

//TITULO
Console.Clear();
Console.ResetColor();
int opcionElegida = menuPrincipal.InicializarMenuStandar(12)+1;



for (int i = 0; i < listaNombresPersonajes.Count; i++)
{
   personajes.Add(FabricaDePersonajes.CrearPersonaje(i));  
}

Console.Clear();
Personaje jugador1;
Personaje jugador2;
switch(opcionElegida)
{
    case 1:
        jugador1 = Batalla.SeleccionPersonaje(1, personajes, listaNombresPersonajes.Count);
        jugador2 = Batalla.SeleccionPersonaje(2, personajes, listaNombresPersonajes.Count);
        IniciarPelea(jugador1, jugador2, opcionElegida,ObtenerEscenario(listaNombresEscenarios), ref guardaPartida); // utilizacion de la palabra ref para pasar por referencia un valor, logrando asi la modificacion del valor del mismo dentro de una funcion 
        break;
    case 2:
        jugador1 = Batalla.SeleccionPersonaje(1, personajes, listaNombresPersonajes.Count);
        jugador2 = personajes[random.Next(0,listaNombresPersonajes.Count)];
        IniciarPelea(jugador1, jugador2, opcionElegida,ObtenerEscenario(listaNombresEscenarios), ref guardaPartida);
        break;
}



// API.GuardarEnJson(listaNombresPersonajes);
Console.ResetColor();
System.Console.WriteLine("presiona enter para salir....");
Console.ReadKey(true);

//FUNCIONES

static void IniciarPelea(Personaje jugador1, Personaje jugador2, int tipoCombate, string escenarioDePelea, ref bool guardaPartida)
{
    if (jugador1 == jugador2)
    {
        Personaje jugador2Nuevo = new Personaje(jugador2.Datos.Tipo, jugador2.Datos.Nombre, jugador2.Caracteristicas.Ataque, jugador2.Caracteristicas.Armadura, jugador2.Caracteristicas.Salud);
        guardaPartida = Batalla.Combate(jugador1, jugador2Nuevo, tipoCombate,escenarioDePelea);
    }
    else
    {
        guardaPartida = Batalla.Combate(jugador1, jugador2, tipoCombate,escenarioDePelea); 
    }
}

static string ObtenerEscenario(NombresApi listaEscenarios)
{
    Random randomEscenarios = new Random();
    int indiceEscenarios = randomEscenarios.Next(0, listaEscenarios.Names.Count);
    return listaEscenarios.Names[indiceEscenarios];
}