import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Header } from '../header/header';
import { User } from '../user';
import { HttpClient } from '@angular/common/http';
import {jwtDecode} from 'jwt-decode';

@Component({
  selector: 'app-login',
  imports: [Header],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})


export class Login {
  public jwtBuffer: any;
  constructor(
    private router: Router,
    private user: User,
    private http: HttpClient
    ){}

  Login(email: string, password:string){
    this.http.post('https://localhost:7243/api/Auth/Login', {
      "email": email,
      "password": password
    }).subscribe((data)=> (
      this.jwtBuffer = data,
      this.user.accessToken = this.jwtBuffer.accessToken,
      this.user.refreshToken = this.jwtBuffer.refreshToken,
      this.jwtBuffer = jwtDecode(this.jwtBuffer.accessToken),
      this.user.name = this.jwtBuffer.nickname
    )
    );
    this.router.navigate(['/']);
  }
}
