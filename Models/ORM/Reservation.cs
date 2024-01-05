namespace TechCarreerBootcampExam.Models.ORM
{
    public class Reservation : BaseModel
    {
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public ReservationStatus Status { get; set; }
    }


    public enum ReservationStatus
    {
        Pending = 0,
        Approved = 1,
        Canceled = 2,
        Completed = 3
    }
}
