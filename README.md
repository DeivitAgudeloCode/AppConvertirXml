**Características**
Conversión de XML a formato TW Object.
Soporte para múltiples registros <ROW>.
Configuración dinámica mediante archivo reglas.mapeo.json.
Transformaciones de datos configurables.
Conversión automática de fechas.
Copia del resultado al portapapeles.
Procesamiento asíncrono para mejorar el rendimiento.
Compatible con XML sin nodo raíz.

**Tecnologías Utilizadas**
.NET 8
Blazor Server
C#
LINQ to XML
System.Text.Json
JavaScript Interop

**Estructura del Proyecto**
ConvertirXml
│
├── Components/
│   └── Pages/
│       └── Home.razor
│
├── Models/
│   ├── FieldRule.cs
│   └── MappingRules.cs
│
├── Services/
│   └── XmlTransformerService.cs
│
├── wwwroot/
│   └── js/
│       └── clipboard.js
│
├── reglas.mapeo.json
│
└── Program.cs

**Funcionamiento**
XML de Entrada
<ROW>
    <FECHA>2025/12/31</FECHA>
    <CUENTA>28101501</CUENTA>
    <CUENTA_NOMBRE>DEPOSITOS PARA FUTUROS SERVICIOS</CUENTA_NOMBRE>
    <DEBITO>672967</DEBITO>
    <CREDITO>0</CREDITO>
</ROW>

**Resultado Generado**

tw.local.sevenTodo = new tw.object.listOf.SevenTodo();
tw.local.sevenTodo[0] = new tw.object.SevenTodo();
var fechaTmp = '2025/12/31';
tw.local.sevenTodo[0].FECHA = new TWDate();
tw.local.sevenTodo[0].FECHA.parse(fechaTmp,"yyyy/MM/dd");
tw.local.sevenTodo[0].CUENTA = '28101501';
tw.local.sevenTodo[0].CUENTA_NOMBRE = 'DEPOSITOS PARA FUTUROS SERVICIOS';
tw.local.sevenTodo[0].DEBITO = '672967';
tw.local.sevenTodo[0].CREDITO = '0';

**Configuración de Reglas**
El comportamiento de la conversión se controla mediante el archivo:

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

**Transformaciones Disponibles**
Actualmente se soportan:

Transformación	Descripción
trim	Elimina espacios al inicio y final
upper	Convierte a mayúsculas
lower	Convierte a minúsculas

Ejemplo:
{
  "tag": "CUENTA_NOMBRE",
  "format": "upper"
}

Resultado:
DEPOSITOS PARA FUTUROS SERVICIOS
Manejo de Fechas

El campo:
<FECHA>2025/12/31</FECHA>

se convierte automáticamente a:
var fechaTmp = '2025/12/31';
tw.local.sevenTodo[0].FECHA = new TWDate();
tw.local.sevenTodo[0].FECHA.parse(fechaTmp,"yyyy/MM/dd");

**Consideraciones Técnicas**
XML sin Nodo Raíz

La aplicación admite entradas como:
<ROW>
...
</ROW>
<ROW>
...
</ROW>

internamente se transforman a:

<ROOT>
    ...
</ROOT>

para permitir el procesamiento mediante XDocument.

**Rendimiento**
Se optimizó el procesamiento para:

Miles de registros <ROW>.
Búsqueda rápida de elementos mediante diccionarios.
Procesamiento asíncrono con Task.Run.
Carga única del archivo de reglas.
Ejecución Local
Restaurar paquetes
dotnet restore
Ejecutar aplicación
dotnet run
o
dotnet watch

**Configuración de Dependencias**
Registrar el servicio en:

Program.cs
builder.Services.AddScoped<XmlTransformerService>();
Mejoras Futuras

Exportación directa a archivo .txt.
Carga de XML mediante archivo.
Descarga automática del resultado.
Barra de progreso para archivos grandes.
Soporte para nuevas transformaciones configurables.
Validación visual del XML.
Procesamiento por lotes.

Autor
Proyecto desarrollado para automatizar la transformación de estructuras XML contables hacia objetos TW configurables mediante reglas JSON, reduciendo tiempos de conversión manual y permitiendo reutilización en distintos escenarios de integración.
