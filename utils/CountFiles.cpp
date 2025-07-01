#include "CountFiles.h"
#include "BoxMessage.h"
#include "FromBytes.h"
#include "DrawProg.h"
#include "RemoveFile.h"
#include <filesystem>
#include <windows.h>
#include <vector>

namespace fs = std::filesystem;

namespace CountFiles
{
    void GetFileAndSize(const std::string &path)
    {
        int fileCount = 0;
        int folderCount = 0;
        uintmax_t totalSize = 0;

        std::vector<fs::directory_entry> files;
        std::vector<fs::directory_entry> folders;
        try
        {
            for (auto &entry : fs::recursive_directory_iterator(path, fs::directory_options::skip_permission_denied))
            {
                if (entry.is_directory())
                    folders.push_back(entry);
                else if (entry.is_regular_file())
                    files.push_back(entry);
            }
        }
        catch (const std::exception &ex)
        {
            BoxMessage::ShowError("Scan error: " + std::string(ex.what()));
            return;
        }
        int totalFiles = files.size();
        int totalFolders = folders.size();

        for (int i = 0; i < totalFiles; ++i)
        {
            const auto &entry = files[i];
            try
            {
                DWORD attr = GetFileAttributesW(entry.path().c_str());

                if (attr != INVALID_FILE_ATTRIBUTES &&
                    (attr & (FILE_ATTRIBUTE_HIDDEN | FILE_ATTRIBUTE_SYSTEM)))
                {
                    BoxMessage::ShowError("Protected file: " + entry.path().string());
                    continue;
                }

                totalSize += entry.file_size();
                fileCount++;
            }
            catch (...)
            {
                BoxMessage::ShowError("Error accessing: " + entry.path().string());
            }

            DrawProg::ProgressBar(fileCount, totalFiles, "Files:", 2);
        }

        for (int i = 0; i < totalFolders; ++i)
        {
            const auto &entry = folders[i];
            try
            {
                folderCount++;
            }
            catch (...)
            {
                BoxMessage::ShowError("Error accessing folder: " + entry.path().string());
            }

            DrawProg::ProgressBar(folderCount, totalFolders, "Folders:", 3);
        }

        BoxMessage::ShowDate("Total Files: " + std::to_string(fileCount) +
                             " | Folders: " + std::to_string(folderCount) +
                             " | Size: " + FromBytes::FormatBytes(totalSize));

        std::string input;
        std::cout << "You wanna clean TEMP Folder? Push (Y) to remove. (N) to cancel\n> ";
        std::getline(std::cin, input);

        if (input.empty())
        {
            BoxMessage::ShowError("No data was entered");
            std::cout << "Push any key to close \n";
            std::cin.get();
            return;
        }
        if (input == "Y" || input == "y")
        {
            RemoveFile::RemoveAll(path, fileCount, folderCount);
            std::cout << "\n Temp was cleaning, put ENTER to close the terminal...";
            std::cin.get();
            return;
        }
        else if (input == "N" || input == "n")
        {
            BoxMessage::ShowInfo("Cancel Operation");
            std::cout << "Push ENTER to close...\n";
            std::cin.get();
            return;
        }
    }
}
