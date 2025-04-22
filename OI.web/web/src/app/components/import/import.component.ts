import { Component } from '@angular/core';
import {NgFor, NgIf} from '@angular/common';
import {SelectableMediaItemComponent} from '../../shared/selectable-media-item/selectable-media-item.component';
import {ApiService} from '../../services/api.service';
import {lastValueFrom} from 'rxjs';

interface SelectableFile {
  file: File,
  selected: boolean;
}

@Component({
  selector: 'app-import',
  imports: [NgFor, NgIf, SelectableMediaItemComponent],
  templateUrl: './import.component.html',
  styleUrl: './import.component.css'
})
export class ImportComponent {
  files: SelectableFile[] = [];

  public progress: number = 0;

  public importingRightNow = false;

  constructor(private apiService: ApiService) {
  }

  onFilesSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (!input.files)
      return;

    this.files = Array.from(input.files)
      .map(file => ({
        file: file,
        selected: true
      } as SelectableFile));
  }

  onChangeSelection(selected: boolean, f: SelectableFile) {
    let foundIndex = this.files.findIndex(
        sf => sf.file.name == f.file.name &&
        sf.file.size == f.file.size &&
        sf.file.type == f.file.type);

    if (foundIndex == -1) {
      alert("Godverdomme");
    }

    this.files[foundIndex].selected = selected;
  }

  getSelectedFiles() {
    return this.files
      .filter(file => file.selected);
  }

  async import() {
    if (this.importingRightNow) {
      return;
    }

    this.importingRightNow = true;
    const batchSize = 10;

    for (let i = 0; i < this.getSelectedFiles().length; i += batchSize) {
      let batch = this.getSelectedFiles().slice(i, i + batchSize);
      await this.apiService.uploadBatch(batch.map(file => file.file));
      this.progress += batchSize;
    }

    console.log("Import complete");
    this.files = [];
    this.progress = 0;

    this.importingRightNow = false;
  }
}
