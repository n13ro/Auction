import { Component } from '@angular/core';
import { NgClass, NgFor, NgIf } from '@angular/common';
import { Router } from '@angular/router';
import { CreateLot } from '../create-lot/create-lot';

@Component({
  selector: 'app-lot-map',
  imports: [NgFor, NgClass, CreateLot, NgIf], 
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
  getUrl(){
    return this.router.url;
  }
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
