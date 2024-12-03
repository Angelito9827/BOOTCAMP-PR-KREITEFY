using AutoMapper;
using bootcamp_framework.Application.Services;
using bootcamp_pr_kreitefy_api.Application.Dtos;
using bootcamp_pr_kreitefy_api.Domain.Entities;
using bootcamp_pr_kreitefy_api.Domain.Persistence;

namespace bootcamp_pr_kreitefy_api.Application.Services
{
    public class SongService : GenericService<Song, SongDto>, ISongService
    {
        private readonly ISongRepository _songRepository;
        public SongService(ISongRepository songRepository, IMapper mapper) : base(songRepository, mapper)
        {
            _songRepository = songRepository;
        }

        public IEnumerable<RecentSongDto> GetRecentSongs(int count = 5)
        {
            var songs = _songRepository.GetRecentSongs(count);
            return songs;
        }

        public List<SongDto> GetAllSongs()
        {
            return _songRepository.GetAllSongs();
        }
    }
}
