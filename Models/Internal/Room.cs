using Models.Attributes;

namespace Models.Internal
{
    public class Room
    {
        [RequiredField]
        public string RoomID { get; set; }
        public string RoomName { get; set; }
        public int NoOfPersons { get; set; }
        public string RoomDescription { get; set; }
        public string RoomNumber { get; set; }
    }
}
