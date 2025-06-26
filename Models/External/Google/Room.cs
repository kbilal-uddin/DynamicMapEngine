using Models.Attributes;

namespace Models.External.Google
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
