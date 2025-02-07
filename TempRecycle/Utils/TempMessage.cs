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
        }
    }
}
