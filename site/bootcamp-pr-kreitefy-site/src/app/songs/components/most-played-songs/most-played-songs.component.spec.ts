import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MostPlayedSongsComponent } from './most-played-songs.component';

describe('MostPlayedSongsComponent', () => {
  let component: MostPlayedSongsComponent;
  let fixture: ComponentFixture<MostPlayedSongsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MostPlayedSongsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MostPlayedSongsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
