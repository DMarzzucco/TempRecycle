using System.Collections.Concurrent;

namespace TempRecycle.Utils
{
    public class Remove_file
    {
        public static async Task RemoveALl(string path, int folderCount, int filesCount)
        {
            int currentLine = Console.CursorTop;

            Console.WriteLine("\nDo you want to delete the scanned files? (Y/N):");
            string? respuesta = Console.ReadLine()?.Trim().ToUpper();


            if (string.IsNullOrEmpty(respuesta))
            {
                BoxMessage.ShowError("No data was written", ref currentLine);
                return;
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
                            BoxMessage.ShowInfo("Deleted Files", ref currentLine);
                        }

                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        BoxMessage.ShowError($"Could not in {file} : {ex.Message}", ref currentLine);
                    }
                    catch (IOException ex)
                    {
                        BoxMessage.ShowError($"Could not in {file} : {ex.Message}",  ref currentLine);
                    }
                    await Task.Yield();
                }
                //folders
                foreach (var dir in directories)
                {
                    try
                    {
                        Directory.Delete(dir);
                        BoxMessage.ShowInfo("Deleted Folders", ref currentLine);
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        BoxMessage.ShowError($"Could not in {dir} : {ex.Message}", ref currentLine);
                    }
                    catch (IOException ex)
                    {
                        BoxMessage.ShowError($"Could not in {dir} : {ex.Message}", ref currentLine);
                    }
                }
            }
            else if (respuesta == "N")
            {
                BoxMessage.ShowInfo("Cancel Operation", ref currentLine);
            }
            else
            {
                BoxMessage.ShowError("Faild Operation", ref currentLine);
            }
        }
    }

}

