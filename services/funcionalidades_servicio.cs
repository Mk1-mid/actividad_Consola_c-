using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using Actividad_2_csharp.database;
using Actividad_2_csharp.tablesSQL;
using Microsoft.EntityFrameworkCore;

namespace Actividad_2_csharp.services;

public class funcionalidades_servicio
{
    MysqlDbContext db = new MysqlDbContext();
    
    // metodo para iniciar un servicio.
    public void inicarServicio()
    {
        Console.WriteLine("===== servicios pendientes =======");
        Console.WriteLine("espera un segundo, cargando ......");

        var servicios_pendientes = db.servicios.Where(s => s.estado == "pendiente").ToList();

        if (servicios_pendientes.Count == 0)
        {
            Console.WriteLine("no hay servicios pendientes.");
            return;
        }
        foreach (var servicio in servicios_pendientes)
        {
            Console.WriteLine($" ID: {servicio.id}, {servicio.origen} --> {servicio.destino} DISTANCIA: {servicio.distancia}KM. ESTADO: {servicio.estado}");
        }

        Console.WriteLine();
        Console.WriteLine("selecciona el ID del servicio que quieres iniciar");
        int opcionId;
        while (true)
        {
            if (!int.TryParse(Console.ReadLine(), out  opcionId))
            {
                Console.WriteLine("ingrese datos numericos");
                continue;
            }
            break;
        }
        
        var servicioElegido = servicios_pendientes.FirstOrDefault(s=> s.id == opcionId);

        if (servicioElegido == null)
        {
            Console.WriteLine("no se encontro el ID.");
            return;
        }

        servicioElegido.estado = "activo";
        
        db.SaveChanges();

        Console.WriteLine("servicio ha sido iniciado.");

    }
    
    // metodo para finalizar el servicio

    public void finalizarServicio()
    {
        Console.WriteLine("===== servicios disponibles =====");
        Console.WriteLine("espera un segundo, cargando ......");
        
        var serviciosDisponibles = db.servicios.Where(s => s.estado == "pendiente" || s.estado == "activo").ToList();

        if (serviciosDisponibles.Count == 0)
        {
            Console.WriteLine("no hay  servicio.");
            return;
        }

        foreach (var servicio in serviciosDisponibles)
        {
            Console.WriteLine($" ID: {servicio.id}, {servicio.origen} --> {servicio.destino} DISTANCIA: {servicio.distancia}KM. ESTADO: {servicio.estado}");
        }

        Console.WriteLine("ingresa el ID del servicio a finalizar.");
        int opcionId;
        while (true)
        {
            if (!int.TryParse(Console.ReadLine(), out opcionId))
            {
                Console.WriteLine("ingresa solo digitos numericos.");
                continue;
            }
            break;
        }
        var servicioFinalizar = db.servicios.FirstOrDefault(s => s.id == opcionId);

        if (servicioFinalizar == null)
        {
            Console.WriteLine("no se encontro el servicio a finalizar.");
            return;
        }
        
        servicioFinalizar.estado = "inactivo";
        db.SaveChanges();
        Console.WriteLine("el servicio a sido finalizado.");
        
    }
    
    
    // consultar servicios

    public void ConsultarServicio()
    {
        Console.WriteLine("===== servicios =====");
        Console.WriteLine("espera un segundo, cargando ......");
        
        var servicios = db.servicios.ToList();
        if (servicios.Count == 0)
        {
            Console.WriteLine("no se encontro el servicio.");
            return;
        }
        Console.WriteLine("TODOS LOS SERVICIOS:");
        foreach (var servicio in servicios)
        {
            Console.WriteLine($" ID: {servicio.id}, {servicio.origen} --> {servicio.destino} DISTANCIA: {servicio.distancia}KM. ESTADO: {servicio.estado}\n");
        }
        Console.WriteLine();
        
    }
    
    // metodo para consultar vehiculos y conductores
    public void ConsultarConductoresVehiculos()
    {
        bool flag1 = true;
        while (flag1)
        {
            Console.WriteLine(" ==== consultar ====" +
                              "1.) conductores" +
                              "2.) vehiculos" +
                              "3). salir al menu principal\n");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.WriteLine("=====  Conductores =====");
                    Console.WriteLine("espera un segundo, cargando ......");
        
                    var conductores = db.conductores.ToList();
                    if (conductores.Count == 0)
                    {
                        Console.WriteLine("no se encontro el conducto.");
                        return;
                    }

                    Console.WriteLine("TODOS LOS    CONDUCTORES:");
                    foreach (var cond in  conductores)
                    {
                        Console.WriteLine($" ID :{cond.id} NUMERO_IDENTIFICACION : {cond.numero_identificacion} NOMBRE: {cond.nombre_completo}" +
                                          $"LICENCIA: {cond.licencia} ");
                    }
                    break;
                case "2":
                    Console.WriteLine("=====  vehiculos =====");
                    Console.WriteLine("espera un segundo, cargando ......");
        
                    var vehiculos = db.vehiculos.ToList();
                    if (vehiculos.Count == 0)
                    {
                        Console.WriteLine("no se encontraron  vehiculos.");
                        return;
                    }

                    Console.WriteLine("TODOS LOS    VEHICULOS:");
                    foreach (var vehiculo in  vehiculos)
                    {
                        Console.WriteLine($"ID: {vehiculo.id} PLACA: {vehiculo.placa} TIPO_VEHICULO: {vehiculo.tipo_vehiculo}" +
                                          $"CAPACIDADA: {vehiculo.capacidad}. ");
                        Console.WriteLine();
                    }
                    break;
                case "3":
                    Console.WriteLine(" === saliendo ====");
                    flag1 = false;
                    break;
                default:
                    Console.WriteLine("error, vuelve a intentarlo.");
                    break;
            }
        }
    }
    
}

