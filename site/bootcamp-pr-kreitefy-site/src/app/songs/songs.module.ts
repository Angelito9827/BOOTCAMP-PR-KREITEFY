import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SongsRoutingModule } from './songs-routing.module';
import { provideHttpClient } from '@angular/common/http';
import { SongDetailComponent } from './components/song-detail/song-detail.component';
import { SongsForMeComponent } from './components/songs-for-me/songs-for-me.component';
import { MostPlayedSongsComponent } from './components/most-played-songs/most-played-songs.component';




@NgModule({
  declarations: [

  
    SongDetailComponent
  ],
  imports: [
    CommonModule,
    SongsRoutingModule
  ],
  providers: [
    provideHttpClient()
  ]
})
export class SongsModule { }
