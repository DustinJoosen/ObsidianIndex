import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {lastValueFrom} from 'rxjs';
import {GetMediaResponseModel, MediaModel} from '../models/media.models';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private apiUri = "https://localhost:7420";

  constructor(private http: HttpClient) {}

  async getMediaById(id: string) {
    return await lastValueFrom(this.http.get<MediaModel>(this.apiUri + "/media/" + id));
  }

  async getAllMedia() {
    return await lastValueFrom(this.http.get<GetMediaResponseModel>(this.apiUri + "/media/"));
  }
}
