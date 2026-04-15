namespace Actividad_2_csharp.database;
using MongoDB.Driver;

public class conexionMongodb
{
    public void conectarMongo()
    {
        var connectionString = "mongodb://root:C7D6E25535C9B@204.168.214.142:27017";

        var client = new MongoClient(connectionString);

        var database = client.GetDatabase("reportes");

        Console.WriteLine("Conectado a MongoDB ");
    }
}