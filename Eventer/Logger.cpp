#include "logger.h"
#include "TimeUtils.h"
#include <ctime>
#include <optional>

Logger::Logger(std::string_view fileName) {
    file.open(std::string(fileName), std::ios::app);
}

Logger::~Logger() {
    if (file.is_open()) {
        file.close();
    }
}

 std::optional<Logger*> Logger::GetLogger(int level) { 
    switch (level) {
    case 0:
        return new Level0Logger("log0.txt");
    case 1:
        return new Level1Logger("log1.txt");
    case 2:
        return new Level2Logger("log2.txt");
    default:
        return std::nullopt; // С++17 nullptr -> std::nullopt
    }
}

void Logger::WriteTimeAndId(std::ostream& out, const Event& event) {
    std::string timeStr;
    timeStr = TimeUtils::getCurrentTimeString();
    out << "Time: " << timeStr << "Event ID: " << event.id;
}

Level0Logger::Level0Logger(std::string_view fileName) : Logger(fileName) {}

void Level0Logger::Write(const Event& event) {
    WriteTimeAndId(file, event);
    file << std::endl;
}

Level1Logger::Level1Logger(std::string_view fileName) : Logger(fileName) {}

void Level1Logger::Write(const Event& event) {
    WriteTimeAndId(file, event);
    file << " Param1: " << event.param1 << std::endl;
}

Level2Logger::Level2Logger(std::string_view fileName) : Logger(fileName) {}

void Level2Logger::Write(const Event& event) {
    WriteTimeAndId(file, event);
    file << " Param1: " << event.param1
         << " Param2: " << event.param2
         << " Param3: " << event.param3 << std::endl;
}