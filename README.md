# Actividad Consola C# - Sistema de Gestión de Transporte

Este proyecto es una aplicación de consola en **C# (.NET 9)** para gestionar operaciones de transporte:

- Registro de conductores
- Registro de vehículos
- Registro de servicios de transporte
- Asignación de conductor y vehículo a un servicio
- Inicio y finalización de servicios
- Consultas y reportes

---

## 1) ¿Qué hace el programa?

Cuando ejecutas la app, se muestra un menú principal con 10 opciones.

Flujo general:

1. Verifica conexión a MySQL (`VerificarConexion.VerificacionConexion()`).
2. Verifica conexión a MongoDB (`conexionMongodb.conectarMongo()`).
3. Muestra el menú principal.
4. Ejecuta la lógica según la opción elegida.

Archivo principal: `/home/runner/work/actividad_Consola_c-/actividad_Consola_c-/Program.cs`

---

## 2) Estructura del proyecto

```text
actividad_Consola_c-
├── Program.cs
├── database/
│   ├── conexion_database.cs
│   ├── conexionMongodb.cs
│   └── verificar_conexion.cs
├── services/
│   ├── servicio_registros.cs
│   ├── asignar_servicio.cs
│   ├── funcionalidades_servicio.cs
│   └── reportes_servicio.cs
└── tablesSQL/
    ├── conductores.cs
    ├── vehiculos.cs
    └── servicios.cs
```

---

## 3) Clases que debías crear (explicadas con ejemplo corto)

### 3.1 Entidades (`tablesSQL`)

Estas clases representan tablas de la base de datos.

#### `conductores`
- `id`
- `numero_identificacion`
- `nombre_completo`
- `licencia` (C1, C2, C3)
- `estado` (por ejemplo `disponible` o `servicio`)

Ejemplo mínimo:

```csharp
var conductor = new conductores
{
    numero_identificacion = 12345,
    nombre_completo = "ana perez",
    licencia = "C2",
    estado = "disponible"
};
```

#### `vehiculos`
- `id`
- `placa`
- `tipo_vehiculo`
- `capacidad`
- `estado`

Ejemplo mínimo:

```csharp
var vehiculo = new vehiculos
{
    placa = "ABC123",
    tipo_vehiculo = "camion",
    capacidad = 12,
    estado = "disponible"
};
```

#### `servicios`
- `id`
- `origen`
- `destino`
- `distancia`
- `estado` (`pendiente`, `activo`, `inactivo`)
- `costo_total`
- `id_conductor` (nullable)
- `id_vehiculo` (nullable)

Ejemplo mínimo:

```csharp
var servicio = new servicios
{
    origen = "medellin",
    destino = "bogota",
    distancia = 420,
    estado = "pendiente",
    costo_total = 420 * 4000
};
```

---

### 3.2 Contexto de base de datos (`database`)

#### `MysqlDbContext` (`conexion_database.cs`)
Conecta con MySQL y expone los `DbSet`:

- `DbSet<conductores> conductores`
- `DbSet<servicios> servicios`
- `DbSet<vehiculos> vehiculos`

Ejemplo corto de uso:

```csharp
var db = new MysqlDbContext();
var totalServicios = db.servicios.Count();
```

#### `VerificarConexion` (`verificar_conexion.cs`)
Método:
- `VerificacionConexion()`: usa `db.Database.CanConnect()` para confirmar si MySQL responde.

#### `conexionMongodb` (`conexionMongodb.cs`)
Método:
- `conectarMongo()`: crea cliente Mongo y valida conexión inicial.

---

### 3.3 Servicios de negocio (`services`)

#### `servicio_registros`
Encargado de crear registros.

Métodos:
- `registro_conductor()`
- `registro_vehiculo()`
- `registro_servicio()`

Ejemplo de lo que hace `registro_servicio()`:

1. Pide origen, destino y distancia.
2. Calcula costo: `distancia * 4000`.
3. Guarda en MySQL con estado inicial `pendiente`.

#### `asignar_servicio`
Método:
- `servicio()`

Lógica:

1. Busca servicios `pendiente` sin conductor ni vehículo.
2. Muestra vehículos `disponible`.
3. Muestra conductores `disponible`.
4. Asigna IDs al servicio.
5. Cambia estados de conductor y vehículo a `servicio`.

#### `funcionalidades_servicio`
Métodos:

- `inicarServicio()`: cambia estado de `pendiente` a `activo`.
- `finalizarServicio()`: cambia estado a `inactivo`.
- `ConsultarServicio()`: lista todos los servicios.
- `ConsultarConductoresVehiculos()`: submenú para consultar conductores o vehículos.

Ejemplo muy corto de cambio de estado:

```csharp
var servicio = db.servicios.FirstOrDefault(s => s.id == opcionId);
if (servicio != null)
{
    servicio.estado = "activo";
    db.SaveChanges();
}
```

#### `reportes_servicio`
Método:
- `reportes()`

Genera:
- Total de servicios
- Resumen por estado (cantidad, promedio de costo, distancia total)
- Listado final de servicios

---

## 4) Explicación de cada opción del menú

1. **Registrar conductor**  
   Guarda identificación, nombre, licencia y estado inicial.

2. **Registrar vehículo**  
   Guarda placa, tipo, capacidad y estado inicial.

3. **Registrar servicio de transporte**  
   Guarda origen/destino/distancia y calcula costo total.

4. **Asignar conductor y vehículo a servicio**  
   Vincula recursos disponibles a un servicio pendiente.

5. **Iniciar servicio**  
   Cambia estado del servicio a activo.

6. **Finalizar servicio**  
   Cambia estado del servicio a inactivo.

7. **Consultar servicios**  
   Lista todos los servicios.

8. **Consultar conductores y vehículos**  
   Submenú para listar conductores o vehículos.

9. **Reportes operativos**  
   Muestra agregados y listado de servicios.

10. **Salir**  
    Termina la aplicación.

---

## 5) Cómo ejecutar

Desde la raíz del proyecto:

```bash
dotnet restore
dotnet build
dotnet run
```

---

## 6) Recomendaciones importantes

- El proyecto usa cadenas de conexión en código. Para producción, se recomienda moverlas a configuración segura (variables de entorno o `appsettings`).
- Validar entradas del usuario evita errores en consola y en base de datos.
- Mantener consistencia de estados (`disponible`, `servicio`, `pendiente`, `activo`, `inactivo`) facilita los reportes.
