import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Header } from '../header/header';

@Component({
  selector: 'app-login',
  imports: [Header],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class Login {
  constructor(private router: Router){}

  Register(){
    this.router.navigate(['/register']);
  }
}
