#include <iostream>
#include <map>
#include <string>
#include <thread>
#include <mutex>
#include <queue>
#include <random>
#include <chrono>
#include <algorithm>
#include "logger.h"

std::queue<Event> eventQueue;
std::mutex queueMutex;
std::mutex loggerMutex;
bool running = true;
bool paused = false;
int eventInterval = 1000;
int eventCount = 0;
Logger* currentLogger = nullptr;

void GenerateEvents() {
    std::random_device rd;
    std::mt19937 gen(rd());
    std::uniform_int_distribution<> dis(1, 100);

    while (running) {
        if (!paused) {
            Event event;
            event.timestamp = std::time(nullptr);
            event.id = dis(gen);
            event.param1 = dis(gen);
            event.param2 = dis(gen);
            event.param3 = dis(gen);

            {
                std::lock_guard<std::mutex> lock(queueMutex);
                eventQueue.push(event);
                eventCount++;
            }
        }
        std::this_thread::sleep_for(std::chrono::milliseconds(eventInterval));
    }
}

void LogEvents() {
    while (running) {
        Event event;
        bool hasEvent = false;
        {
            std::lock_guard<std::mutex> lock(queueMutex);
            if (!eventQueue.empty()) {
                event = eventQueue.front();
                eventQueue.pop();
                hasEvent = true;
            }
        }

        if (hasEvent) {
            std::lock_guard<std::mutex> logLock(loggerMutex);
            if (currentLogger) {
                currentLogger->Write(event);
            }
        }

        std::this_thread::sleep_for(std::chrono::milliseconds(100));
    }
}

void PrintDate(const char*) {
    time_t now = time(nullptr);
    char dateStr[26];
    ctime_s(dateStr, sizeof(dateStr), &now);
    std::cout << "Current date: " << dateStr;
}

void PrintTime(const char*) {
    auto now = std::chrono::system_clock::now();
    time_t tt = std::chrono::system_clock::to_time_t(now);
    char timeStr[26];
    ctime_s(timeStr, sizeof(timeStr), &tt);
    std::cout << "Current time: " << timeStr;
}

void SetLevel(std::string_view param) {
    if (param.empty()) {
        std::cerr << "Level parameter required" << std::endl;
        return;
    }

    std::string paramStr(param);
    paramStr.erase(std::remove_if(paramStr.begin(), paramStr.end(), ::isspace), paramStr.end());

    try {
        int level = std::stoi(paramStr);
        if (level < 0 || level > 2) {
            std::cerr << "Invalid level. Must be 0, 1, or 2" << std::endl;
            return;
        }

        {
            std::lock_guard<std::mutex> logLock(loggerMutex);
            auto newLogger = Logger::GetLogger(level);
            if (newLogger) {
                delete currentLogger;
                currentLogger = newLogger.value();
                std::cout << "Logging level changed to " << level << std::endl;
            }
        }
    }
    catch (const std::invalid_argument&) {
        std::cerr << "Invalid level format" << std::endl;
    }
    catch (const std::out_of_range&) {
        std::cerr << "Level value out of range" << std::endl;
    }
}

void PrintStat(const char*) {
    std::cout << "Total events generated: " << eventCount << std::endl;
}

int main() {
    std::map<std::string, void(*)(const char*)> commands;

    commands["date"] = PrintDate;
    commands["time"] = PrintTime;
    commands["stat"] = PrintStat;

    std::string levelPrefix = "level ";

    auto loggerOpt = Logger::GetLogger(0);
    if (!loggerOpt) {
        std::cerr << "Failed to initialize logger" << std::endl;
        return 1;
    }
    currentLogger = loggerOpt.value();

    std::thread eventGenerator(GenerateEvents);
    std::thread eventLogger(LogEvents);

    std::string input;
    while (running) {
        std::getline(std::cin, input);

        if (input == "exit") {
            running = false;
            break;
        }
        else if (input == "faster") {
            eventInterval = std::max(100, eventInterval - 100);
            std::cout << "Event interval decreased to " << eventInterval << "ms" << std::endl;
        }
        else if (input == "slower") {
            eventInterval += 100;
            std::cout << "Event interval increased to " << eventInterval << "ms" << std::endl;
        }
        else if (input == "pause") {
            paused = true;
            std::cout << "Event generation paused" << std::endl;
        }
        else if (input == "resume") {
            paused = false;
            std::cout << "Event generation resumed" << std::endl;
        }
        else if (input.compare(0, levelPrefix.length(), levelPrefix) == 0) {
            std::string_view levelStr = std::string_view(input).substr(levelPrefix.length()); //C+++17 
            SetLevel(levelStr);
        }
        else {
            auto it = commands.find(input);
            if (it != commands.end()) {
                it->second(nullptr);
            }
            else {
                std::cerr << "Unknown command: " << input << std::endl;
            }
        }
    }

    eventGenerator.join();
    eventLogger.join();

    delete currentLogger;
    return 0;
}