import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Main } from './main/main';
@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Main],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('auction_frontend');
}
