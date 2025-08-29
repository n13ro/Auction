import { NgIf } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../user';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-header',
  imports: [NgIf],
  templateUrl: './header.html',
  styleUrl: './header.scss'
})
export class Header {
  private jwtBuffer: any;
  protected menu:boolean = false;
  @Output() show = new EventEmitter<void>();
  constructor(
    private router: Router,
    private user: User,
    private http: HttpClient
  ){}
  logOut(){
    this.http.post('https://localhost:7243/api/Auth/Logout', {
      "refreshToken": this.user.refreshToken
    }).subscribe()
    this.user.picture = null;
    this.user.name = null;
    this.user.accessToken = null;
    this.user.refreshToken = null;
    this.goTo('/');
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
    
    this.http.post('https://localhost:7243/api/Auth/Refresh', {
      "refreshToken": this.user.refreshToken
    }).subscribe((data) =>(
      this.jwtBuffer = data,
      this.user.accessToken = this.jwtBuffer.accessToken,
      this.user.refreshToken = this.jwtBuffer.refreshToken
    ))
  }
}

