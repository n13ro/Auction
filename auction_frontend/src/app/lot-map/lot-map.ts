import { Component } from '@angular/core';
import { NgClass, NgFor } from '@angular/common';

@Component({
  selector: 'app-lot-map',
  imports: [NgFor, NgClass], 
  templateUrl: './lot-map.html',
  styleUrl: './lot-map.scss'
})
export class LotMap {
  name: string = "Название предмета";
  date: string = "11:11:11";
  price: string = "30.000p.";
  image: string = "./images/image.png";

  public arr = Array.from({length: 50}, () => ({
    name: this.name,
    date: this.date,
    price: this.price,
    image: this.image
  }));

  getName(index: number): string {
    return this.arr[index].name;
  }
  getDate(index: number): string {
    return this.arr[index].date;
  }
  getPrice(index: number): string {
    return this.arr[index].price;
  }
  getImage(index: number): string {
    return this.arr[index].image;
  }
}
