using Actividad_2_csharp.tablesSQL;
using Microsoft.EntityFrameworkCore;

namespace Actividad_2_csharp.database;

public class MysqlDbContext : DbContext
{
    public DbSet<conductores>conductores{ get; set; }
    public DbSet<servicios>servicios{ get; set; }
    public DbSet<vehiculos>vehiculos{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseMySql(
                "server=204.168.214.142;database=operaciones_trasporte;user=root;password=NgY95q3hmmiGMjjjmao",
                ServerVersion.AutoDetect(
                    "server=204.168.214.142;database=operaciones_trasporte;user=root;password=NgY95q3hmmiGMjjjmao")
            );
    
}
