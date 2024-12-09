using AutoMapper;
using bootcamp_framework.Application.Services;
using bootcamp_pr_kreitefy_api.Application.Dtos;
using bootcamp_pr_kreitefy_api.Application.Services;
using bootcamp_pr_kreitefy_api.Domain.Entities;
using bootcamp_pr_kreitefy_api.Domain.Persistence;

public class ScoreService : GenericService<Score, ScoreDto>, IScoreService
{
    private readonly IScoreRepository _scoreRepository;
    private readonly ISongRepository _songRepository;

    public ScoreService(IScoreRepository scoreRepository, IMapper mapper, ISongRepository songRepository)
        : base(scoreRepository, mapper)
    {
        _scoreRepository = scoreRepository;
        _songRepository = songRepository;
    }

    public ScoreDto AddScore(long userId, long songId, int stars)
    {
        if (UserHasAlreadyRated(userId, songId))
        {
            throw new InvalidOperationException("El usuario ya ha puntuado esta canción.");
        }

        var score = new Score
        {
            UserId = userId,
            SongId = songId,
            Stars = stars
        };

        _scoreRepository.Add(score);

        UpdateSongAverageScore(songId);

        return _mapper.Map<ScoreDto>(score);
    }

    private bool UserHasAlreadyRated(long userId, long songId)
    {
        return _scoreRepository.GetByUserAndSong(userId, songId) != null;
    }

    public void UpdateSongAverageScore(long songId)
    {
        var scores = _scoreRepository.GetScoresBySong(songId);
        if (!scores.Any())
        {
            return;
        }

        var averageScore = CalculateAverageScore(scores);

        var song = _songRepository.GetById(songId);
        if (song == null)
        {
            throw new KeyNotFoundException($"Song with ID {songId} not found.");
        }

        song.AverageScore = averageScore;
        _songRepository.Update(song);
    }

    private double CalculateAverageScore(IEnumerable<Score> scores)
    {
        return Math.Round(scores.Average(s => s.Stars), 2);
    }
}
