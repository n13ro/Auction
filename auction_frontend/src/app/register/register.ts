import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Header } from '../header/header';

@Component({
  selector: 'app-register',
  imports: [Header],
  templateUrl: './register.html',
  styleUrl: './register.scss'
})
export class Register {
  constructor(private router: Router){}

  Login(){
    this.router.navigate(['/login']);
  }
}
