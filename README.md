# MicroserviceExample

This is a sample E-Commerce project utilizing multiple microservice projects.
****

## Overview

This project demonstrates a comprehensive approach to building a scalable E-Commerce system using a microservices architecture. Each microservice is designed to handle specific business functionalities, ensuring modularity, maintainability, and scalability.

**✨ Other microservice projects will be added in time.**

## Microservices

### Core.Application
The `Core.Application` project contains shared behaviors and libraries used across all microservice projects. This includes common utilities, base classes, and shared domain logic, ensuring consistency and reducing redundancy.

### Catalog.API
The `Catalog.API` service is responsible for managing the product catalog. It leverages the .NET 8 framework and follows the Vertical Slice Architecture, enabling clear separation of concerns and modular design. The service is containerized using Docker and can be orchestrated with Docker Compose.

#### Key Features:
- **CQRS Implementation**: Utilizes the MediatR library for Command and Query Responsibility Segregation (CQRS) pattern.
- **Pipeline Behaviors**: Incorporates MediatR and FluentValidation for request validation and other cross-cutting concerns.
- **Transactional Document DB**: Uses Marten library for transactional document database management on PostgreSQL.
- **Minimal API Endpoints**: Defines API endpoints using Carter for minimal and efficient API routing.
- **Cross-Cutting Concerns**: Implements logging, global exception handling, and health checks for robust and reliable operations.

### Basket.API
The `Basket.API` service is responsible for managing the shopping basket. It leverages the .NET 8 framework and follows the Vertical Slice Architecture. The service is containerized using Docker and can be orchestrated with Docker Compose.

#### Key Features:
- **CQRS Implementation**: Utilizes the MediatR library for Command and Query Responsibility Segregation (CQRS) pattern.
- **Pipeline Behaviors**: Incorporates MediatR and FluentValidation for request validation and other cross-cutting concerns.
- **Transactional Document DB**: Uses Marten library for transactional document database management on PostgreSQL.
- **Minimal API Endpoints**: Defines API endpoints using Carter for minimal and efficient API routing.
- **Cross-Cutting Concerns**: Implements logging, global exception handling, and health checks for robust and reliable operations.
- **Distributed Cache**: Uses Redis as a distributed cache over basketdb.
- **Design Patterns**: Implements Proxy, Decorator, and Cache-aside patterns.

### Discount.gRPC
The `Discount.gRPC` service is responsible for managing discounts. It leverages the .NET 8 framework and follows the N-Layer Architecture. The service is containerized using Docker and can be orchestrated with Docker Compose.

#### Key Features:
- **ASP.NET gRPC Service Application**: Ensures highly performant inter-service gRPC communication between Discount and Basket microservices.
- **gRPC Communications and Proto Files**: Handles CRUD operations through gRPC by defining Protobuf messages.
- **SQLite Database Connection**: Establishes a connection to an SQLite database, containerized for ease of deployment.
- **Entity Framework Core ORM**: Utilizes the SQLite Data Provider and migrations for simplified data access and high performance.
- **N-Layer Architecture Implementation**: Follows the N-Layer architecture for a well-structured and maintainable codebase.
- **Containerization with Docker Compose**: The Discount microservice, along with its SQLite database, can be orchestrated using Docker Compose.
- **JSON Transcoding and Swagger Support**: Supports HTTP calls with JSON transcoding and utilizes the Swashbuckle library to provide Swagger documentation, enabling both gRPC and HTTP API calls.

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/products/docker-desktop)
- [PostgreSQL](https://www.postgresql.org/download/)

### Installation

1. **Clone the Repository**
    ```sh
    git clone https://github.com/metehanyurtoglu/MicroserviceExample.git
    cd your-repo
    ```

2. **Build and Run Services**
    ```sh
    docker-compose up --build
    ```

3. **Accessing the Services**
    - Catalog API
    ```sh
    http://localhost:6000/swagger/index.html
    https://localhost:6060/swagger/index.html
    ```

    - Basket API
    ```sh
    http://localhost:6001/swagger/index.html
    https://localhost:6061/swagger/index.html
    ```
    
    - Discount gRPC
    ```sh
    http://localhost:6002/swagger/index.html
    https://localhost:6062/swagger/index.html
    ```

## Usage

### API Documentation

API endpoints and usage details are available in the Swagger index.html

Also you can check status using by /health route.
