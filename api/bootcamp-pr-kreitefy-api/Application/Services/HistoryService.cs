using AutoMapper;
using bootcamp_framework.Application.Services;
using bootcamp_pr_kreitefy_api.Application.Dtos;
using bootcamp_pr_kreitefy_api.Domain.Entities;
using bootcamp_pr_kreitefy_api.Domain.Persistence;

namespace bootcamp_pr_kreitefy_api.Application.Services
{
    public class HistoryService : GenericService<History, HistoryDto>, IHistoryService
    {
        private readonly IHistoryRepository _historyRepository;
        public HistoryService(IHistoryRepository historyRepository, IMapper mapper) : base(historyRepository, mapper)
        {
            _historyRepository = historyRepository;
        }

        public HistoryDto IncrementPlayCount(long userId, long songId)
        {
            _historyRepository.IncrementPlayCount(userId, songId);

            var updatedHistory = _historyRepository.GetAll()
               .FirstOrDefault(h => h.UserId == userId && h.SongId == songId);

            if (updatedHistory == null)
                throw new Exception("Failed to retrieve updated history.");

            return _mapper.Map<HistoryDto>(updatedHistory);
        }
    }
}
