using Personajes_Y_Estadisticas;
using Gui;
using APIS;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;
using System.Runtime.CompilerServices;
using LogicaBatalla;
using System.Net.Http.Headers;
using PersistenciaDelJuego;
using Constantes;

Console.OutputEncoding = Encoding.Unicode;  // para caracteres UTF-16 (caracteres especiales)


//INSTANCIACIONES
List<Personaje> personajes = new List<Personaje>();
var tituloMenuPrincipal = Textos.TituloPrincipal;
string[] opcionesMenuPrincipal = { "JcJ", "JcCPU", "MODO TORNEO", "CONTINUAR PARTIDA", "SALIR DEL JUEGO" };
Menu menuPrincipal = new Menu(tituloMenuPrincipal, opcionesMenuPrincipal);
string[] opcionesMenuInicio = { "ONLINE MODE", "OFFLINE MODE" };
string[] tituloMenuInicio = { "---<>---<>---<> SELECCIONE EL MODO DE JUEGO <>---<>---<---" };
Menu menuInicio = new Menu(tituloMenuInicio, opcionesMenuInicio);

//MIS VARIABLES
Random random = new Random();
NombresApi listaNombresEscenarios = null;
NombresApi listaNombresPersonajes = null;

//INICIALIZACION DEL JUEGO, CARGA DE RECURSOS (nota no encapsulo el codigo por tema de la asincronia del metodo)
int opcionInicio = menuInicio.InicializarMenuStandar(10) + 1;
if (opcionInicio == 1)
{
    try
    {
        Textos.textoCentrado($"Conectando con los servicios....");
        Thread.Sleep(ConstData.DELAY1500);
        Textos.textoCentrado($"Reclutando informacion del mundo gusano..");
        Thread.Sleep(ConstData.DELAY1500);
        listaNombresPersonajes = await API.Deserializar(ConstData.API_NOMBRES_PERSONAJES); //obtengo de la API mi objeto con la lista de nombres
        listaNombresEscenarios = await API.Deserializar(ConstData.API_NOMBRES_CAMPO_DE_BATALLA); //obtengo de la API mi objeto con la lista de nombres
        API.GuardarEnJson(listaNombresPersonajes, ConstData.NOMBRE_ARCHIVO_PERSONAJES_BACKUP);
        API.GuardarEnJson(listaNombresEscenarios, ConstData.NOMBRE_ARCHIVO_ESCENARIOS_BACKUP);
    }
    catch (Exception error)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Textos.textoCentrado($"Error al conectar con la API: {error.Message}");
        Thread.Sleep(ConstData.DELAY3000);
        Persistencia.CargarDatosInicioBackup(ref listaNombresPersonajes, ref listaNombresEscenarios);

    }
}
else if (opcionInicio == 2)
{
    Persistencia.CargarDatosInicioBackup(ref listaNombresPersonajes, ref listaNombresEscenarios);
    Console.ResetColor();
}
FabricaDePersonajes fabricaDePersonajes = new FabricaDePersonajes(listaNombresPersonajes.Names); // instanceo la clase de fabrica para poder operar

Console.Clear();
Console.ResetColor();
int opcionElegida;
do
{
    opcionElegida = menuPrincipal.InicializarMenuStandar(12) + 1;
    Console.Clear();

    //Ejecutamos la accion correspondiente segun la opcion elegida en el menu
    switch (opcionElegida)
    {
        case 1:
            JugadosContraJugador(listaNombresPersonajes, listaNombresEscenarios, fabricaDePersonajes, personajes, opcionElegida);
            break;
        case 2:
            JugadorContraCPU(listaNombresPersonajes, listaNombresEscenarios, fabricaDePersonajes, personajes, random, opcionElegida);
            break;
        case 3:
            Torneos(personajes, random, fabricaDePersonajes, listaNombresEscenarios, listaNombresPersonajes, opcionElegida);
            break;
        case 4:
            Persistencia.CargarPartida();
            break;
        case 5:
            SalirDelJuego();
            break;
    }
} while (opcionElegida != 5);


//FUNCIONES

static bool IniciarPelea(Personaje jugador1, Personaje jugador2, int tipoCombate, string escenarioDePelea, int opcionElegida, int cantidadRondasTorneo)
{
    bool resultado;
    if (jugador1 == jugador2)
    {
        Personaje jugador2Nuevo = new Personaje(jugador2.Datos.Tipo, jugador2.Datos.Nombre, jugador2.Caracteristicas.Ataque, jugador2.Caracteristicas.Armadura, jugador2.Caracteristicas.Salud);
        resultado = Batalla.Combate(jugador1, jugador2Nuevo, tipoCombate, escenarioDePelea, opcionElegida, cantidadRondasTorneo);
    }
    else
    {
        resultado = Batalla.Combate(jugador1, jugador2, tipoCombate, escenarioDePelea, opcionElegida, cantidadRondasTorneo);
    }
    return resultado;
}

static string ObtenerEscenario(NombresApi listaEscenarios)
{
    Random randomEscenarios = new Random();
    int indiceEscenarios = randomEscenarios.Next(0, listaEscenarios.Names.Count);
    return listaEscenarios.Names[indiceEscenarios];
}

