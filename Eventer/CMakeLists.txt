cmake_minimum_required(VERSION 3.10)
project(Eventer)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

# Определение платформозависимых файлов
if(WIN32)
    set(PLATFORM_SOURCES TimeUtils_win.cpp)
    add_definitions(-DPLATFORM_WINDOWS)
else()
    set(PLATFORM_SOURCES TimeUtils_unix.cpp)
    add_definitions(-DPLATFORM_UNIX)
endif()

add_executable(Eventer 
    main.cpp
    Logger.cpp
    ${PLATFORM_SOURCES}
)

target_include_directories(Eventer PRIVATE ${CMAKE_CURRENT_SOURCE_DIR}) 