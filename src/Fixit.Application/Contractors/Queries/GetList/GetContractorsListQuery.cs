using Fixit.Shared.CQRS;
using Fixit.Shared.Pagination;

namespace Fixit.Application.Contractors.Queries.GetList
{
    public class GetContractorsListQuery : IQuery<PaginatedResult<ContractorForList>>
    {
        public ContractorsListFilter ContractorsListFilter { get; set; }
        public PagingParams PagingParams { get; set; }
    }
}