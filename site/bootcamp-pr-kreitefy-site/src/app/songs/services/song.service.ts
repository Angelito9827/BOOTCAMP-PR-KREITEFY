import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RecentSongDto } from '../model/recent-song.model';
import { SongDetail } from '../model/song-detail.model';
import { AuthService } from '../../auth/service/auth/auth.service';
import { ScoreDto } from '../model/score.model';

@Injectable({
  providedIn: 'root'
})
export class SongService {
  
  constructor(private http: HttpClient, private authService: AuthService  ) { }

  public getRecentSongs(): Observable<RecentSongDto[]> {
    let urlEndpoint: string = "http://localhost:5272/songs/recent-songs";
    return this.http.get<RecentSongDto[]>(urlEndpoint);
  }

  public getSongsDetail(songId: number): Observable<SongDetail> {
    let urlEndpoint:string = `http://localhost:5272/songs/${songId}`;
    return this.http.get<SongDetail>(urlEndpoint);
  }

  public incrementTotalPlayCount(songId: number): Observable<any> {
    const userId = this.authService.getStoredUserId();
    const urlEndpoint = `http://localhost:5272/history/increment-playcount?userId=${userId}&songId=${songId}`;
    return this.http.post(urlEndpoint, songId);
  }

  public averageScore(scoreDto: ScoreDto): Observable<any> {
    const userId = this.authService.getStoredUserId();
    const urlEndpoint = `http://localhost:5272/score`;
    return this.http.post<SongDetail>(urlEndpoint, scoreDto);
  }  
}
