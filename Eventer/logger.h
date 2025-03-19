#pragma once
#include <fstream>
#include <optional>
#include "event.h"

class Logger {
protected:
    std::ofstream file;
    Logger(std::string_view /* С++17 const char* -> std::string_view */ fileName);
    void WriteTimeAndId(std::ostream& out, const Event& event);
public:
    virtual ~Logger();
    static std::optional<Logger*> GetLogger(int level);
    virtual void Write(const Event& event) = 0;
};

class Level0Logger : public Logger {
public:
    Level0Logger(std::string_view fileName);
    virtual void Write(const Event& event) override;
};

class Level1Logger : public Logger {
public:
    Level1Logger(std::string_view fileName);
    virtual void Write(const Event& event) override;
};

class Level2Logger : public Logger {
public:
    Level2Logger(std::string_view fileName);
    virtual void Write(const Event& event) override;
};
