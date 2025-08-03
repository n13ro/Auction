import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { LotMap } from './lot-map/lot-map'
import { Header } from './header/header';
@Component({
  selector: 'app-root',
  imports: [RouterOutlet, LotMap, Header],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('auction_frontend');
}
