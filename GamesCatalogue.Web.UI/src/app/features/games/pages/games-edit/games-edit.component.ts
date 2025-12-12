import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { GamesService } from '../../services/games.service';
import { VideoGameDto } from '../../models/game.model';

@Component({
  selector: 'app-games-edit',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './games-edit.component.html'
})
export class GamesEditComponent implements OnInit {
  form!: FormGroup;
  isEditMode = false;
  gameId?: number;
  isSaving = false;
  error?: string;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private service: GamesService
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(100)]],
      platform: ['', [Validators.required, Validators.maxLength(50)]],
      genre: ['', [Validators.required, Validators.maxLength(50)]],
      releaseDate: [''],
      price: [null],
      isAvailable: [true]
    });

    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.isEditMode = true;
      this.gameId = +idParam;
      this.loadGame(this.gameId);
    }
  }

  loadGame(id: number): void {
    this.service.getById(id).subscribe({
      next: (game: VideoGameDto) => {
        this.form.patchValue({
          title: game.title,
          platform: game.platform,
          genre: game.genre,
          releaseDate: game.releaseDate?.substring(0, 10) ?? '',
          price: game.price ?? null,
          isAvailable: game.isAvailable
        });
      },
      error: () => this.error = 'Could not load game.'
    });
  }

  onSubmit(): void {
    if (this.form.invalid || this.isSaving) return;
  
    this.isSaving = true;
    const value = this.form.value;
  
    if (this.isEditMode && this.gameId) {
      
      this.service.update(this.gameId, value).subscribe({
        next: () => this.router.navigate(['/games']),
        error: () => {
          this.error = 'Save failed.';
          this.isSaving = false;
        }
      }); 
    } else {
      
      this.service.create(value).subscribe({
        next: () => this.router.navigate(['/games']),
        error: () => {
          this.error = 'Save failed.';
          this.isSaving = false;
        }
      });
    }
  }
  

  onCancel(): void {
    this.router.navigate(['/games']);
  }
}
