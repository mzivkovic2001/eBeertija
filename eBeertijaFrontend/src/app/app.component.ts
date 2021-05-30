import { Component } from '@angular/core';
import { HttpLoaderService } from './core/Services/http-loader.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(public loaderService: HttpLoaderService) {
    // Use the component constructor to inject services.
}
}
