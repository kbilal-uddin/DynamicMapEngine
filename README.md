#  DynamicMapEngine

**DynamicMapEngine** is a modular, .NET-based object mapping system that facilitates safe and predictable transformations between external (e.g., third-party or partner) models and internal domain models — and vice versa.

---

##  Features

-  Bi-directional mapping (e.g., Google → Internal, Internal → Google)
-  Static and dynamic validation (pre- and post-mapping)
-  Extensible mapper registry system
-  Modular, test-driven architecture
-  Ready for integration with external APIs and partners

---

## Project Structure


---

## Mapping Workflow

Example Use Cases
- Transform guest/reservation/room data from Google APIs to internal models.
- Validate and sanitize incoming payloads before saving or processing.
- Easily extendable for new partners or mapping targets.

## Testing
Unit tests are organized by:

- Domain (Guest, Room, Reservation)
- Direction (From / To)
- Run tests via Visual Studio Test Explorer or dotnet test CLI.

## Postman Collection
See Postman/Dynamic Mapping Engine API - Test Collection.postman_collection.json for API examples and payload structures.

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
