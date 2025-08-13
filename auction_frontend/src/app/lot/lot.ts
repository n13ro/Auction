import { Component } from '@angular/core';
import { Header } from '../header/header';
import { Bet } from '../bet/bet';
import { NgIf } from '@angular/common';
import { Carousel } from '../carousel/carousel';
interface bet{
    name:string;
    date:string;
    amount:string;
  }
@Component({
  selector: 'app-lot',
  imports: [Header,Carousel, Bet, NgIf],
  templateUrl: './lot.html',
  styleUrl: './lot.scss'
})

export class Lot {
  bets:Array<bet> = [
    {name: "Ivan", date: "11:11:12", amount: "5.000.000p." },
    {name: "Olga", date: "11:11:15", amount: "6.000.000p." },
    {name: "Max Maxbetov", date: "11:11:25", amount: "7.500.000p." }
  ];
  betting:boolean = false;
  showBet(){
    this.betting = true;
  }
  closeBet(){
    this.betting = false;
  }
}
