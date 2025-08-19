using static Domain.Bids.Bid;

namespace Application.Services.BidService.DTOs
{
    public class BidResponse
    {
        public long Amount { get; set; }

        public DateTime PlacedAt { get; set; }
        public BidStatus Status { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
