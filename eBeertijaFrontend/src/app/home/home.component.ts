import { Component, OnInit } from '@angular/core';
import { GrafickiPrikazService } from '../core/Services/GrafickiPrikaz.service';
import { GrafickiPrikazDto } from '../Models/GrafickiPrikazDto';
import { StolDto } from '../Models/StolDto';
import { MatDialog } from '@angular/material';
import { AddStolComponent } from '../add-stol/add-stol.component';
import { orientationsList } from './StolPopUpModel';
import { CdkDragEnd } from '@angular/cdk/drag-drop';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  title = 'E - Beertija';
  isLoaded: boolean;
  grafickiPrikaz: GrafickiPrikazDto;
  editMode: boolean;
  opened = true;

  constructor(private grafickiPrikazService: GrafickiPrikazService, public dialog: MatDialog, private router: Router) { }

  ngOnInit() {
    this.isLoaded = false;
    this.grafickiPrikazService.getGrafickiPrikaz().subscribe(data => {
      if (data) {
        this.grafickiPrikaz = data;
        this.isLoaded = true;
      }
    });
  }

  onCreate() {
    const dialogRef = this.dialog.open(AddStolComponent, {
      width: '470px',
      height: '380px',
      data: {
      isEdit: false,
      oznakaStola: '',
      serijskiBrojUredaja: '',
      orientation: orientationsList
    }
  });
    dialogRef.afterClosed().subscribe(result => {
    console.log('The dialog was closed' + result);
    if (result) {
    const currentStol: StolDto = {
      oznakaStola: result.oznakaStola,
      x: 0,
      y: 0,
      orientation: result.odabranaOrientationStupanj,
      xOffset: 0,
      yOffset: 0,
      serijskiBrojUredaja: result.serijskiBrojUredaja
    };

    this.grafickiPrikazService.dodajStol(currentStol).subscribe((data: any) => {
      this.grafickiPrikaz.aktivniStolovi.push(data);
    });
  }
  });

  }

  SaveAll() {
    this.grafickiPrikaz.aktivniStolovi.forEach(stol => {
      if ((stol.xOffset !== null && stol.xOffset !== undefined) && ((stol.xOffset !== 0) && (stol.yOffset !== 0 ))) {
        stol.x = stol.x + stol.xOffset;
        stol.xOffset = 0;
        stol.y = stol.y + stol.yOffset;
        stol.yOffset = 0;
    }
    });
    this.grafickiPrikazService.saveGrafickiPrikaz(this.grafickiPrikaz).subscribe(
      data => {
        console.log('PUT Request is successful ', data);
        this.grafickiPrikaz = data;
      },
      error => {
        console.log('Rrror', error);
      }
    );
    this.editMode = false;
  }

  goRacun(stol: StolDto) {
    this.router.navigate(['/racun/', stol.id]);
  }

  goNarudzba(narudzbaId: number) {
    this.router.navigate(['/narudzba/', narudzbaId]);
  }

  onEdit(stol: StolDto) {
    const dialogRef = this.dialog.open(AddStolComponent, {
      width: '470px',
      height: '380px',
      data: {
      isEdit: true,
      oznakaStola: stol.oznakaStola,
      stariStol: stol,
      odabranaOrientationStupanj: stol.orientation,
      orientation: orientationsList,
      serijskiBrojUredaja: stol.serijskiBrojUredaja
    }
  });
    dialogRef.afterClosed().subscribe(result => {
    console.log('The dialog was closed' + result);
    if (result) {
      if (result.isDeleted) {
        this.grafickiPrikaz.aktivniStolovi = [...this.grafickiPrikaz.aktivniStolovi.filter(item => item !== result.stariStol)];
        this.grafickiPrikaz.obrisaniStolovi = [...this.grafickiPrikaz.obrisaniStolovi, result.stariStol];
      } else {
        const currentStol: StolDto = {
          id: result.stariStol.id,
          oznakaStola : result.oznakaStola,
          serijskiBrojUredaja: result.serijskiBrojUredaja,
          x: result.stariStol.x + (result.stariStol.xOffset ? result.stariStol.xOffset : 0),
          y: result.stariStol.y + (result.stariStol.yOffset ? result.stariStol.yOffset : 0),
          orientation: result.odabranaOrientationStupanj,
          xOffset: 0,
          yOffset: 0,
          stolClass: result.stariStol.stolClass
        };
          // tslint:disable-next-line: max-line-length
          this.grafickiPrikaz.aktivniStolovi = [...this.grafickiPrikaz.aktivniStolovi.filter(item => item !== result.stariStol), currentStol];
    }
  }
  });
  }

  dragEnd(event: CdkDragEnd) {
    const offset = { ...(<any>event.source._dragRef)._passiveTransform };
    console.log(offset);
    const stolId = parseInt(event.source.element.nativeElement.id, 10);
    this.grafickiPrikaz.aktivniStolovi.forEach(stol => {
      if (stol.id === stolId) {
        stol.xOffset = offset.x;
        stol.yOffset = offset.y;
      }
    });
  }

}

