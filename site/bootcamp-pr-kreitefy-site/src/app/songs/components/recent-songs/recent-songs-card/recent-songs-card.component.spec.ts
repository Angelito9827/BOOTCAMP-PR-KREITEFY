import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecentSongsCardComponent } from './recent-songs-card.component';

describe('RecentSongsCardComponent', () => {
  let component: RecentSongsCardComponent;
  let fixture: ComponentFixture<RecentSongsCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RecentSongsCardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RecentSongsCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
