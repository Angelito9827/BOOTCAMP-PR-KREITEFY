﻿namespace bootcamp_pr_kreitefy_api.Application.Dtos
{
    public class AlbumDto
    {
        public long Id { get; set; }

        public required string Name { get; set; }

        public required byte[] Image { get; set; }
    }
}