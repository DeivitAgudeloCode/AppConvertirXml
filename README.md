🚀 Conversor XML → TW Object Script

Aplicación web desarrollada en Blazor Server (.NET 8) que permite transformar archivos XML contables en estructuras compatibles con TW Object, utilizando reglas dinámicas definidas mediante configuración JSON.

📋 Descripción

Esta herramienta fue creada para automatizar la conversión de información contable proveniente de archivos XML hacia estructuras de objetos TW utilizadas en procesos de integración.

El sistema identifica automáticamente cada nodo <ROW>, aplica las transformaciones configuradas en el archivo de reglas y genera el script final listo para ser utilizado.

Antes
<ROW>
    <FECHA>2025/12/31</FECHA>
    <CUENTA>28101501</CUENTA>
    <CUENTA_NOMBRE>DEPOSITOS PARA FUTUROS SERVICIOS</CUENTA_NOMBRE>
    <DEBITO>672967</DEBITO>
    <CREDITO>0</CREDITO>
</ROW>
Después
tw.local.sevenTodo = new tw.object.listOf.SevenTodo();

tw.local.sevenTodo[0] = new tw.object.SevenTodo();

var fechaTmp = '2025/12/31';

tw.local.sevenTodo[0].FECHA = new TWDate();
tw.local.sevenTodo[0].FECHA.parse(fechaTmp,"yyyy/MM/dd");

tw.local.sevenTodo[0].CUENTA = '28101501';
tw.local.sevenTodo[0].CUENTA_NOMBRE = 'DEPOSITOS PARA FUTUROS SERVICIOS';
tw.local.sevenTodo[0].DEBITO = '672967';
tw.local.sevenTodo[0].CREDITO = '0';
✨ Características

✅ Conversión automática XML → TW Object

✅ Procesamiento de múltiples registros <ROW>

✅ Configuración dinámica mediante JSON

✅ Conversión automática de fechas

✅ Transformaciones configurables

✅ Copiado automático al portapapeles

✅ Procesamiento asíncrono

✅ Compatible con XML sin nodo raíz

✅ Escalable para miles de registros

🏗️ Arquitectura
XML
 │
 ▼
XmlTransformerService
 │
 ▼
Reglas JSON
 │
 ▼
Transformaciones
 │
 ▼
Generación TW Object
 │
 ▼
Resultado Final
📁 Estructura del Proyecto
ConvertirXml
│
├── Components
│   └── Pages
│       └── Home.razor
│
├── Models
│   ├── MappingRules.cs
│   └── FieldRule.cs
│
├── Services
│   └── XmlTransformerService.cs
│
├── wwwroot
│   └── js
│       └── clipboard.js
│
├── reglas.mapeo.json
│
├── Program.cs
│
└── README.md
⚙️ Configuración de Reglas

Toda la lógica de conversión es configurable mediante:

reglas.mapeo.json
Ejemplo
{
  "rowTag": "ROW",
  "fields": [
    {
      "tag": "CUENTA",
      "format": "trim"
    },
    {
      "tag": "CUENTA_NOMBRE",
      "format": "upper"
    }
  ]
}
🔄 Transformaciones Disponibles
Transformación	Descripción
trim	Elimina espacios sobrantes
upper	Convierte a mayúsculas
lower	Convierte a minúsculas
Ejemplo
{
    "tag": "CUENTA_NOMBRE",
    "format": "upper"
}

Resultado:

DEPOSITOS PARA FUTUROS SERVICIOS
📅 Manejo de Fechas

Entrada XML:

<FECHA>2025/12/31</FECHA>

Salida:

var fechaTmp = '2025/12/31';

tw.local.sevenTodo[0].FECHA = new TWDate();
tw.local.sevenTodo[0].FECHA.parse(fechaTmp,"yyyy/MM/dd");
⚡ Rendimiento

La aplicación fue optimizada para trabajar con grandes volúmenes de información mediante:

Procesamiento asíncrono (Task.Run)
Uso de StringBuilder
Carga única de reglas JSON
Búsquedas optimizadas mediante diccionarios
Procesamiento dinámico de nodos XML
Compatibilidad con miles de registros <ROW>
🖥️ Tecnologías Utilizadas
Tecnología	Versión
.NET	8
Blazor Server	.NET 8
C#	12
LINQ to XML	Integrado
System.Text.Json	Integrado
JavaScript Interop	Integrado
🚀 Ejecución Local
Restaurar dependencias
dotnet restore
Ejecutar aplicación
dotnet run

o

dotnet watch

La aplicación quedará disponible en:

https://localhost:xxxx
🔧 Configuración Inicial

Registrar el servicio en:

builder.Services.AddScoped<XmlTransformerService>();
📈 Mejoras Futuras
 Descarga automática del resultado en .txt
 Carga de XML mediante archivo
 Barra de progreso
 Exportación masiva
 Validación visual del XML
 Transformaciones avanzadas configurables
 Historial de conversiones
 Procesamiento por lotes
👨‍💻 Autor

Deivit

Proyecto desarrollado para automatizar la transformación de estructuras XML contables hacia objetos TW configurables mediante reglas dinámicas, reduciendo tiempos operativos y facilitando procesos de integración.
