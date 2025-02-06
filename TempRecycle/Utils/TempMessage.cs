namespace TempRecycle.Utils
{
    public class TempMessage
    {
        public static void ShowTempMessage(string message, ref int line, bool isError = true)
        {
            if (isError)
            {
                BoxMessage.ShowError(message, ref line);
            }
            else
            {
                BoxMessage.ShowInfo(message, ref line);
            }

            //int cursorLeft = Console.CursorLeft;
            //int cursorTop = Console.CursorTop;

            //Console.SetCursorPosition(0, line);

            //Console.Write(message);

            //Task.Delay(100).Wait();

            //Console.SetCursorPosition(0, line);
            //Console.Write(new string(' ', Console.WindowWidth - 1));

            //Console.SetCursorPosition(cursorLeft, cursorTop);
        }
    }
}
