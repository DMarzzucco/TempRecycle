#pragma once
#include <iostream>
#include <windows.h>

namespace BoxMessage
{
    void SetColor(WORD color);
    void ShowError(const std::string &message);
    void ShowInfo(const std::string &message);
    void ShowDate(const std::string &message);
}