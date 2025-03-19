#include "TimeUtils.h"
#include <windows.h>
#include <cstdio>

namespace TimeUtils {

    // Внутренняя функция для форматирования времени
    void formatTime(char* buffer, size_t bufferSize, const SYSTEMTIME* time) {
        snprintf(buffer, bufferSize, "%04d-%02d-%02d %02d:%02d:%02d",
                 time->wYear, time->wMonth, time->wDay,
                 time->wHour, time->wMinute, time->wSecond);
    }

    std::string getCurrentTimeString() {
        SYSTEMTIME systemTime;
        GetLocalTime(&systemTime);

        char buffer[20];
        formatTime(buffer, sizeof(buffer), &systemTime);
        return std::string(buffer);
    }

    std::string getCurrentDateString() {
        SYSTEMTIME systemTime;
        GetLocalTime(&systemTime);

        char buffer[11];
        snprintf(buffer, sizeof(buffer), "%04d-%02d-%02d",
                 systemTime.wYear, systemTime.wMonth, systemTime.wDay);
        return std::string(buffer);
    }

} // namespace TimeUtils