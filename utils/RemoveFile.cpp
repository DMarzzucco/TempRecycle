#include "RemoveFile.h"
#include "BoxMessage.h"
#include <filesystem>
#include <windows.h>
#include <iostream>

namespace fs = std::filesystem;

namespace RemoveFile {
    void RemoveAll(const std::string& path, int fileCount, int folderCount) {
        std::string input;
        std::cout << "\nDo you want to delete the scanned files? (Y/N): ";
        std::getline(std::cin, input);

        if (input.empty()) {
            BoxMessage::ShowError("No data was written");
            return;
        }

        if (input != "Y" && input != "y") {
            BoxMessage::ShowInfo("Cancel Operation");
            return;
        }

        // Delete files
        for (auto& entry : fs::recursive_directory_iterator(path,
                 fs::directory_options::skip_permission_denied)) {
            try {
                if (entry.is_regular_file()) {
                    std::string pathStr = entry.path().string();
                    DWORD attr = GetFileAttributesW(entry.path().c_str());

                    if (attr != INVALID_FILE_ATTRIBUTES &&
                        (attr & (FILE_ATTRIBUTE_HIDDEN | FILE_ATTRIBUTE_SYSTEM))) {
                        continue;
                    }

                    fs::remove(entry.path());
                    BoxMessage::ShowInfo("Deleted file: " + pathStr);
                }
            } catch (const std::exception& ex) {
                BoxMessage::ShowError("Could not delete file: " + std::string(ex.what()));
            }
        }

        // Delete empty folders
        for (auto it = fs::recursive_directory_iterator(path,
                     fs::directory_options::skip_permission_denied);
             it != fs::recursive_directory_iterator(); ++it) {
            try {
                if (it->is_directory() && fs::is_empty(*it)) {
                    fs::remove(*it);
                    BoxMessage::ShowInfo("Deleted folder: " + it->path().string());
                }
            } catch (const std::exception& ex) {
                BoxMessage::ShowError("Could not delete folder: " + std::string(ex.what()));
            }
        }

        BoxMessage::ShowInfo("Cleanup completed.");
    }
}
