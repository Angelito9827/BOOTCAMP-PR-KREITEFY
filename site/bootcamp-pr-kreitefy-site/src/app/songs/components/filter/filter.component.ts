import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { SongService } from '../../service/song.service';
import { StyleDto } from '../../model/style.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss'],
  standalone: true,
  imports: [CommonModule]
})
export class FilterComponent implements OnInit {
  styles: StyleDto[] = [];
  @Output() styleSelected = new EventEmitter<number | null>();

  constructor(private songService: SongService) {}

  ngOnInit(): void {
    this.songService.getStyles().subscribe((styles: StyleDto[]) => {
      this.styles = styles;
    });
  }

  onStyleChange(styleId: number | null): void {
    this.styleSelected.emit(styleId);
    console.log('Selected Style ID:', styleId);
  }
}
