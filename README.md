#  DynamicMapEngine
DynamicMapEngine is a modular, .NET 8 API-based object mapping system that facilitates safe and predictable transformations between external (e.g., third-party or partner) models and internal domain models — and vice versa.

## Technical Overview
- Platform: .NET 8
- Database: None — this project is fully in-memory and stateless.
- Execution Model: Synchronous, as no I/O or external service dependencies are involved.
- Architecture: Clean layered structure with Controller → Processor → Handler, all wired through Dependency Injection (DI).
- Dynamic Resolution: Uses reflection to load the appropriate mapper at runtime.

This project is ideal for systems requiring flexible and extensible model transformation pipelines without tightly coupling transformation logic.

##  Features

- Bi-directional mapping (e.g., Google → Internal, Internal → Google)
- Static and dynamic validation (pre- and post-mapping)
- Extensible mapper registry system
- Modular, test-driven architecture
- Dependency Injection enabled for flexible and testable components
- Ready for integration with external APIs and partners

---

## Architecture Overview
The DynamicMapEngine project is designed with clean code principles and modularity in mind. Below are the core design patterns and implementation choices:

### Clean Architecture Layers & DI
Controller → Processor → Handler
This separation ensures a clean flow of responsibility:

- Controller receives HTTP requests, depends on interfaces and services injected by DI.
- Processor orchestrates mapping logic and validation, injected with handlers.
- Handler executes dynamic mapping logic, injected into processors.

### Interface-Driven Design
Core components (IMapHandler, IMapProcessor, IObjectMapper<TSource, TTarget>, IModelTypeResolver, IMapperFactory) are structured using interfaces. This promotes:

- Loose coupling
- Easier unit testing via mocks or fakes
- Flexible swapping of implementations without code changes

### Mapper Factory & Dynamic Mapping Resolution
- The MapperFactory dynamically resolves the correct IObjectMapper<TSource, TTarget> implementation via reflection at runtime.
- This enables plug-and-play mappers for various sources without hardcoded dependencies.

### Dynamic Mapping Resolution
The correct IObjectMapper<TSource, TTarget> implementation is resolved dynamically via reflection at runtime, enabling plug-and-play mappers for different sources (e.g., Google, Facebook).

### Unit Testing
- Comprehensive xUnit tests cover mappers, handlers, validation, and model resolution.
- DI enables easy mocking of dependencies for isolated tests.

### Centralized Error Handling
A custom ExceptionHandlingMiddleware is used to handle:

- Known application exceptions (e.g., invalid model input)
- Unhandled runtime exceptions (to prevent leaking stack traces)

### Postman Collection
A Postman collection is included for end-to-end testing of API endpoints, covering major input scenarios. See Postman/Dynamic Mapping Engine API - Test Collection.postman_collection.json for API examples and payload structures.

### Key Benefits
- Extensible: Easily add new mappers or source systems.
- Clean and maintainable codebase.
- Testable by design, from unit to integration level.
- Dynamic yet safe mapping resolution using reflection + validation.
---

## Design Assumptions
The dynamic factory pattern (powered by reflection) is implemented with the assumption that the system will eventually support hundreds of mappers across various partner models and internal domains.

This design ensures:
- Scalability: New mappers can be added without modifying core logic.
- Loose coupling: No hard-coded references to mapper classes.
- Plug-and-play extensibility: Simply implement IObjectMapper<TSource, TTarget> and register the models — the factory handles the rest.

## Mapping Workflow

Example Use Cases
- Transform guest/reservation/room data from Google APIs to internal models.
- Validate and sanitize incoming payloads before saving or processing.
- Easily extendable for new partners or mapping targets.

## Adding Support for a New Mapping
### 1. Define Models
- Create your source models inside Models Class Library 
- Organize models according to the source context and required access level.
  
### 2. Implement Mappers
- Implement the mapping classes for converting between external and internal models:
- From<PartnerName><ModelName>Mapper (external → internal)
- To<PartnerName><ModelName>Mapper (internal → external)
- These should implement the [IObjectMapper<TSource, TTarget>](https://github.com/kbilal-uddin/DynamicMapEngine/blob/main/MappingEngine.Core/Interfaces/IObjectMapper.cs) interface and include validation as per the [RequiredField] attributes.

### 3. Register Supported Mappings
- Register the ENUMS here [SupportedMappings](https://github.com/kbilal-uddin/DynamicMapEngine/blob/main/MappingEngine.Core/Registry/MappingRegistry.cs#L8) in MappingRegistry.cs.
- This enables the mapping engine to recognize your new source and target model pairs.

### 4. Update Model Type Resolver
- Map your enum to the appropriate mapper class in: [ModelTypeResolver.cs](https://github.com/kbilal-uddin/DynamicMapEngine/blob/main/MappingEngine.Core/ModelTypeResolver.cs#L14)

These step ensures the engine uses the correct mapper implementation during runtime.
