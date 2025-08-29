import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class User {
  public name: string | null = null;
  public picture: string | null = "./images/default-user.jpg";
  public accessToken: string | null = null;
  public refreshToken: string | null = null;
  

  setUser(name: string, picture:string) {
    this.name = name;
    this.picture = picture;
  }

  getUserName() {
    return this.name;
  }

  getUserPicture(){
    return this.picture;
  }
}
