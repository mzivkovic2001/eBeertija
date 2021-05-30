import { Component, OnInit } from '@angular/core';
import { Router} from '@angular/router';
import { StavkaCjenikaService } from '../core/Services/StavkaCjenika.service';
import { StavkaCjenikaDto } from '../Models/StavkaCjenikaDto';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  title = 'E - Beertija';
  public isLoaded: boolean;
  stavkeCjenika: StavkaCjenikaDto[];

  constructor(private router: Router, private stavkaService: StavkaCjenikaService) { }

  ngOnInit() {
    this.getStavkeCjenika();
  }

  getStavkeCjenika() {
      this.isLoaded = false;
      this.stavkaService.getStavkeCjenikaNaSnazi().subscribe(data => {
          this.stavkeCjenika = data;
          this.isLoaded = true;
      });
  }

}
