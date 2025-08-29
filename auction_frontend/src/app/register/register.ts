import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Header } from '../header/header';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-register',
  imports: [Header],
  templateUrl: './register.html',
  styleUrl: './register.scss'
})
export class Register {  
  public postRegister: any;
  constructor(
    private router: Router,
    private http: HttpClient
  ){}
  
  Register(name: string, email: string, password: string){
    this.http.post('https://localhost:7243/api/Auth/SignIn', {
      "id": 0,
      "nickName": name,
      "email": email,
      "password": password
    }).subscribe();
    this.router.navigate(['/login']);
  }
}
