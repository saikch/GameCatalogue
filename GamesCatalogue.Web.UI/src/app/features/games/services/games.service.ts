import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { VideoGameDto, CreateVideoGameRequest, UpdateVideoGameRequest } from '../models/game.model';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})

export class GamesService {
    private readonly baseUrl = `${environment.apiBaseUrl}/videogames`;

    
  
    constructor(private http: HttpClient) {}
  
    getAll(): Observable<VideoGameDto[]> {
      return this.http.get<VideoGameDto[]>(this.baseUrl);
    }
  
    getById(id: number): Observable<VideoGameDto> {
      return this.http.get<VideoGameDto>(`${this.baseUrl}/${id}`);
    }
  
    create(request: CreateVideoGameRequest): Observable<VideoGameDto> {
      return this.http.post<VideoGameDto>(this.baseUrl, request);
    }
  
    update(id: number, request: UpdateVideoGameRequest): Observable<VideoGameDto> {
      return this.http.put<VideoGameDto>(`${this.baseUrl}/${id}`, request);
    }
  
    delete(id: number): Observable<void> {
      return this.http.delete<void>(`${this.baseUrl}/${id}`);
    }
  }


