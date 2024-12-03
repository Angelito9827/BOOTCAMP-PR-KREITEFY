import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RecentSongDto } from '../model/recent-song.model';

@Injectable({
  providedIn: 'root'
})
export class SongService {

  constructor(private http: HttpClient) { }

  public getRecentSongs(): Observable<RecentSongDto[]> {
    let urlEndpoint: string = "https://localhost:7026/songs/recent-songs";
    return this.http.get<RecentSongDto[]>(urlEndpoint);
  }
}
