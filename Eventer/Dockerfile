FROM ubuntu:22.04 AS build

RUN apt-get update && apt-get install -y \
    g++ \
    cmake \
    build-essential \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /src
COPY . .

RUN rm -rf build && mkdir build && cd build && \
    cmake .. && \
    cmake --build .

FROM ubuntu:22.04
WORKDIR /app

RUN apt-get update && apt-get install -y \
    libstdc++6 \
    && rm -rf /var/lib/apt/lists/*

COPY --from=build /src/build/Eventer .

ENTRYPOINT ["./Eventer"]