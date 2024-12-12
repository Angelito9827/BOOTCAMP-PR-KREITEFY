using AutoMapper;
using bootcamp_framework.Application;
using bootcamp_framework.Application.Services;
using bootcamp_pr_kreitefy_api.Application.Dtos;
using bootcamp_pr_kreitefy_api.Application.Services;
using bootcamp_pr_kreitefy_api.Domain.Entities;
using bootcamp_pr_kreitefy_api.Domain.Persistence;

public class HistoryService : GenericService<History, HistoryDto>, IHistoryService
{
    private readonly IUserRepository _userRepository;
    private readonly ISongRepository _songRepository;
    private readonly IHistoryRepository _historyRepository;

    public HistoryService(
        IUserRepository userRepository, ISongRepository songRepository, IHistoryRepository historyRepository, IMapper mapper) : base(historyRepository, mapper)
    {
        _userRepository = userRepository;
        _songRepository = songRepository;
        _historyRepository = historyRepository;
    }

    public IEnumerable<RecommendedSongDto> GetRecommendedSongsForUser(long userId)
    {

        return _historyRepository.GetRecommendedSongs(userId);


    }

    public HistoryDto IncrementPlayCount(long userId, long songId)
    {

        var user = _userRepository.GetById(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {userId} not found.");
        }

        var song = _songRepository.GetById(songId);
        if (song == null)
        {
            throw new KeyNotFoundException($"Song with ID {songId} not found.");
        }

        var history = _historyRepository.GetAll()
            .FirstOrDefault(h => h.UserId == userId && h.SongId == songId);

        if (history == null)
        {
            history = new History
            {
                UserId = userId,
                SongId = songId,
                MyPlayCount = 1,
                PlayedAt = DateTime.UtcNow
            };
            _historyRepository.Insert(history);
        }
        else
        {
            history.MyPlayCount++;
            history.PlayedAt = DateTime.UtcNow;
            _historyRepository.Update(history);
        }

        song.TotalPlayCount++;
        _songRepository.Update(song);

        return _mapper.Map<HistoryDto>(history);
    }

    public PagedList<HistoryProfileDto> GetHistorySongs(long userId, PaginationParameters paginationParameters)
    {
        var history = _historyRepository.GetHistorySongs(userId, paginationParameters);
        var historyDtos = history.Select(h => _mapper.Map<HistoryProfileDto>(h)).ToList();
        var pagedList = new PagedList<HistoryProfileDto>(
       historyDtos,
       history.TotalCount,
       history.CurrentPage,
       history.PageSize
   );
        return pagedList;
    }
}
