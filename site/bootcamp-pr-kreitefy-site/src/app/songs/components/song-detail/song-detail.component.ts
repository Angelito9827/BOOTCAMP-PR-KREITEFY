import { Component } from '@angular/core';
import { SongDetail } from '../../model/song-detail.model';
import { ActivatedRoute } from '@angular/router';
import { SongService } from '../../services/song.service';
import { ScoreDto } from '../../model/score.model';
import { AuthService } from '../../../auth/service/auth/auth.service';

@Component({
  selector: 'app-song-detail',
  templateUrl: './song-detail.component.html',
  styleUrl: './song-detail.component.scss'
})
export class SongDetailComponent {
  song!: SongDetail;
  songId?: number;

  constructor(
    private route: ActivatedRoute, 
    private service: SongService,
    private authService: AuthService 
  ) {}

  ngOnInit(): void {
    const idSong = Number(this.route.snapshot.paramMap.get('id'));
    this.getSongDetail(idSong);
  }

  getSongDetail(songId: number) {
    this.service
    .getSongsDetail(songId)
    .subscribe((data: SongDetail) => {
      this.song = data;
    });
  }

  incrementTotalPlayCount(): void {
    this.service.incrementTotalPlayCount(this.song.id)
    .subscribe({
      next: () => {
        this.song.totalPlayCount++;
      },
      error: (err) => {
        console.error('Error al incrementar el contador de reproducciones:', err);
      }
    });

  }
  averageScore(stars: number): void {
    const userId = Number(this.authService.getStoredUserId());
    const scoreDto: ScoreDto = {
      userId: userId,
      songId: this.song.id,
      stars: stars,
      id:0
    };

    this.service.averageScore(scoreDto).subscribe({
      next: (updatedSong) => {
        this.song.averageScore = updatedSong.averageScore;
        this.getSongDetail(this.song.id);
      },
      error: (err) => {
        console.error('Error al puntuar la canción:', err);
      }
    });
  }
}

