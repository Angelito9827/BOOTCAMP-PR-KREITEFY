using AutoMapper;
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

        var histories = _historyRepository.GetAllByUserId(userId);

        var topTwoStyles = histories
            .Where(h => h.Song != null)
            .GroupBy(h => h.Song.StyleId)
            .Select(g => new
            {
                StyleId = g.Key,
                TotalPlayCount = g.Sum(h => h.MyPlayCount)
            })
            .OrderByDescending(g => g.TotalPlayCount)
            .Take(2)
            .Select(g => g.StyleId)
            .ToList();

        var recommendedSongs = _historyRepository.GetRecommendedSongsByStyles(topTwoStyles);

        return _mapper.Map<IEnumerable<RecommendedSongDto>>(recommendedSongs);

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
        }

        song.TotalPlayCount++;
        _songRepository.Update(song);

        return _mapper.Map<HistoryDto>(history);
    }
}
