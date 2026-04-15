namespace Actividad_2_csharp.tablesSQL;

public class servicios
{
    public int id { get; set; }
    public string origen { get; set; }
    public string destino { get; set; }
    public double distancia { get; set; }
    public string estado { get; set; }
    public double costo_total  { get; set; } 
    public int? id_conductor { get; set; }
    public int? id_vehiculo { get; set; }
}
