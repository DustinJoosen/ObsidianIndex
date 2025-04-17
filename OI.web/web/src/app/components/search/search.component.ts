import {Component, OnInit} from '@angular/core';
import {ApiService} from '../../services/api.service';
import {MediaModel} from '../../models/media.models';
import {JsonPipe, NgFor, NgIf} from '@angular/common';

@Component({
  selector: 'app-search',
  imports: [NgIf, NgFor, JsonPipe],
  templateUrl: './search.component.html',
  styleUrl: './search.component.css',
  standalone: true,
})
export class SearchComponent implements OnInit {

  public mediaList: MediaModel[] = [];
  constructor(private apiService: ApiService) { }

  async ngOnInit() {
    this.mediaList = (await this.apiService.getAllMedia()).media;
    console.log(this.mediaList);
  }

}
