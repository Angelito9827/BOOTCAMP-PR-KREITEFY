using bootcamp_framework.Application.Services;
using bootcamp_pr_kreitefy_api.Application.Dtos;

namespace bootcamp_pr_kreitefy_api.Application.Services
{
    public interface IHistoryService : IGenericService<HistoryDto>
    {
        HistoryDto IncrementPlayCount(long userId, long songId);
    }
}
