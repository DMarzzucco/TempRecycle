
using System.Collections.Concurrent;

namespace TempRecycle.Utils
{
    public class CountFiles
    {
        public static async Task<(int, int, long)> GetFileAndSizeAsync(string path)
        {
            int currentLine = Console.CursorTop;

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
                            BoxMessage.ShowError($"Protect file{file}", ref currentLine);
                        }
                        totalSize += fileInfo.Length;
                        fileCount++;
                        DrawProg.ProgresBar(fileCount, totalFile, "Files:", 2);
                        await Task.Delay(100);
                    }
                    catch (Exception ex)
                    {
                        BoxMessage.ShowError($"Could not in {file}: {ex.Message}", ref currentLine);
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
                        BoxMessage.ShowError($"Could not in {dir}: {ex.Message}", ref currentLine);
                    }
                    await Task.Yield();
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                BoxMessage.ShowError($"Denied access: {ex.Message}", ref currentLine);
            }
            catch (Exception ex)
            {
                BoxMessage.ShowError($"Error to scan files: {ex.Message}", ref currentLine);
            }

            //BoxMessage.ShowDate(
            //    $"Total Files:{fileCount}" +
            //    $"Total Folders:{folderCount}" +
            //    $"Total Size:{FromBytes.FormatBytes(totalSize)}", Console.CursorTop);

            Console.WriteLine($"\nCantidad de archivos:{fileCount}");
            Console.WriteLine($"Cantidad de carpeta:{folderCount}");
            Console.WriteLine($"Tamaño total:{FromBytes.FormatBytes(totalSize)}");

            await Remove_file.RemoveALl(path, fileCount, folderCount);

            return (fileCount, folderCount, totalSize);
        }
    }
}
