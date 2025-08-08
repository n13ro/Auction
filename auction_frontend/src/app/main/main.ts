import { Component } from '@angular/core';
import { LotMap } from '../lot-map/lot-map';
import { Header} from '../header/header';
import { TimeOutLot } from '../time-out-lot/time-out-lot';
import { PriceLot } from '../price-lot/price-lot';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-main',
  imports: [LotMap, Header, TimeOutLot, PriceLot, NgIf],
  templateUrl: './main.html',
  styleUrl: './main.scss'
})
export class Main {
  menuVisible: boolean = true;
  seeMenu(){
    this.menuVisible = !this.menuVisible
    
  }
}
