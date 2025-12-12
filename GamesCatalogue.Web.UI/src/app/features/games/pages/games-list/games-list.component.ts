import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { GamesService } from '../../services/games.service';
import { VideoGameDto } from '../../models/game.model';

@Component({
  selector: 'app-games-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './games-list.component.html'
})
export class GamesListComponent implements OnInit {
  games: VideoGameDto[] = [];
  isLoading = false;
  error?: string;

  constructor(
    private service: GamesService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    console.log('GamesList ngOnInit');
    this.loadGames();
  }


  //get
  loadGames(): void {
    this.isLoading = true;
    this.error = undefined;
    console.log('loading games....');

    this.service.getAll().subscribe({
      next: (games) => {
        console.log('games:', games);
        this.games = games ?? [];
        this.isLoading = false;
        this.cdr.detectChanges();
        console.log('gameslength:', this.games.length);
      },
      error: (err) => {
        console.error('Error:', err);
        this.error = 'Failed to load games.';
        this.isLoading = false;
        this.cdr.detectChanges();
      }
    });
  }


  //create
  onCreate(): void {
    this.router.navigate(['/games/create']);
  }


  //edit/update
  onEdit(game: VideoGameDto): void {
    this.router.navigate(['/games/edit', game.id]);
  }
 

  //delete
  onDelete(game: VideoGameDto): void {
    if (!confirm(`Delete "${game.title}"?`)) return;
  
    this.service.delete(game.id).subscribe({
      next: () => {
        alert('Game deleted successfully.');
        this.loadGames(); 
      },
      error: (err) => {
        console.error('Error deleting game:', err);
        alert('Failed to delete game.');
      },
      complete: () => {
        this.loadGames(); 
      }
    });
  }
}
