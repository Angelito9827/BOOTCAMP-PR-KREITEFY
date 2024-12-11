import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RecentSongDto } from '../../model/recent-song.model';
import { SongService } from '../../service/song.service';
import { Router } from '@angular/router';
import { AuthService } from '../../../auth/service/auth/auth.service';
import { RecommendedSongDto } from '../../model/recommended-song.model';

@Component({
  selector: 'app-songs-for-me',
  templateUrl: './songs-for-me.component.html',
  styleUrl: './songs-for-me.component.scss',
  standalone: true,
  imports:[CommonModule]
})
export class SongsForMeComponent {
songs: RecommendedSongDto[] = [];
userId: string = '';

constructor(
  private songService: SongService,
  private router : Router,
  private authService: AuthService
) {}

ngOnInit(): void {
  this.authService.getUserIdObservable().subscribe(id => {
    this.userId = id;
  });
  this.getSongsForMe();
   
}

private getSongsForMe(): void {
  this.songService.getRecommendedSongs(this.userId).subscribe({
    next:(response: RecommendedSongDto[]) => {
      this.songs = response},
      error:(err) => {
        console.error("Error getting recent songs")
      }
  }) 
}

navigateToSongDetails(songId: number) {
  this.router.navigate(["/songs/",songId])
}
}
