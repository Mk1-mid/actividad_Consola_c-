
using Actividad_2_csharp.database;
using Actividad_2_csharp.tablesSQL;
using Microsoft.EntityFrameworkCore;

namespace Actividad_2_csharp.services;

public class asignar_servicio
{
    private MysqlDbContext db = new MysqlDbContext();

    public void servicio()
    {

        //  === buscar servicios disponibles. === 
        var service = db.servicios
            .Where(s => s.estado == "pendiente" && s.id_conductor == null
                                                && s.id_vehiculo == null).ToList();

        if (service.Count == 0)
        {
            Console.WriteLine("No se ha encontrado ningun servicio.");
            return;
        }

        Console.WriteLine("===== SERVICIOS DISPONIBLES =====");
        foreach (var se in service)
        {
            Console.WriteLine($" ID: {se.id}, {se.origen} --> {se.destino} DISTANCIA: {se.distancia}KM. ESTADO: {se.estado}");
        }

        int opcionId;
        while (true)
        {
            if (!int.TryParse(Console.ReadLine(), out opcionId))
            {
                Console.WriteLine("Error, ingresa un digito numerico ");
                continue;
            }

            break;
        }

        var servicio_encontrado = db.servicios.FirstOrDefault(s => s.id == opcionId);

        if (servicio_encontrado == null)
        {
            Console.WriteLine("servicio no encontrado");
            return;
        }

        if (servicio_encontrado.estado != "pendiente" || servicio_encontrado.id_conductor != null
                                                      || servicio_encontrado.id_vehiculo != null)
        {
            Console.WriteLine("Servicio no disponible para asignar.");
            return;
        }

        // mostrar vehiculos disponibles y asignar vehiculos

        var vehiculos = db.vehiculos.Where(v => v.estado == "disponible").ToList();

        if (vehiculos.Count == 0)
        {
            Console.WriteLine("No se ha encontrado ningun vehiculo disponible.");
            return;
        }
        Console.WriteLine("===== SERVICIOS DISPONIBLES =====\n");
        foreach (var vehiculo in vehiculos)
        {
            Console.WriteLine($"ID: {vehiculo.id} PLACA: {vehiculo.placa} TIPO_VEHICULO: {vehiculo.tipo_vehiculo}" +
                              $"CAPACIDADA: {vehiculo.capacidad}. ");
            Console.WriteLine();
        }

        int opcionVehivulo;
        while (true)
        {
            Console.WriteLine(" elige el id del vehiculo que deseas asignar: ");
            if (!int.TryParse(Console.ReadLine(), out opcionVehivulo))
            {
                Console.WriteLine("Error, ingresa un digito numerico ");
                continue;
            }
            break;
        }
        
        var vehiculoAsignado =  db.vehiculos.FirstOrDefault(v => v.id == opcionVehivulo);

        if (vehiculoAsignado == null)
        {
            Console.WriteLine("vehiculo no encontrado");
            return;
        }

        if (vehiculoAsignado.estado != "disponible")
        {
            Console.WriteLine("Vehiculo no disponible para asignar");
        }
        
        //   mostrar y asignar conductor

        var conductor = db.conductores
            .Where(c => c.estado == "disponible");

        if (conductor.Count() == 0)
        {
            Console.WriteLine("No se ha encontrado ningun conductor disponible.");
            return;
        }

        Console.WriteLine("===== CONDUCTORES DISPONIBLES =====\n");
        foreach (var cond in conductor)
        {
            Console.WriteLine($" ID :{cond.id} NUMERO_IDENTIFICACION : {cond.numero_identificacion} NOMBRE: {cond.nombre_completo}" +
                              $"LICENCIA: {cond.licencia} ");
        }

        Console.WriteLine();

        int opcionConductor;
        while (true)
        {
            Console.WriteLine("elige un id para asignar el conductor: ");
            if (!int.TryParse(Console.ReadLine(), out opcionConductor))
            {
                Console.WriteLine("Error, ingresa un digito numerico ");
                continue;
            }
            break;
        }
        var conductorAsignado = db.conductores.FirstOrDefault(c => c.id == opcionConductor);

        if (conductorAsignado == null)
        {
            Console.WriteLine("conductor no encontrado");
            return;
        }

        if (conductorAsignado.estado != "disponible")
        {
            Console.WriteLine("conductor no disponible para asignar");
            return;
        }
        // cambio el estado del conductor y el vehiculo
        
        conductorAsignado.estado = "servicio";
        vehiculoAsignado.estado = "servicio";
        
        // asigno conductor y vehiculo al servicio correspondiente
        
        servicio_encontrado.id_conductor = conductorAsignado.id;
        servicio_encontrado.id_vehiculo = vehiculoAsignado.id;

        db.SaveChanges();
    }
}