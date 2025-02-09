#pragma once
#include <fstream>
#include "event.h"

class Logger {
protected:
    std::ofstream file;
    Logger(const char* fileName);

public:
    virtual ~Logger();
    static Logger* GetLogger(int level);
    virtual void Write(const Event& event) = 0;
};

class Level0Logger : public Logger {
public:
    Level0Logger(const char* fileName);
    virtual void Write(const Event& event) override;
};

class Level1Logger : public Logger {
public:
    Level1Logger(const char* fileName);
    virtual void Write(const Event& event) override;
};

class Level2Logger : public Logger {
public:
    Level2Logger(const char* fileName);
    virtual void Write(const Event& event) override;
};
