import { NgIf } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../user';

@Component({
  selector: 'app-header',
  imports: [NgIf],
  templateUrl: './header.html',
  styleUrl: './header.scss'
})
export class Header {
  protected menu:boolean = false;
  @Output() show = new EventEmitter<void>();
  constructor(
    private router: Router,
    private user: User
  ){}
  logOut(){
    this.user.picture = null;
    this.user.name = null;
    this.goTo('login');
  }
  getName(){
    return this.user.name;
  }
  toggleMenu(){
    this.menu=!this.menu;
  }
  getAvatar(){
    return this.user.picture;
  }
  getUrl(){
    return this.router.url;
  }

  goTo(path: string){
    if(path==='me'){
      if(this.user.name===null){
        this.router.navigate(['login'])
      }
      else{
        this.router.navigate([path]);
      }
    }
    else{
      this.router.navigate([path]);
    }
  }
}

