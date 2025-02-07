using TempRecycle.Auth;
using TempRecycle.Int;
using TempRecycle.Utils;

class Program
{
    static async Task Main(string[] args)
    {
        int currentLine = Console.CursorTop;

        if (!AdminConfig.isRunningAsAdmin())
        {
            BoxMessage.ShowError("You need be Admin", ref currentLine);
            Console.WriteLine(" ");
            Console.WriteLine("Push any key to close..");
            Console.ReadKey();
            AdminConfig.RestarAsAdminstrator();
            return;
        }

        await IntMain.ImplementInt();
        Console.WriteLine("Push any key to close");
        Console.ReadKey();
    }
}