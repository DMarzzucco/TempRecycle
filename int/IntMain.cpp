#pragma once
#include <iostream>
#include <cstdlib>
#include <string>
#include "../Utils/BoxMessage.h"
#include "../Utils/CountFiles.h"
#include "../Utils/Logo.h"


namespace IntMain
{
    int ImplementInt()
    {
        std::string tempPath;
        const char *tempEnv = std::getenv("TEMP");

        if (!tempEnv)
        {
            BoxMessage::ShowError("TEMP is null");
            return 1;
        }

        tempPath = tempEnv;


        std::string input;

        Logo::ShowLogo();
        std::cout << "Push (E) to scan. (N) to cancel\n> ";
        std::getline(std::cin, input);

        if (input.empty())
        {
            BoxMessage::ShowError("No data was entered");
            std::cout << "Push ENTER to close...\n";
            std::cin.get();
            return 1;
        }

        if (input == "E" || input == "e")
        {
            system("cls");
            CountFiles::GetFileAndSize(tempPath);
        }
        else if (input == "N" || input == "n")
        {
            BoxMessage::ShowInfo("Cancel Operation");
            std::cout << "Push ENTER to close...\n";
            std::cin.get();
            return 1;
        }
        else
        {
            BoxMessage::ShowError("Failed Operation");
        }

        return 0;
    }
}
