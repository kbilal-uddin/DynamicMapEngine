using Models.Attributes;

namespace Models.External.Google
{
    public class Reservation
    {
        [RequiredField]
        public int Id { get; set; }
        public Guest GuestInfo { get; set; }
        public Room RoomInfo { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
