import {TagModels} from './tag.models';

export interface MediaModel {
  mediaId: string;
  fileTypeExtension: string;
  description?: string;
  fileSizeInKb?: number;
  dimensions?: string;
  dateAdded: string;
  tags: TagModels[];
}

export interface GetMediaResponseModel {
  media: MediaModel[]
}
