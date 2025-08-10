import { Component, Output, EventEmitter} from '@angular/core';

@Component({
  selector: 'app-bet',
  imports: [],
  templateUrl: './bet.html',
  styleUrl: './bet.scss'
})
export class Bet {
  @Output() toggle = new EventEmitter<void>();
}
