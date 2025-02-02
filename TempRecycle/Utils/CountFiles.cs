
using System.Collections.Concurrent;
using System.IO;

namespace TempRecycle.Utils
{
    public class CountFiles
    {
        public static async Task<(int, int, long)> GetFileAndSizeAsync(string path)
        {
            int fileCount = 0;
            int folderCount = 0;
            long totalSize = 0;
            try
            {
                var files = new ConcurrentBag<string>(Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories));

                int totalFile = files.Count;

                var directories = new ConcurrentBag<string>(Directory.EnumerateDirectories(path, "*", SearchOption.AllDirectories));

                int totalFolder = directories.Count;

                Console.WriteLine();

                //files
                foreach (var file in files)
                {
                    try
                    {
                        FileInfo fileInfo = new FileInfo(file);

                        if (fileInfo.Attributes.HasFlag(FileAttributes.System) || fileInfo.Attributes.HasFlag(FileAttributes.Hidden))
                        {
                            //Console.WriteLine($"Archivo protegido: {file}");
                            TempMessage.ShowTempMessage($"Protect file{file}", Console.CursorTop);
                            //return;
                        }
                        totalSize += fileInfo.Length;
                        fileCount++;
                        DrawProg.ProgresBar(fileCount, totalFile, "Files:", 2);
                        await Task.Delay(100);
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine($"No se puede acceder a {file}: {ex.Message}");
                        TempMessage.ShowTempMessage($"Could not in {file}: {ex.Message}", Console.CursorTop);
                    }
                    await Task.Yield();
                }
                //Folder
                foreach (var dir in directories)
                {
                    try
                    {
                        folderCount++;
                        DrawProg.ProgresBar(folderCount, totalFolder, "Folders:", 3);
                        await Task.Delay(100);
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine($"No se puede acceder a {dir}: {ex.Message}");
                        TempMessage.ShowTempMessage($"Could not in {dir}: {ex.Message}", Console.CursorTop);
                    }
                    await Task.Yield();
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                //Console.WriteLine("Acceso denegado a algunos directorios.");
                TempMessage.ShowTempMessage($"Denied access: {ex.Message}", Console.CursorTop);
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error al escanear archivos: {ex.Message}");
                TempMessage.ShowTempMessage($"Error to scan files: {ex.Message}", Console.CursorTop);
            }

            Console.WriteLine($"\nCantidad de archivos:{fileCount}");
            Console.WriteLine($"Cantidad de carpeta:{folderCount}");
            Console.WriteLine($"Tamaño total:{FromBytes.FormatBytes(totalSize)}");

            await Remove_file.RemoveALl(path, fileCount, folderCount);

            return (fileCount, folderCount, totalSize);
        }
    }
}
