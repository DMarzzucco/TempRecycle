#include "BoxMessage.h"

namespace BoxMessage
{
    void SetColor(WORD color)
    {
        SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), color);
    }
    
    void ShowBox(const std::string &message, WORD color)
    {
        SetColor(color);
        std::cout << "\n[ " << message << " ]\n";
        SetColor(15); 
    }

    void ShowError(const std::string &message)
    {
        ShowBox(message, 12); 
    }

    void ShowInfo(const std::string &message)
    {
        ShowBox(message, 11); 
    }

    void ShowDate(const std::string &message)
    {
        ShowBox(message, 14); 
    }
}
