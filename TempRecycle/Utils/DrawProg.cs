
namespace TempRecycle.Utils
{
    public class DrawProg
    {
        public static void ProgresBar(int progress, int total, string label, int lineOffset)
        {
            Console.CursorVisible = false;
            int width = 50;
            int pos = (int)((double)progress / total * width);

            Console.SetCursorPosition(0, lineOffset);

            Console.Write(new string(' ', Console.WindowWidth - 1));
            Console.SetCursorPosition(0, lineOffset);

            Console.Write($"{label, -7} [");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(new string('=', pos));
            Console.ResetColor();
            Console.Write(new string(' ', width - pos));
            Console.Write($"] {progress}/{total} ({100 * progress / total}%)");
        }
    }
}
