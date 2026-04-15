using Actividad_2_csharp.database;

namespace Actividad_2_csharp.services;

public class reportes_servicio
{
     MysqlDbContext db =  new MysqlDbContext();
     
    public void reportes()
    {
        Console.WriteLine("======================================");
        Console.WriteLine("        REPORTE DE SERVICIOS");
        Console.WriteLine("======================================\n");

        // Total
        var total = db.servicios.Count();
        Console.WriteLine($"Total: {total}");

        // Por estado
        var estados = db.servicios
            .GroupBy(s => s.estado)
            .Select(g => new
            {
                estado = g.Key,
                total = g.Count(),
                promedio = g.Average(x => x.costo_total),
                distancia = g.Sum(x => x.distancia)
            })
            .ToList();

        Console.WriteLine("------ RESUMEN POR ESTADO ------\n");
        foreach (var e in estados)
        {
            Console.WriteLine("======================================");
            Console.WriteLine($"Estado: {e.estado}");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"Cantidad de servicios : {e.total}");
            Console.WriteLine($"Promedio costo       : {e.promedio}");
            Console.WriteLine($"Distancia total      : {e.distancia}");
            Console.WriteLine("======================================\n");
        }
        Console.WriteLine("------ LISTADO DE SERVICIOS ------\n");

        var lista = db.servicios.ToList();

        foreach (var s in lista)
        {
            Console.WriteLine($"[{s.estado.ToUpper()}] {s.origen} → {s.destino} | ${s.costo_total}");
        }

        Console.WriteLine("\n======================================");
        Console.WriteLine("       FIN DEL REPORTE");
        Console.WriteLine("======================================\n");
        Console.WriteLine();
        
    }
}