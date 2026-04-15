using Actividad_2_csharp.database;
using Actividad_2_csharp.tablesSQL;
using Microsoft.EntityFrameworkCore;



namespace Actividad_2_csharp.services;

public class servicio_registros
{
     DbContext db = new  MysqlDbContext();
     
     // primer metodo
     
    public void registro_conductor()
    {
        
        Console.WriteLine("Inicializando registro conductor");
        Console.WriteLine();
        bool flag = true;
        int identificacion;
        string nombre;
        string licencia;
        while (flag)
        {
            Console.WriteLine("Ingresa el numero de identificacion: ");
            if (!int.TryParse(Console.ReadLine(), out  identificacion))
            {
                Console.WriteLine("error, solo datos numericos");
                continue;
            }
            

            while (true)
            {
                Console.WriteLine("Ingresa el nombre completo del conductor: ");
                 nombre = Console.ReadLine().ToLower();
                 if ( string.IsNullOrEmpty(nombre))
                 {
                     Console.WriteLine("error, no dejes campos vacios. ");
                     continue;
                 }
                 break;
            }
            

            while (true)
            {
                Console.WriteLine("ingresa  la licencia del conductos C1), C2), C3)");
                 licencia = Console.ReadLine().ToUpper();
                if ( string.IsNullOrWhiteSpace(licencia))
                {
                    Console.WriteLine("error, no dejes campos vacios. ");
                    continue;
                }
                else if (licencia != "C1" && licencia != "C2" && licencia != "C3")
                {
                    Console.WriteLine("ingresa correctamente la liciencias permitidas.");
                    continue;
                }
                
                break;
                
            }
            var conductor = new conductores()
            {
                numero_identificacion = identificacion,
                nombre_completo = nombre,
                licencia = licencia,
                estado = "disponible"
                
            };

            db.Add(conductor);
            db.SaveChanges();
            Console.WriteLine("Registro agregado exitosamente");
            flag = false;
        }
        
    }
    
    // segundo metodo
     public void registro_vehiculo()
        {

            Console.WriteLine("Inicializando registro conductor");
            Console.WriteLine();
            bool flag = true;
            string placa;
            string tipo_vehiculo;
            int capacidad;
            

            while (flag)
            {

                while (true)
                {
                    Console.WriteLine("Ingresa la placa del vehiculo: ");
                    placa = Console.ReadLine().ToUpper();
                    if ( string.IsNullOrWhiteSpace(placa))
                    {
                        Console.WriteLine("error, no dejes campos vacios. ");
                        continue;
                    }

                    break;
                }

                while (true)
                {
                    Console.WriteLine("ingresa  el tipo de vehiculo:");
                    tipo_vehiculo = Console.ReadLine().ToLower();
                    if ( string.IsNullOrWhiteSpace(tipo_vehiculo))
                    {
                        Console.WriteLine("error, no dejes campos vacios. ");
                        continue;
                    }

                    break;

                }
                while (true)
                {
                    Console.WriteLine("Ingrese la capacidad del vehiculo: ");
                    if (!int.TryParse(Console.ReadLine(), out  capacidad))
                    {
                        Console.WriteLine("error, solo digitos numericos. ");
                        continue;
                    }

                    break;
                }

                var nuevoVehiculo = new vehiculos()
                {
                    placa = placa,
                    tipo_vehiculo = tipo_vehiculo,
                    capacidad = capacidad,
                    estado = "disponible"
                };

                db.Add(nuevoVehiculo);

                db.SaveChanges();
                Console.WriteLine("registro agregado exitosamente");
                flag = false;
            }
        }
     
     // tercer metodo.
     
      public void registro_servicio()
    {
        bool flag = true;

        double costo_kilometro = 4000;

        string origen;
        string destino;
        double distancia;
        double costo_total;

        Console.WriteLine("Inicializando el registro del viaje");
        Console.WriteLine();
        
    while (flag)
        {
            Console.WriteLine("donde empieza el viaje: ");
            origen = Console.ReadLine();
            if ( string.IsNullOrWhiteSpace(origen))
            {
                Console.WriteLine(" error, no dejes campos vacios.");
                continue;
            }

            while (true)
            {
                Console.WriteLine("ingresa el destino del viaje :");
                destino = Console.ReadLine();
                if ( string.IsNullOrWhiteSpace(destino))
                {
                    Console.WriteLine("error, no dejes campos vacios.");
                    continue;
                }
                break;
            }

            while (true)
            {
                Console.WriteLine("ingresa la distancia en kilometros (km): ");
                if (!double.TryParse(Console.ReadLine(), out distancia))
                {
                    Console.WriteLine("error, no dejes campos vacios.");
                    continue;
                }
                break;
            }
            
            costo_total = distancia * costo_kilometro;

            Console.WriteLine($"El costo total del viaje es: ${costo_total}.");
            
            var nuevodestino = new servicios()
            {
                origen = origen,
                destino = destino,
                distancia = distancia,
                estado = "pendiente",
                costo_total = costo_total,
            };
            
            db.Add(nuevodestino);
            db.SaveChanges();
            Console.WriteLine(" \nregistro agregado exitosamente..\n");
            flag =  false;
            
        }
    }
}