#include "FromBytes.h"
#include <sstream>
#include <iomanip>
#include <cstdint> 

namespace FromBytes {
    std::string FormatBytes(uintmax_t bytes) {
        const char* suffixes[] = {"B", "KB", "MB", "GB", "TB"};
        size_t i = 0;
        double size = static_cast<double>(bytes);

        while (size >= 1024 && i < 4) {
            size /= 1024;
            ++i;
        }

        std::ostringstream oss;
        oss << std::fixed << std::setprecision(2) << size << " " << suffixes[i];
        return oss.str();
    }
}
