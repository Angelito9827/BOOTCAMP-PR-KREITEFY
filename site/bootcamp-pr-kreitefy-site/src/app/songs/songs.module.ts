import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SongsRoutingModule } from './songs-routing.module';
import { provideHttpClient } from '@angular/common/http';



@NgModule({
  declarations: [

  ],
  imports: [
    CommonModule,
    SongsRoutingModule,
  ],
  providers: [
    provideHttpClient()
  ]
})
export class SongsModule { }
