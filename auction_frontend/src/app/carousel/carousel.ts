import { NgFor } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-carousel',
  imports: [NgFor],
  templateUrl: './carousel.html',
  styleUrl: './carousel.scss'
})
export class Carousel {
  images: string[] = [
        './images/big_car.png',
        './images/image copy.png',
        './images/image copy 2.png'
    ];
    currentIndex: number = 0;

    moveSlide(direction: number): void {
        const newIndex = this.currentIndex + direction;

        if (this.currentIndex==0 && direction == -1) {
            this.currentIndex = this.images.length-1;
        }
        else if (this.currentIndex === this.images.length - 1 && direction === 1) {
            this.currentIndex = 0;
        }
        else {
            this.currentIndex = newIndex;
        }
    }
}
