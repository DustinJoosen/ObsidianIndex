import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {JsonPipe, NgIf} from '@angular/common';

@Component({
  selector: 'app-selectable-media-item',
  imports: [
    JsonPipe,
    NgIf
  ],
  templateUrl: './selectable-media-item.component.html',
  styleUrl: './selectable-media-item.component.css',
  standalone: true,
})
export class SelectableMediaItemComponent implements OnInit {

  @Input({required: true})
  public media!: File;

  @Output()
  public onChangeSelection = new EventEmitter<boolean>();

  public selected = true;

  public previewUrl: string | null = null;

  ngOnInit() {
    if (this.media.type.startsWith('image/') || this.media.type.startsWith('video/')) {
      this.previewUrl = URL.createObjectURL(this.media);
    }
  }

  toggleSelected() {
    this.selected = !this.selected;
    this.onChangeSelection.emit(this.selected);
  }

  isImage(file: any): boolean { return file.type.startsWith('image/'); }
  isPdf(file: any): boolean { return file.type === 'application/pdf'; }
  isVideo(file: any): boolean { return file.type.startsWith('video/'); }
  isOther(file: any): boolean { return !this.isImage(file) && !this.isPdf(file) && !this.isVideo(file); }

}
