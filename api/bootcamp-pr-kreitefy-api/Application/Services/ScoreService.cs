using AutoMapper;
using bootcamp_framework.Application.Services;
using bootcamp_pr_kreitefy_api.Application.Dtos;
using bootcamp_pr_kreitefy_api.Domain.Entities;
using bootcamp_pr_kreitefy_api.Domain.Persistence;

namespace bootcamp_pr_kreitefy_api.Application.Services
{
    public class ScoreService : GenericService<Score, ScoreDto>, IScoreService
    {
        private readonly IScoreRepository _scoreRepository;
        private readonly ISongRepository _songRepository;
        public ScoreService(IScoreRepository scoreRepository, IMapper mapper, ISongRepository songRepository) : base(scoreRepository, mapper)
        {
            _scoreRepository = scoreRepository;
            _songRepository = songRepository;
        }

        public ScoreDto AddScore(long userId, long songId, int stars)
        {
            var existingScore = _scoreRepository.GetByUserAndSong(userId, songId);
            if (existingScore != null)
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
            UpdateSongAverageScore(songId, stars);

            return new ScoreDto
            {
                UserId = userId,
                SongId = songId,
                Stars = stars
            };
        }

        public void UpdateSongAverageScore(long songId, int newScore)
        {
            var scores = _scoreRepository.GetScoresBySong(songId);
            if (scores.Count == 0)
            {
                return;
            }

            var totalStars = scores.Sum(s => s.Stars) + newScore;
            var totalScores = scores.Count + 1;

            var averageScore = Math.Round((double)totalStars / totalScores, 2);

            var song = _songRepository.GetById(songId);

            if (song != null)
            {
                song.AverageScore = averageScore;
                _songRepository.Update(song);
            }
        }
    }
}
