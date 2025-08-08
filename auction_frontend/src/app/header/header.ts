import { Component, EventEmitter, Output } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  imports: [],
  templateUrl: './header.html',
  styleUrl: './header.scss'
})
export class Header {
  image_up = "images/arrow_up.svg";
  image_down = "images/arrow_down.svg"
  currentImage = this.image_up;
  @Output() show = new EventEmitter<void>();
  constructor(
    private router: Router
  ){}

  toggleImage() {
    this.currentImage = this.currentImage === this.image_up ? this.image_down : this.image_up;
  }

  Login(){
    this.router.navigate(['/login']);
  }
}

