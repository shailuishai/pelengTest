#include "TimeUtils.h"
#include <ctime>
#include <iomanip>
#include <sstream>

namespace TimeUtils {

    std::string getCurrentTimeString() {
        std::time_t now = std::time(nullptr);
        char buffer[20];
        std::strftime(buffer, sizeof(buffer), "%Y-%m-%d %H:%M:%S", std::localtime(&now));
        return std::string(buffer);
    }

    std::string getCurrentDateString() {
        std::time_t now = std::time(nullptr);
        char buffer[11];
        std::strftime(buffer, sizeof(buffer), "%Y-%m-%d", std::localtime(&now));
        return std::string(buffer);
    }

} // namespace TimeUtils