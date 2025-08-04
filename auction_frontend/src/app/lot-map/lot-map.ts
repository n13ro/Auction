import { Component } from '@angular/core';
import { NgClass, NgFor } from '@angular/common';

@Component({
  selector: 'app-lot-map',
  imports: [NgFor, NgClass], 
  templateUrl: './lot-map.html',
  styleUrl: './lot-map.scss'
})
export class LotMap {
  public arr_id = Array.from({length: 50}, (_, i) => 0+i);
}
