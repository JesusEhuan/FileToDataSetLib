# FileToDataSetLib 📄➡️📊

    Convierte archivos de distintos formatos (`.xlsx`, `.csv`, `.json`, `.xml`, `.zip`) en `DataSet` de forma sencilla y reutilizable.  
    Diseñado por [Jesus Manuel Ehuan](https://github.com/JesusEhuan) para proyectos modernos en .NET 6+.

    ---

## 🚀 Características

    ✅ Lectura de archivos:
    - Excel (`.xls`, `.xlsx`)
    - CSV (`.csv`)
    - JSON (`.json`)
    - XML (`.xml`)
    - ZIP con múltiples archivos combinados

    ✅ Retorna `DataSet` directamente

    ✅ Modular, extensible, y 100% compatible con:
    - APIs en .NET
    - Aplicaciones Web
    - Consolas o procesos batch

---

## 🔧 Instalación
    dotnet add package FileToDataSetLib


## Ejemplo de uso
    using FileToDataSetLib;
    using System.Data;

    var reader = new FileToDataSetReader();

    using var stream = File.OpenRead("documento.xlsx");
    DataSet ds = reader.ReadFile(stream, "documento.xlsx");

    // Acceder a los datos
    foreach (DataTable table in ds.Tables)
    {
        Console.WriteLine($"Tabla: {table.TableName} - Filas: {table.Rows.Count}");
}


## 🧩 Estructura Interna
    -FileToDataSetReader.cs → Clase orquestadora
    -Interfaces/IFileParser.cs → Interfaz base
    -Parsers/*Parser.cs → Parsers individuales por tipo

## 📦 Ejemplo: Uso con ZIP
    using var zipStream = File.OpenRead("archivos.zip");
    DataSet ds = reader.ReadFile(zipStream, "archivos.zip");

    //Todos los archivos dentro del .zip serán procesados si su extensión está soportada.

## 📜 Licencia
    MIT © Jesus Manuel Ehuan
    Puedes usarlo, modificarlo y distribuirlo libremente (¡pero dame estrellita si te sirvió! ⭐).
## ✨ Créditos
    Este paquete usa:

    ExcelDataReader (MIT)

    Newtonsoft.Json (MIT)

## 💬 ¿Dudas o sugerencias?
    Mándame mensaje por GitHub o abre un issue. ¡Estoy abierto a mejoras!
