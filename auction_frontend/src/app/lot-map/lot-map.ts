import { Component } from '@angular/core';
import { NgClass, NgFor } from '@angular/common';
import { Router } from '@angular/router';

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
  constructor(
    private router: Router
  ){}

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
  goTo(path: string){
    this.router.navigate([path]);
  }
}
