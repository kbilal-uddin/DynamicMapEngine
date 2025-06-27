using DynamicMapEngine.Models.Attributes;

namespace DynamicMapEngine.Models.External.Google
{
    public class Room
    {
        public string Id { get; set; }
        public string Name { get; set; }

        [RequiredField]
        public int Capacity { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
    }
}
