
using Actividad_2_csharp.database;
using Actividad_2_csharp.services;

var reportes = new reportes_servicio();
var funcionalidades = new funcionalidades_servicio();
var conexionMongosb = new conexionMongodb();
var asignar_servicio = new asignar_servicio();  
var servicio_registros =  new servicio_registros();
var verificar_conexion = new VerificarConexion();

verificar_conexion.VerificacionConexion();
conexionMongosb.conectarMongo();

Console.WriteLine("Bienvenidos al Sistema de Gestión de Operaciones de Transporte !!.");
Console.WriteLine();

bool flag = true;

while (flag)
{
    Console.WriteLine("--- Sistema de Gestión de Transporte ---\n" +
                      "\n 1). Registrar conductor\n" +
                      " 2). Registrar vehículo\n" +
                      " 3). Registrar servicio de transporte\n" +
                      " 4). Asignar conductor y vehículo a servicio\n" +
                      " 5). Iniciar servicio\n" +
                      " 6). Finalizar servicio\n" +
                      " 7). Consultar servicios\n" +
                      " 8). Consultar conductores y vehículos\n" +
                      " 9). Reportes operativos\n" +
                      " 10). Salir\n");

    Console.WriteLine("selecciona una opcion: ");
    string opcion =  Console.ReadLine();

    switch (opcion)
    {
        case "1":
            servicio_registros.registro_conductor();
            break;
        case "2":
            servicio_registros.registro_vehiculo();
            break;
        case "3":
            servicio_registros.registro_servicio();
            break;
        case "4":
            asignar_servicio.servicio();
            break;
        case "5":
            funcionalidades.inicarServicio();
            break;
        case "6":
            funcionalidades.finalizarServicio();
            break;
        case "7":
            funcionalidades.ConsultarServicio();
            break;
        case "8":
            funcionalidades.ConsultarConductoresVehiculos();
            break;
        case "9":
            reportes.reportes();
            break;
        case "10":
            flag = false;
            Console.WriteLine("cerrando programa.");
            break;
        default:
            Console.WriteLine("error, opcion invalida vuelve a intentarlo.");
            break;
    }
    
}
