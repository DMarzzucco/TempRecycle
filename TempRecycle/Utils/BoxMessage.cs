namespace TempRecycle.Utils
{
    public static class BoxMessage
    {
        public static void ShowError(string message, ref int line)
        {
            ShowBox(message, ref line, ConsoleColor.Red);
        }

        public static void ShowInfo(string message, ref int line) {
            ShowBox(message, ref line, ConsoleColor.Blue);
        }

        public static void ShowDate(string message, ref int line) {
            ShowBox(message, ref line, ConsoleColor.Yellow);
        }

        private static void ShowBox(string message, ref int line, ConsoleColor color)
        {
            int padding = 2;
            int maxWidth = Console.WindowWidth - 2;

            if (message.Length > maxWidth - 2 * padding)
                message = message.Substring(0, maxWidth - 2 * padding - 3) + "...";

            int boxWidth = message.Length + 2 * padding;

            Console.ForegroundColor = color;
            Console.SetCursorPosition(0, line);
            Console.Write("+" + new string('-', boxWidth) + "+");

            Console.SetCursorPosition(0, line + 1);
            Console.Write("|" + new string(' ', padding) + message + new string(' ', padding) + "|");

            Console.SetCursorPosition(0, line + 2);
            Console.Write("+" + new string('-', boxWidth) + "+");

            Console.ResetColor();

            line += 3;
        }
    }
}
