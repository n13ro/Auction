import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Header } from '../header/header';
import { User } from '../user';

@Component({
  selector: 'app-login',
  imports: [Header],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class Login {
  constructor(
    private router: Router,
    private user: User
    ){}

  Login(name: string, img: string){
    this.user.name = name;
    this.user.picture = img;
    this.router.navigate(['/']);
  }
}
