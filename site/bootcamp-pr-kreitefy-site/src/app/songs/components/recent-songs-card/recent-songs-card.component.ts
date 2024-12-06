import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RecentSongDto } from '../../model/recent-song.model';
import { SongService } from '../../services/song.service';

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
    private songService: SongService,
    private router : Router
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

  navigateToProductDetails(songId: number) {
    this.router.navigate(["/songs/",songId])
  }
}
