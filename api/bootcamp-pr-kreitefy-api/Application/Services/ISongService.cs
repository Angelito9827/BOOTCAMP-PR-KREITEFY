﻿using bootcamp_framework.Application.Services;
using bootcamp_pr_kreitefy_api.Application.Dtos;

namespace bootcamp_pr_kreitefy_api.Application.Services
{
    public interface ISongService : IGenericService<SongDto>
    {
        List<SongDto> GetAllSongs();
        IEnumerable<RecentSongDto> GetRecentSongs(int count = 5);
    }
}
