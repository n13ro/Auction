using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Lots.Lot;

namespace Infrastructure.Persistence.Repositores.Lots.DTOs
{
    public class LotResponse
    {
        public string Name { get;  set; }
        public string Description { get;  set; }
        public long StartingPrice { get;  set; }
        public long MinBet { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsExtraTime { get; set; }
        public LotStatus Status { get; set; } = LotStatus.Active;
        public TimeSpan LotLife { get; set; }
    }
}
