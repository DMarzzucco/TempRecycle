using System.Collections.Concurrent;

namespace TempRecycle.Utils
{
    public class Remove_file
    {
        public static async Task RemoveALl(string path, int folderCount, int filesCount)
        {

            Console.WriteLine("\nDeseas eliminar los archivos Temporales? (Y/N):");

            string? respuesta = Console.ReadLine()?.Trim().ToUpper();


            if (string.IsNullOrEmpty(respuesta))
            {
                Console.WriteLine("No escribio ningun dato");
            }

            var files = new ConcurrentBag<string>(Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories).Where(f => !f.Contains("Microsoft.CodeAnalysis") && !f.Contains("Windows")));


            var directories = new ConcurrentBag<string>(Directory.EnumerateDirectories(path, "*", SearchOption.AllDirectories));


            Console.WriteLine();

            if (respuesta == "Y")
            {
                foreach (var file in files)
                {
                    try
                    {
                        if (File.Exists(file))
                        {
                            File.SetAttributes(file, FileAttributes.Normal);
                            File.Delete(file);
                            Console.WriteLine($"Archivos Eliminado:");
                        }

                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        TempMessage.ShowTempMessage($"Could not in {file}: {ex.Message}", Console.CursorTop);
                    }
                    catch (IOException ex)
                    {
                        TempMessage.ShowTempMessage($"Could not in {file}: {ex.Message}", Console.CursorTop);
                    }
                    await Task.Yield();
                }
                //folders
                foreach (var dir in directories)
                {
                    try
                    {
                        Directory.Delete(dir);
                        Console.WriteLine($"Folder del eliminado");
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        TempMessage.ShowTempMessage($"Could not in {dir}: {ex.Message}", Console.CursorTop);
                    }
                    catch (IOException ex)
                    {
                        TempMessage.ShowTempMessage($"Could not in {dir}: {ex.Message}", Console.CursorTop);
                    }
                }
            }
            else if (respuesta == "N")
            {
                Console.WriteLine("Operacion Cancelada");
            }
            else
            {
                Console.WriteLine("Operacion invalida");
            }
        }
    }

}

