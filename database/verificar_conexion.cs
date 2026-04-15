namespace Actividad_2_csharp.database;

public class VerificarConexion
{
    public void VerificacionConexion()
    {
        var db =  new MysqlDbContext();

        if (db.Database.CanConnect())
        {
            Console.WriteLine("Conectado");
        }
        else
        {
            Console.WriteLine("error, error.");
        
        }
    }
}