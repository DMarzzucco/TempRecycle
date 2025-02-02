using System;
using System.IO;
using System.Threading;
using System.Security.Principal;
using System.Diagnostics;
using TempRecycle.Auth;
using TempRecycle.Utils;

class Program
{
    static async Task Main(string[] args)
    {
        // Admin
        if (!AdminConfig.isRunningAsAdmin())
        {
            Console.WriteLine("Se necesita ser admin");
            Console.WriteLine("Presiona Cualquier tecla para salir");
            Console.ReadKey();
            AdminConfig.RestarAsAdminstrator();
            return;
        }

        Console.WriteLine("Welcome, presione (E) para hacer el escaneo. (N) para cancelar la operacion");

        string? respuesta = Console.ReadLine()?.Trim().ToUpper();

        if (string.IsNullOrEmpty(respuesta))
        {
            Console.WriteLine("No escribio ningun dato");
            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
            return;
        }

        if (respuesta == "E")
        {
            // Path
            string? tempPath = Environment.GetEnvironmentVariable("TEMP");

            if (tempPath == null)
            {
                Console.WriteLine("No se pudo obtener la carpeta TEMP");
                return;
            }

            Console.WriteLine($"Cargando datos de {tempPath}..");

            //
            var (fileCount, folderCount, totalSize) = await CountFiles.GetFileAndSizeAsync(tempPath);
            Console.Clear();

        }
        else if (respuesta == "N")
        {
            Console.WriteLine("Operacion Cancelada");
        }
        else
        {
            Console.WriteLine("Operacion no valida");
        }


        Console.WriteLine("Presiona Cualquier tecla para salir");
        Console.ReadKey();
    }


}