static void GenerarPersonajes(NombresApi listaNombresPersonajes, FabricaDePersonajes FabricaDePersonajes, List<Personaje> personajes)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Textos.TextoCentradoArray(Textos.GenerandoPersonajes);
    Console.ResetColor();
    for (int i = 0; i < listaNombresPersonajes.Count; i++)
    {
        personajes.Add(FabricaDePersonajes.CrearPersonaje(i));
    }
    Thread.Sleep(1500);
}

static void JugadosContraJugador(NombresApi listaNombresPersonajes, NombresApi listaNombresEscenarios, FabricaDePersonajes FabricaDePersonajes, List<Personaje> personajes, int opcionElegida)
{
    GenerarPersonajes(listaNombresPersonajes, FabricaDePersonajes, personajes);
    Personaje jugador1 = Batalla.SeleccionPersonaje(1, personajes, listaNombresPersonajes.Count);
    if (jugador1 != null)
    {
        Personaje jugador2 = Batalla.SeleccionPersonaje(2, personajes, listaNombresPersonajes.Count);
        if (jugador2 != null)
        {
            Console.ResetColor();
            IniciarPelea(jugador1, jugador2, opcionElegida, ObtenerEscenario(listaNombresEscenarios), opcionElegida, 1); // utilizacion de la palabra ref para pasar por referencia un valor, logrando asi la modificacion del valor del mismo dentro de una funcion 
        }
    }
}

static void JugadorContraCPU(NombresApi listaNombresPersonajes, NombresApi listaNombresEscenarios, FabricaDePersonajes FabricaDePersonajes, List<Personaje> personajes, Random random, int opcionElegida)
{
    GenerarPersonajes(listaNombresPersonajes, FabricaDePersonajes, personajes);
    Personaje jugador1 = Batalla.SeleccionPersonaje(1, personajes, listaNombresPersonajes.Count);
    if (jugador1 != null)
    {
        Personaje jugador2 = personajes[random.Next(0, listaNombresPersonajes.Count)];
        if (jugador2 != null)
        {
            Console.ResetColor();
            IniciarPelea(jugador1, jugador2, opcionElegida, ObtenerEscenario(listaNombresEscenarios), opcionElegida, 1);
        }
    }
}

static void SalirDelJuego()
{
    Console.ForegroundColor = ConsoleColor.White;
    Textos.TextoCentradoArray(ImagenesGameplay.ImagenDespedida);
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Textos.TextoCentradoArray(ConstData.MENSAJE_DESPEDIDA);
    Console.ForegroundColor = ConsoleColor.White;
    Thread.Sleep(ConstData.DELAY3000);
    Console.Clear();
}

static void IniciarTorneo(List<Personaje> personajes, FabricaDePersonajes fabricaDePersonajes, Random random, NombresApi listaNombresEscenarios, NombresApi listaNombresPersonajes, int rondasTorneo, int opcionElegida)
{
    string nombrePersonajeAnterior = "";
    GenerarPersonajes(listaNombresPersonajes, fabricaDePersonajes, personajes);
    Personaje jugador1 = Batalla.SeleccionPersonaje(1, personajes, listaNombresPersonajes.Count);
    Personaje jugador2;
    do
    {
        jugador2 = personajes[random.Next(0, listaNombresPersonajes.Count)];

    } while (nombrePersonajeAnterior == jugador2.Datos.Nombre);
    int rondas = rondasTorneo;
    for (int i = 0; i < rondasTorneo; i++)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Clear();
        Textos.textoCentrado("-----------------------------------------------------------------------------");
        Textos.textoCentrado($"RONDA :{i + 1}");
        Thread.Sleep(ConstData.DELAY2000);
        Console.ResetColor();
        if (IniciarPelea(jugador1, jugador2, 2, ObtenerEscenario(listaNombresEscenarios), opcionElegida, rondas))
        {
            break;
        }
        else
        {

            rondas--;
        }
    }
}

static void Torneos(List<Personaje> personajes, Random random, FabricaDePersonajes fabricaDePersonajes, NombresApi listaNombresEscenarios, NombresApi listaNombresPersonajes, int opcionElegida)
{
    Console.Clear();
    string[] tituloMenuTorneo = { "----------<MENÚ TORNEO>----------" };
    string[] OpcionesTorneo = { ">>>4 RONDAS<<<", ">>>6 RONDAS<<<", ">>>8 RONDAS<<<" };
    Menu menuTorneo = new Menu(tituloMenuTorneo, OpcionesTorneo);
    int opcionTorneo = menuTorneo.InicializarMenuStandar(3) + 1;
    switch (opcionTorneo)
    {
        case 1:
            IniciarTorneo(personajes, fabricaDePersonajes, random, listaNombresEscenarios, listaNombresPersonajes, 2, opcionElegida);
            break;
        case 2:
            IniciarTorneo(personajes, fabricaDePersonajes, random, listaNombresEscenarios, listaNombresPersonajes, 6, opcionElegida);
            break;
        case 3:
            IniciarTorneo(personajes, fabricaDePersonajes, random, listaNombresEscenarios, listaNombresPersonajes, 8, opcionElegida);
            break;
    }
}