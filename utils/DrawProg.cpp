#include "DrawProg.h"
#include <iostream>
#include <windows.h>
#include <iomanip>

namespace DrawProg {
    void ProgressBar(int progress, int total, const std::string& label, int lineOffset) {
        if (total == 0) return;

        const int width = 50;
        int percent = static_cast<int>(100.0 * progress / total);
        int pos = static_cast<int>((double)progress / total * width);

        COORD coord = {0, static_cast<SHORT>(lineOffset)};
        SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), coord);

        std::cout << label << std::setw(8 - label.length()) << " [";

        SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), 10); 
        std::cout << std::string(pos, '=');

        SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), 7);   
        std::cout << std::string(width - pos, ' ');

        std::cout << "] " << progress << "/" << total << " (" << percent << "%)     ";
        std::cout.flush();
    }
}
