import { Component, OnInit, AfterViewInit, OnDestroy, ViewChildren, QueryList } from '@angular/core';
import { IzvjestajService } from '@src/app/core/Services/Izvjestaj.service';
import { RacunIzvjestajDto } from '@src/app/Models/RacunIzvjestajDto';
import { Subject } from 'rxjs';
import { DataTableDirective } from 'angular-datatables';
import { NarudzbaDto } from '@src/app/Models/NarudzbaDto';
import { StavkaRacunaDto } from '@src/app/Models/StavkaRacunaDto';
import { RacunService } from '@src/app/core/Services/Racun.service';

@Component({
  selector: 'app-racun-izvjestaj',
  templateUrl: './racun-izvjestaj.component.html',
  styleUrls: ['./racun-izvjestaj.component.css']
})
export class RacunIzvjestajComponent implements OnInit, AfterViewInit, OnDestroy {

  @ViewChildren(DataTableDirective)
  dtElements: QueryList<DataTableDirective>;

  constructor(private izvjestajService: IzvjestajService, private racunService: RacunService) { }

  odabraniDatum: Date;
  racunIzvjestaj: RacunIzvjestajDto[];
  isLoaded: boolean;
  opcije: any = {};
  opcijeNarudzba: any = {};
  dtTrigger: Subject<any> = new Subject();
  dtTriggerNarudzba: Subject<any> = new Subject();
  dtTriggerStavka: Subject<any> = new Subject();
  isViewNarudzbe: boolean;
  viewNarudzba: NarudzbaDto;
  stavkeOdabranogRacuna: StavkaRacunaDto[];
  isViewStavki: boolean;
  displayDate: Date = new Date();

  ngOnInit() {
    this.opcije = {
      // Declare the use of the extension in the dom parameter
      dom: 'Bfrtip',
      // Configure the buttons
      buttons: [
        'copy',
        'print',
        'excel'
      ]
    };
    this.getIzvjestajZaDanasnjiDatum();
  }

  ngAfterViewInit(): void {
    if (this.dtTrigger) {
      this.dtTrigger.next();
    }
    if (this.dtTriggerNarudzba) {
      this.dtTriggerNarudzba.next();
    }
    if (this.dtTriggerStavka) {
      this.dtTriggerStavka.next();
    }
  }

  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    if (this.dtTrigger) {
      this.dtTrigger.unsubscribe();
    }
    if (this.dtTriggerNarudzba) {
      this.dtTriggerNarudzba.unsubscribe();
    }
    if (this.dtTriggerStavka) {
      this.dtTriggerStavka.unsubscribe();
    }
  }

  rerender(): void {
    this.dtElements.forEach((dtElement: DataTableDirective) => {
      if (dtElement.dtInstance) {
      dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
        dtInstance.destroy();
        console.log('destroying');
        console.log(dtElement);
      });
      }
    });
    this.dtTrigger.next();
    if (this.dtTriggerNarudzba) {
      this.dtTriggerNarudzba.next();
    }
    if (this.dtTriggerStavka) {
      this.dtTriggerStavka.next();
    }
  }

  getIzvjestajZaDanasnjiDatum() {
    this.isLoaded = false;
    this.izvjestajService.getIzvjestajNaDanasnjiDatum().subscribe(data => {
      if (data) {
        this.racunIzvjestaj = data;
        
        this.isLoaded = true;
      }
    });
    this.dtTrigger.next();
        this.rerender();
  }

  pogledajNarudzbu(racun: RacunIzvjestajDto) {
    if (this.isViewNarudzbe === true && racun.narudzbaId === this.viewNarudzba.id) {
      this.isViewNarudzbe = false;
      if (this.dtTriggerNarudzba) {
        this.dtTriggerNarudzba.unsubscribe();
      }
      if (this.dtTriggerStavka) {
        this.dtTriggerStavka.unsubscribe();
      }
      this.rerender();
    } else {
      this.isViewStavki = false;
      this.viewNarudzba = racun.narudzba;
      this.isViewNarudzbe = true;
      this.dtTriggerNarudzba.next();
      this.rerender();
    }
  }

  pogledajStavke(racun: RacunIzvjestajDto) {
    if (this.isViewStavki === true && racun.stavkeRacuna === this.stavkeOdabranogRacuna) {
      this.isViewStavki = false;
      if (this.dtTriggerNarudzba) {
        this.dtTriggerNarudzba.unsubscribe();
      }
      if (this.dtTriggerStavka) {
        this.dtTriggerStavka.unsubscribe();
      }
      this.rerender();
    } else {
      this.isViewNarudzbe = false;
      this.stavkeOdabranogRacuna = racun.stavkeRacuna;
      this.isViewStavki = true;
      this.dtTriggerStavka.next();
      this.rerender();
    }
  }

  pretragaPoDatumu() {
    console.log(this.displayDate);
    const formatedDate: string = this.displayDate.toDateString();
    this.isLoaded = false;
    this.izvjestajService.getIzvjestaj(formatedDate).subscribe(data => {
      this.racunIzvjestaj = data;
      this.isViewNarudzbe = false;
      this.isViewStavki = false;
      if (this.dtTriggerNarudzba) {
        this.dtTriggerNarudzba.unsubscribe();
      }
      if (this.dtTriggerStavka) {
        this.dtTriggerStavka.unsubscribe();
      }
      if (this.dtTrigger) {
        this.dtTriggerStavka.unsubscribe();
      }
      this.isLoaded = true;
      this.dtTrigger.next();
    });
    this.rerender();
  }

  stornoRacuna(racun: RacunIzvjestajDto) {
    this.racunService.stornoRacuna(racun.id).subscribe(data => {
      this.ngOnInit();
    });
  }

}
