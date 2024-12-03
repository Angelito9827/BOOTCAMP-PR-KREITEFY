import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RecentSongDto } from '../../../model/recent-song.model';
import { SongService } from '../../../services/song.service';
import { response } from 'express';

@Component({
  selector: 'app-recent-songs-card',
  templateUrl: './recent-songs-card.component.html',
  styleUrl: './recent-songs-card.component.scss',
  standalone: true,
  imports:[CommonModule]
})
export class RecentSongsCardComponent {

  recentSongs: RecentSongDto[] = [];

  constructor(
    private songService: SongService
  ) {}

  ngOnInit(): void {
    this.getRecentSongs();    
  }

  private getRecentSongs(): void {
    this.songService.getRecentSongs().subscribe({
      next:(response: RecentSongDto[]) => {
        this.recentSongs = response},
        error:(err) => {
          console.error("Error getting recent songs")
        }
    }) 
  }
}
