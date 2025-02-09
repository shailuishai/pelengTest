#include "logger.h"
#include <ctime>

Logger::Logger(const char* fileName) {
    file.open(fileName, std::ios::app);
}

Logger::~Logger() {
    if (file.is_open()) {
        file.close();
    }
}

Logger* Logger::GetLogger(int level) {
    switch (level) {
    case 0:
        return new Level0Logger("log0.txt");
    case 1:
        return new Level1Logger("log1.txt");
    case 2:
        return new Level2Logger("log2.txt");
    default:
        return nullptr;
    }
}

Level0Logger::Level0Logger(const char* fileName) : Logger(fileName) {}

void Level0Logger::Write(const Event& event) {
    char timeStr[26];
#ifdef _WIN32
    ctime_s(timeStr, sizeof(timeStr), &event.timestamp);  // Windows version
#else
    ctime_r(&event.timestamp, timeStr);  // Linux version
#endif
    file << "Time: " << timeStr << "Event ID: " << event.id << std::endl;
}

Level1Logger::Level1Logger(const char* fileName) : Logger(fileName) {}

void Level1Logger::Write(const Event& event) {
    char timeStr[26];
#ifdef _WIN32
    ctime_s(timeStr, sizeof(timeStr), &event.timestamp);  // Windows version
#else
    ctime_r(&event.timestamp, timeStr);  // Linux version
#endif
    file << "Time: " << timeStr
        << "Event ID: " << event.id
        << " Param1: " << event.param1 << std::endl;
}

Level2Logger::Level2Logger(const char* fileName) : Logger(fileName) {}

void Level2Logger::Write(const Event& event) {
    char timeStr[26];
#ifdef _WIN32
    ctime_s(timeStr, sizeof(timeStr), &event.timestamp);  // Windows version
#else
    ctime_r(&event.timestamp, timeStr);  // Linux version
#endif
    file << "Time: " << timeStr
        << "Event ID: " << event.id
        << " Param1: " << event.param1
        << " Param2: " << event.param2
        << " Param3: " << event.param3 << std::endl;
}