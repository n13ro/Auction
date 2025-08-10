import { Component } from '@angular/core';
import { Header } from '../header/header';
import { Bet } from '../bet/bet';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-lot',
  imports: [Header, Bet, NgIf],
  templateUrl: './lot.html',
  styleUrl: './lot.scss'
})
export class Lot {
  betting:boolean = false;
  showBet(){
    this.betting = true;
  }
  closeBet(){
    this.betting = false;
  }
}
