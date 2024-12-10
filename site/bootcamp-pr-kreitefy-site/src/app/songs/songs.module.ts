import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SongsRoutingModule } from './songs-routing.module';
import { provideHttpClient } from '@angular/common/http';
import { SongDetailComponent } from './components/song-detail/song-detail.component';




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
