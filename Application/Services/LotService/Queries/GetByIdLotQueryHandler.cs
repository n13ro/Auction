using Application.Services.LotService.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.LotService.Queries
{
    public class GetByIdLotQuery : IRequest<LotResponse>;

    public class GetByIdLotQueryHandler : IRequestHandler<GetByIdLotQuery, LotResponse>
    {
        public Task<LotResponse> Handle(GetByIdLotQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
