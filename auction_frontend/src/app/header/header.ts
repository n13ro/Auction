import { Component, EventEmitter, Output } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  imports: [],
  templateUrl: './header.html',
  styleUrl: './header.scss'
})
export class Header {
  @Output() show = new EventEmitter<void>();
  constructor(
    private router: Router
  ){}

  Login(){
    this.router.navigate(['/login']);
  }
}
