using System;
using System.Runtime.InteropServices;
public class Program
{
    [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
    private static extern int SHEmptyRecycleBin(IntPtr hwnd, string? pszRootPath, RecycleBinFlags dwFlags);

    [Flags]
    private enum RecycleBinFlags : uint
    {
        NoConfirmation = 0x00000001,
        NoProgressUI = 0x00000002,
        NoSound = 0x00000004
    }

    public static void Main()
    {
        Console.WriteLine("Attempting to empty the Recycle Bin...");

        try
        {
            bool succes = EmptyRecycleBin();
            if (succes)
            {
                Console.WriteLine("The Recycle Bin has ben cleaning successfully");

            }
            else
            {
                Console.WriteLine("Unknown error occurred during the execution.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error ocurred: {ex.Message}");
        }

    }

    public static bool EmptyRecycleBin()
    {
        int result = SHEmptyRecycleBin(IntPtr.Zero, null, RecycleBinFlags.NoConfirmation | RecycleBinFlags.NoProgressUI | RecycleBinFlags.NoSound);

        if (result != 0)
        {
            Console.WriteLine($"Error: Unable to empty the Recycle Bin (Error Code: {result})");
            return false;
        }
        return true;
    }
}
