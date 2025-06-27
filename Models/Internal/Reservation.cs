namespace DynamicMapEngine.Models.Internal
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public Guest Guest { get; set; }
        public Room Room { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
