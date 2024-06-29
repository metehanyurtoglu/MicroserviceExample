# MicroserviceExample

This is a sample E-Commerce project utilizing multiple microservice architectures.

## Overview

This project demonstrates a comprehensive approach to building a scalable E-Commerce system using a microservices architecture. Each microservice is designed to handle specific business functionalities, ensuring modularity, maintainability, and scalability.

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

**Other microservice projects will be add in time.**

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
    - Catalog API: 
    ```sh
    http://localhost:6000/swagger/index.html
    https://localhost:6060/swagger/index.html
    ```

## Usage

### API Documentation

API endpoints and usage details are available in the Swagger index.html

Also you can check status usin by /health route.
