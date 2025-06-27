using Mapper;
using System.Text.Json;
using static DynamicMapEngine.Mapper.Registry.MappingRegistry;
using InternalModels = DynamicMapEngine.Models.Internal;
using ExternalModels = DynamicMapEngine.Models.External;
using DynamicMapEngine.Common.Extensions;

namespace DynamicMapEngine.Tests.Mappers
{
    public class ModelTypeResolverTests
    {
        [Fact]
        public void ResolveType_ValidKey_ReturnsCorrectType()
        {
            // Arrange
            var key = SupportedMappings.InternalRoom.GetDescription();

            // Act
            var type = new ModelTypeResolver().ResolveType(key);

            // Assert
            Assert.Equal(typeof(InternalModels.Room), type);
        }

        [Fact]
        public void ResolveType_InvalidKey_ThrowsArgumentException()
        {
            // Arrange
            var key = "Invalid_Model";

            // Act & Assert
            var ex = Assert.Throws<StatusCodeException>(() => new ModelTypeResolver().ResolveType(key));
            Assert.Contains("Model does not exists", ex.Message);
        }

        [Fact]
        public void CreateInstance_ValidKey_ReturnsInstance()
        {
            // Arrange
            var key = SupportedMappings.InternalRoom.GetDescription();

            // Act
            var instance = new ModelTypeResolver().CreateInstance(key);

            // Assert
            Assert.NotNull(instance);
            Assert.IsType<InternalModels.Room>(instance);
        }

        [Fact]
        public void CreateInstance_InvalidKey_ThrowsArgumentException()
        {
            // Arrange
            var key = "Invalid_Model";

            // Act & Assert
            Assert.Throws<StatusCodeException>(() => new ModelTypeResolver().CreateInstance(key));
        }

        [Fact]
        public void GetSourceObject_ValidJson_ReturnsDeserializedObject()
        {
            // Arrange
            var json = JsonSerializer.Serialize(new ExternalModels.Google.Room { Id = "123", Name = "Test Room" });
            var jsonElement = JsonSerializer.Deserialize<JsonElement>(json);
            var type = typeof(ExternalModels.Google.Room);

            // Act
            var obj = (ExternalModels.Google.Room)new ModelTypeResolver().GetSourceObject(jsonElement, type);

            // Assert
            Assert.NotNull(obj);
            Assert.IsType<ExternalModels.Google.Room>(obj);
            Assert.Equal("123", obj.Id);
            Assert.Equal("Test Room", obj.Name);
        }
    }
}
