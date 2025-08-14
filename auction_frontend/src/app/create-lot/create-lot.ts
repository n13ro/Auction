import { NgIf } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-create-lot',
  imports: [NgIf],
  templateUrl: './create-lot.html',
  styleUrl: './create-lot.scss'
})
export class CreateLot {
  protected create:boolean =false
  
  preventDefault(e: Event) {
    e.preventDefault();
  } 

  ngOnInit(){

  }
  showCreate(){
    this.create = true;
    window.addEventListener('wheel', this.preventDefault, { passive: false });
    window.addEventListener('touchmove', this.preventDefault, { passive: false });;
  }
  closeCreate(){
    this.create = false;
    window.removeEventListener('wheel', this.preventDefault);
    window.removeEventListener('touchmove', this.preventDefault);;
  }
}
