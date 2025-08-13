import { Component } from '@angular/core';
import { User } from '../user';
import { Header } from '../header/header';
import { LotMap } from '../lot-map/lot-map';

@Component({
  selector: 'app-user-page',
  imports: [Header, LotMap],
  templateUrl: './user-page.html',
  styleUrl: './user-page.scss'
})
export class UserPage {
  constructor(
    protected user:User
  ){}
}
