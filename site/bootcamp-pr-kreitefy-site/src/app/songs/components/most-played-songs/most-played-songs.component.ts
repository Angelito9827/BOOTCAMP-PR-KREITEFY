import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { MostPlayedSongDto } from '../../model/most-played-songs.model';
import { SongService } from '../../service/song.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-most-played-songs',
  templateUrl: './most-played-songs.component.html',
  styleUrl: './most-played-songs.component.scss',
  standalone: true,
  imports:[CommonModule]
})
export class MostPlayedSongsComponent {

  mostPlayedSongs: MostPlayedSongDto[] = [];
  @Input() styleId?: number;

  constructor(
    private songService: SongService,
    private router : Router
  ) {}

  ngOnInit(): void {
    this.getMostPlayedSongs();    
  }

  ngOnChanges(): void {
    this.getMostPlayedSongs();
  }

  private getMostPlayedSongs(): void {
    this.songService.getMostPlayedSongs(this.styleId).subscribe({
      next:(response: MostPlayedSongDto[]) => {
        this.mostPlayedSongs = response},
        error:(err) => {
          console.error("Error getting recent songs")
        }
    }) 
  }

  navigateToSongDetails(songId: number) {
    this.router.navigate(["/songs/",songId])
  }

}
