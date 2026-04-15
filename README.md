# Actividad Consola C# - Sistema de Gestión de Transporte

Este proyecto es una app de consola en **C# (.NET 9)** para practicar:

- Clases y objetos
- Métodos con validaciones
- Flujo por menú en consola
- Persistencia con base de datos

La idea de esta guía es que una persona con poca experiencia en C# pueda seguir la actividad paso a paso.

---

## 1) ¿Qué resuelve este sistema?

Permite administrar una operación de transporte:

1. Registrar conductores
2. Registrar vehículos
3. Crear servicios (viajes)
4. Asignar conductor + vehículo a un servicio
5. Iniciar servicios
6. Finalizar servicios
7. Consultar información
8. Ver reportes operativos

---

## 2) Estructura del proyecto (qué archivo hace qué)

```text
actividad_Consola_c-
├── Program.cs                      # Menú principal y navegación
├── database/
│   ├── conexion_database.cs        # DbContext de MySQL
│   ├── conexionMongodb.cs          # Conexión básica a MongoDB
│   └── verificar_conexion.cs       # Verifica conexión MySQL
├── services/
│   ├── servicio_registros.cs       # Registrar conductor/vehículo/servicio
│   ├── asignar_servicio.cs         # Asignar recursos a servicio pendiente
│   ├── funcionalidades_servicio.cs # Iniciar/finalizar/consultar
│   └── reportes_servicio.cs        # Resumen operativo
└── tablesSQL/
    ├── conductores.cs              # Modelo conductor
    ├── vehiculos.cs                # Modelo vehículo
    └── servicios.cs                # Modelo servicio
```

---

## 3) Paso 1: Preparar el entorno

### Requisitos

- .NET SDK 9
- Acceso a MySQL (según cadena de conexión actual del proyecto)
- Acceso a MongoDB (según cadena de conexión actual del proyecto)

### Comandos base

Ejecutar desde:
`/home/runner/work/actividad_Consola_c-/actividad_Consola_c-`

```bash
dotnet restore
dotnet build
dotnet run
```

---

## 4) Paso 2: Entender el menú principal

`Program.cs` crea instancias de las clases de servicio y muestra el menú:

```csharp
var reportes = new reportes_servicio();
var funcionalidades = new funcionalidades_servicio();
var asignar_servicio = new asignar_servicio();
var servicio_registros = new servicio_registros();
```

Cuando eliges una opción, se llama un método específico:

- Opción 1 -> `servicio_registros.registro_conductor()`
- Opción 2 -> `servicio_registros.registro_vehiculo()`
- Opción 3 -> `servicio_registros.registro_servicio()`
- Opción 4 -> `asignar_servicio.servicio()`
- Opción 5 -> `funcionalidades.iniciarServicio()` *(en el código actual el método se llama `inicarServicio()`)*
- Opción 6 -> `funcionalidades.finalizarServicio()`
- Opción 7 -> `funcionalidades.ConsultarServicio()`
- Opción 8 -> `funcionalidades.ConsultarConductoresVehiculos()`
- Opción 9 -> `reportes.reportes()`

---

## 5) Paso 3: Clases que debías crear (con ejemplos pequeños)

## 5.1 Modelos (`tablesSQL`)

Estas clases representan tablas en la base de datos.

### `conductores`

Campos:
- `id`
- `numero_identificacion`
- `nombre_completo`
- `licencia` (C1, C2, C3)
- `estado`

Ejemplo:

```csharp
var conductor = new conductores
{
    numero_identificacion = 1001,
    nombre_completo = "Ana Perez",
    licencia = "C2",
    estado = "disponible"
};
```

### `vehiculos`

Campos:
- `id`
- `placa`
- `tipo_vehiculo`
- `capacidad`
- `estado`

Ejemplo:

```csharp
var vehiculo = new vehiculos
{
    placa = "ABC123",
    tipo_vehiculo = "camion",
    capacidad = 8,
    estado = "disponible"
};
```

### `servicios`

Campos:
- `id`
- `origen`
- `destino`
- `distancia`
- `estado`
- `costo_total`
- `id_conductor` (nullable)
- `id_vehiculo` (nullable)

Ejemplo:

```csharp
double distancia = 120;
double costoKilometro = 4000;

var servicio = new servicios
{
    origen = "Medellin",
    destino = "Rionegro",
    distancia = distancia,
    estado = "pendiente",
    costo_total = distancia * costoKilometro
};
```

---

## 5.2 Capa de datos (`database`)

### `MysqlDbContext`

Define el acceso a tablas:

```csharp
public DbSet<conductores> conductores { get; set; }
public DbSet<servicios> servicios { get; set; }
public DbSet<vehiculos> vehiculos { get; set; }
```

Ejemplo de consulta:

```csharp
var db = new MysqlDbContext();
var activos = db.servicios.Where(s => s.estado == "activo").ToList();
```

### `VerificarConexion`

Comprueba si MySQL está disponible:

```csharp
if (db.Database.CanConnect())
{
    Console.WriteLine("Conectado");
}
```

### `conexionMongodb`

Realiza una conexión base a Mongo para futuras extensiones de reportes.

---

## 5.3 Capa de servicios (`services`)

