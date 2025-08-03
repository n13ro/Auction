import { Component } from '@angular/core';
import { LotCard } from '../lot-card/lot-card'
import { LotCardAlt } from '../lot-card-alt/lot-card-alt'

@Component({
  selector: 'app-lot-map',
  imports: [LotCard, LotCardAlt], 
  templateUrl: './lot-map.html',
  styleUrl: './lot-map.scss'
})
export class LotMap {

}
