import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class User {
  private user: { 
    name: string,
    picture: string
  } | null = null;

  setUser(name: string, picture:string) {
    this.user = { name, picture };
  }

  getUserName() {
    return this.user?.name;
  }

  getUserPicture(){
    return this.user?.picture;
  }
}
