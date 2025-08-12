import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Header } from '../header/header';
import { User } from '../user';

@Component({
  selector: 'app-register',
  imports: [Header],
  templateUrl: './register.html',
  styleUrl: './register.scss'
})
export class Register {
  constructor(
    private router: Router,
    private user: User
  ){}

  Register(name: string, img: string){
    this.user.name = name;
    this.user.picture = img;
    this.router.navigate(['/']);
  }
}
