

export interface VideoGameDto {
    id: number;
    title: string;
    platform: string;
    genre: string;
    releaseDate?: string | null;  
    price?: number | null;     
    isAvailable: boolean;
  }
  
  export interface CreateVideoGameRequest {
    title: string;
    platform: string;
    genre: string;
    releaseDate?: string | null;
    price?: number | null;
    isAvailable: boolean;
  }
  
  export type UpdateVideoGameRequest = CreateVideoGameRequest;
  