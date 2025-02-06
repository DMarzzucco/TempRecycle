using TempRecycle.Auth;
using TempRecycle.Utils;

class Program
{
    static async Task Main(string[] args)
    {
        int currentLine = Console.CursorTop;
        // Admin
        if (!AdminConfig.isRunningAsAdmin())
        {
            BoxMessage.ShowError("Se necesuta ser admin", ref currentLine);
            Console.WriteLine("Presiona Cualquier tecla para salir");
            Console.ReadKey();
            AdminConfig.RestarAsAdminstrator();
            return;
        }

        Console.WriteLine("Welcome, presione (E) para hacer el escaneo. (N) para cancelar la operacion");
        string? respuesta = Console.ReadLine()?.Trim().ToUpper();

        if (string.IsNullOrEmpty(respuesta))
        {
            BoxMessage.ShowError("No se ingresó ningún dato", ref currentLine);
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
                BoxMessage.ShowError("No se pudo obtener la carpeta TEMP", ref currentLine);
                return;
            }

            Console.WriteLine($"Cargando datos de {tempPath}..");

            //
            var (fileCount, folderCount, totalSize) = await CountFiles.GetFileAndSizeAsync(tempPath);
            Console.Clear();

        }
        else if (respuesta == "N")
        {
            BoxMessage.ShowInfo("Operación cancelada", ref currentLine);
        }
        else
        {
            BoxMessage.ShowError("Operación invalida", ref currentLine);
        }


        Console.WriteLine("Presiona Cualquier tecla para salir");
        Console.ReadKey();
    }


}