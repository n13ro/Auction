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
  image_up = "images/arrow_up.svg";
  image_down = "images/arrow_down.svg"
  currentImage = this.image_up;
  menuVisible: boolean = true;
  seeMenu(){
    this.menuVisible = !this.menuVisible
  }
  toggleImage() {
    this.currentImage = this.currentImage === this.image_up ? this.image_down : this.image_up;
  }
}
