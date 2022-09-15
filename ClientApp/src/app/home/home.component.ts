import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  registerMode = false;


  constructor(private http: HttpClient) { }

  ngOnInit() { }

  registerToggle() {
    this.registerMode = true;
  }

  cancelRegisterMode() {
    this.registerMode = false;
  }
}