### A) `servicio_registros` (crear información)

Métodos:
- `registro_conductor()`
- `registro_vehiculo()`
- `registro_servicio()`

Ejemplo del patrón usado en validaciones:

```csharp
int identificacion;
if (!int.TryParse(Console.ReadLine(), out identificacion))
{
    Console.WriteLine("error, solo datos numéricos");
}
```

Ejemplo de guardado:

```csharp
db.Add(conductor);
db.SaveChanges();
```

---

### B) `asignar_servicio` (asignación de recursos)

Método:
- `servicio()`

Lógica paso a paso:
1. Busca servicios pendientes sin asignaciones.
2. Pide elegir un servicio por ID.
3. Muestra vehículos disponibles y pide elegir ID.
4. Muestra conductores disponibles y pide elegir ID.
5. Actualiza estados y relación del servicio.

Fragmento clave:

```csharp
servicio_encontrado.id_conductor = conductorAsignado.id;
servicio_encontrado.id_vehiculo = vehiculoAsignado.id;
conductorAsignado.estado = "servicio";
vehiculoAsignado.estado = "servicio";
db.SaveChanges();
```

---

### C) `funcionalidades_servicio` (operación diaria)

Métodos:
- `iniciarServicio()` *(en el código actual aparece como `inicarServicio()`)* -> pendiente a activo
- `finalizarServicio()` -> pendiente/activo a inactivo
- `ConsultarServicio()` -> lista todos
- `ConsultarConductoresVehiculos()` -> submenú de consulta

Ejemplo iniciar servicio:

```csharp
var servicioElegido = servicios_pendientes.FirstOrDefault(s => s.id == opcionId);
if (servicioElegido != null)
{
    servicioElegido.estado = "activo";
    db.SaveChanges();
}
```

---

### D) `reportes_servicio` (resumen operativo)

Método:
- `reportes()`

Muestra:
- total de servicios,
- resumen por estado (cantidad, promedio costo, distancia),
- listado final.

Ejemplo de agrupación:

```csharp
var estados = db.servicios
    .GroupBy(s => s.estado)
    .Select(g => new { estado = g.Key, total = g.Count() })
    .ToList();
```

---

## 6) Paso 4: Guía de uso completa (ejercicio sugerido)

Sigue este orden exacto para probar todo el flujo:

### 1. Registrar conductor (opción 1)

Entradas de ejemplo:
- Identificación: `1001`
- Nombre: `Ana Perez`
- Licencia: `C2`

Resultado esperado: “Registro agregado exitosamente”.

### 2. Registrar vehículo (opción 2)

Entradas de ejemplo:
- Placa: `ABC123`
- Tipo: `camion`
- Capacidad: `10`

Resultado esperado: registro creado con estado `disponible`.

### 3. Registrar servicio (opción 3)

Entradas de ejemplo:
- Origen: `Medellin`
- Destino: `Bogota`
- Distancia: `420`

Resultado esperado:
- costo calculado = `420 * 4000 = 1680000`,
- servicio en estado `pendiente`.

### 4. Asignar servicio (opción 4)

Selecciona:
1. ID del servicio pendiente,
2. ID del vehículo disponible,
3. ID del conductor disponible.

Resultado esperado:
- servicio queda asignado,
- conductor/vehículo pasan a `servicio`.

### 5. Iniciar servicio (opción 5)

Selecciona el ID del servicio.
Resultado esperado: estado `activo`.

### 6. Finalizar servicio (opción 6)

Selecciona el ID del servicio.
Resultado esperado: estado `inactivo`.

### 7. Consultar servicios (opción 7)

Debe mostrar el historial de servicios con su estado.

### 8. Consultar conductores y vehículos (opción 8)

Prueba subopciones:
- `1` para conductores
- `2` para vehículos
- `3` para salir al menú principal

### 9. Reportes (opción 9)

Verifica que aparezcan:
- total,
- resumen por estado,
- listado general.

---

## 7) Errores comunes y cómo evitarlos

1. **Ingresar texto donde se espera número**  
   Solución: usar solo números en identificaciones, capacidad e IDs.

2. **Dejar campos vacíos**  
   Solución: completar nombre, origen, destino, placa, etc.

3. **Licencia inválida**  
   Solución: solo `C1`, `C2` o `C3`.

4. **Intentar asignar recursos no disponibles**  
   Solución: validar estados antes de seleccionar.

---

## 8) Mini plantilla para crear nuevas clases/métodos parecidos

Si te piden agregar una nueva funcionalidad, sigue esta plantilla:

```csharp
public class nueva_funcionalidad
{
    MysqlDbContext db = new MysqlDbContext();

    public void ejecutar()
    {
        // 1) Pedir datos
        // 2) Validar datos
        // 3) Consultar/actualizar BD
        // 4) Guardar con db.SaveChanges()
        // 5) Mostrar mensaje final en consola
    }
}
```

---

## 9) Recomendaciones finales para mejorar la actividad

- Mover cadenas de conexión fuera del código (variables de entorno o configuración).
- Estandarizar nombres de métodos (por ejemplo `iniciarServicio`).
- Añadir pruebas automáticas para validaciones de entrada.
- Separar mejor lógica de presentación (Console) y lógica de negocio.
