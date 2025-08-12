import { NgIf } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  imports: [NgIf],
  templateUrl: './header.html',
  styleUrl: './header.scss'
})
export class Header {
  @Output() show = new EventEmitter<void>();
  constructor(
    private router: Router
  ){}

  getUrl(){
    return this.router.url;
  }

  goTo(path: string){
    this.router.navigate([path]);
  }
}

