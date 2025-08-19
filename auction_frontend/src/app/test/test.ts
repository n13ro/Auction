import { HttpClient } from '@angular/common/http';
import { Component, Injectable } from '@angular/core';

@Component({
  selector: 'app-test',
  imports: [],
  templateUrl: './test.html',
  styleUrl: './test.scss'
})

@Injectable({
  providedIn: 'root',
})

export class Test {

  public getJSONValue:any;
  public PostJSONValue: any;

  constructor(private http: HttpClient){

  }
  ngOnInit(){
    this.get();
  }
  public get(){
    return this.http.get('https://jsonplaceholder.typicode.com/posts/1').subscribe((data) =>{
      this.getJSONValue = data;
    })
  }
}
