import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RecentSongDto } from '../model/recent-song.model';
import { SongDetail } from '../model/song-detail.model';
import { AuthService } from '../../auth/service/auth/auth.service';
import { ScoreDto } from '../model/score.model';
import { StyleDto } from '../model/style.model';
import { RecommendedSongDto } from '../model/recommended-song.model';
import { MostPlayedSongDto } from '../model/most-played-songs.model';

@Injectable({
  providedIn: 'root'
})
export class SongService {
  
  constructor(private http: HttpClient, private authService: AuthService  ) { }

  public getRecentSongs(styleId?: number): Observable<RecentSongDto[]> {
    let urlEndpoint: string = "http://localhost:5272/songs/recent-songs";
    if (styleId !== undefined) {
      urlEndpoint += `?styleId=${styleId}`;
    }
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

  public getStyles(): Observable<StyleDto[]> {
    let urlEndpoint:string = 'http://localhost:5272/styles';
    return this.http.get<StyleDto[]>(urlEndpoint);
  }

  public getRecommendedSongs(userId: string): Observable<RecommendedSongDto[]> {
    let urlEndpoint:string = `http://localhost:5272/history/user/${userId}/recommendedsongs`;
    return this.http.get<RecommendedSongDto[]>(urlEndpoint);
  }

  public getMostPlayedSongs(styleId?: number): Observable<MostPlayedSongDto[]> {
    let urlEndpoint: string = "http://localhost:5272/songs/most-played-songs";
    if (styleId !== undefined) {
      urlEndpoint += `?styleId=${styleId}`;
    }
    return this.http.get<MostPlayedSongDto[]>(urlEndpoint);
  }
}
