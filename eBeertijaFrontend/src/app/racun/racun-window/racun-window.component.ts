import { Component, OnInit } from '@angular/core';
import { RacunService } from '@src/app/core/Services/Racun.service';
import { NewRacunDto } from '@src/app/Models/NewRacunDto';
import { ActivatedRoute, Router } from '@angular/router';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { StavkaCjenikaDto } from '@src/app/Models/StavkaCjenikaDto';
import { StavkaRacunaDto } from '@src/app/Models/StavkaRacunaDto';
import { RacunDto } from '@src/app/Models/RacunDto';

@Component({
  selector: 'app-racun-window',
  templateUrl: './racun-window.component.html',
  styleUrls: ['./racun-window.component.css']
})
export class RacunWindowComponent implements OnInit {
  isLoaded: boolean;
  newRacunWindow: NewRacunDto;
  currentStolId: number;
  odabraneStavke: StavkaCjenikaDto[] = [];
  insertRacun: RacunDto;

  constructor(private racunService: RacunService, private _Activatedroute: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this._Activatedroute.params.subscribe(params => {
        this.currentStolId = params['id'];
        this.getRacunWindow();
    });
  }

  getRacunWindow() {
    this.isLoaded = false;
    this.racunService.getNewRacunWindow(this.currentStolId).subscribe(data => {
      if (data) {
        this.newRacunWindow = data;
        this.newRacunWindow.stavkeCjenikaNaSnazi.forEach(element => {
          element.ukupnoNewRacun = element.cijenaSaPdvom;
        });
        this.isLoaded = true;
      }
    });
  }

  drop(event: CdkDragDrop<StavkaCjenikaDto[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      this.newRacunWindow.stavkeCjenikaNaSnazi.forEach(element => {
        element.ukupnoNewRacun = element.cijenaSaPdvom;
      });
      transferArrayItem(event.previousContainer.data,
                        event.container.data,
                        event.previousIndex,
                        event.currentIndex);
    
    this.newRacunWindow.ukupnoSaPdvom = 0;
    this.odabraneStavke.forEach(element => {
      this.newRacunWindow.ukupnoSaPdvom += element.ukupnoNewRacun;
    });
    }
}

  updateKolicina(kolicina: number, stavka: StavkaCjenikaDto) {
    stavka.ukupnoNewRacun = stavka.cijenaSaPdvom * kolicina;
    stavka.kolicina = kolicina;
    this.newRacunWindow.ukupnoSaPdvom = 0;
    this.odabraneStavke.forEach(element => {
      this.newRacunWindow.ukupnoSaPdvom += element.ukupnoNewRacun;
    });
  }

  createRacun() {
    const newRacun: RacunDto = {
      broj: this.newRacunWindow.broj,
      brojOpis: this.newRacunWindow.brojOpis,
      isStorniran: false,
      oznakaStola: this.newRacunWindow.oznakaStola,
      stolId: this.currentStolId
    };
    newRacun.stavkeRacuna = [];
    this.odabraneStavke.forEach(stavka => {
      const stavkaRacuna: StavkaRacunaDto = {
        kolicina: (stavka.ukupnoNewRacun / stavka.cijenaSaPdvom),
        stavkaCjenikaId: stavka.id
      };
      newRacun.stavkeRacuna.push(stavkaRacuna);
    });

    this.racunService.createRacun(newRacun).subscribe(data => {
      if (data) {
        console.log('racun poslan');
        
      }

      this.router.navigate(['/home']);
    });
  }


}
