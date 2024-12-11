import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SongsForMeComponent } from './songs-for-me.component';

describe('SongsForMeComponent', () => {
  let component: SongsForMeComponent;
  let fixture: ComponentFixture<SongsForMeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SongsForMeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SongsForMeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
