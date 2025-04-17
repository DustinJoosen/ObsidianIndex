import {Component, OnInit} from '@angular/core';
import {ApiService} from '../../services/api.service';
import {MediaModel} from '../../models/media.models';
import {JsonPipe, NgIf} from '@angular/common';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-detail',
  imports: [JsonPipe, NgIf],
  templateUrl: './detail.component.html',
  styleUrl: './detail.component.css'
})
export class DetailComponent implements OnInit {

  public mediaData?: MediaModel;

  constructor(private apiService: ApiService, private route: ActivatedRoute) {
  }

  async ngOnInit() {
    let id = this.route.snapshot.paramMap.get('id');
    if (id == null) {
      console.error("Hey fuckhead, you need to pass an ID along...");
      return;
    }
    this.mediaData = await this.apiService.getMediaById(id);
  }
}
