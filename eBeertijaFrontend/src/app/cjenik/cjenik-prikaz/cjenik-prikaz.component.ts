import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatTableDataSource } from '@angular/material';
import { AddEditCjenikComponent } from '../add-edit-cjenik/add-edit-cjenik.component';
import { MatDialog } from '@angular/material';
import { AddEditStavkaComponentComponent } from '../add-edit-stavka-component/add-edit-stavka-component.component';
import { CjenikDto } from '@src/app/Models/CjenikDto';
import { CjenikService } from '@src/app/core/Services/Cjenik.service';
import { StavkaCjenikaService } from '@src/app/core/Services/StavkaCjenika.service';
import { kategorijeList } from './StavkaPopUpModel';
import { StavkaCjenikaDto } from '@src/app/Models/StavkaCjenikaDto';

@Component({
  selector: 'app-cjenik-prikaz',
  templateUrl: './cjenik-prikaz.component.html',
  styleUrls: ['./cjenik-prikaz.component.css']
})
export class CjenikPrikazComponent implements OnInit {

  listaCjenika: CjenikDto[];
  currentCjenik: CjenikDto;

  listData: MatTableDataSource<any>;
  displayedColumns: string[] = ['naziv', 'kategorija', 'opis', 'cijenaBezPdva', 'cijenaSaPdvom', 'pdv', 'actions'];

  constructor(
    private cjenikService: CjenikService,
    private _Activatedroute: ActivatedRoute,
    public dialog: MatDialog,
    private stavkaService: StavkaCjenikaService
  ) {}

  ngOnInit() {
      this.getCjeniciList();
  }

  getCjeniciList() {
    this.cjenikService
      .getCjenici()
      .subscribe((data: CjenikDto[]) => {
        this.listaCjenika = data;
      });
  }

  displayStavkeForCurrentCjenik(value: CjenikDto) {
    if (this.listData) {
      this.listData.disconnect();
    }
    this.currentCjenik = value;
    console.log(value.stavkeCjenika.length);
    this.listData = new MatTableDataSource(this.currentCjenik.stavkeCjenika);
    this.listData.connect();
  }

  // PopUpCjenik
  onNew() {
    const dialogRef = this.dialog.open(AddEditCjenikComponent, {
      width: '400px',
      height: '300px',
      data: {
        datumPocetkaPrimjene: ''
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const noviCjenik: CjenikDto = {
          datumPocetkaPrimjene: result.datumPocetkaPrimjene,
          isEditable: true
        };
        this.cjenikService
          .dodajCjenik(noviCjenik)
          .subscribe((data: CjenikDto) => {
            console.log(data);
            this.listaCjenika = [...this.listaCjenika, data];
            this.displayStavkeForCurrentCjenik(data);
          });
      }
    });
  }

  onEdit(cjenik: CjenikDto) {
    console.log(cjenik);
    const dialogRef = this.dialog.open(AddEditCjenikComponent, {
      width: '400px',
      height: '300px',
      data: {
        id: cjenik.id,
        datumPocetkaPrimjene: cjenik.datumPocetkaPrimjene
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const cjenikEdit: CjenikDto = {
          id: result.id,
          datumPocetkaPrimjene: result.datumPocetkaPrimjene
        };

        // update
        this.cjenikService.updateCjenik(cjenikEdit).subscribe((data: any) => {
          this.listaCjenika = [...this.listaCjenika.filter(item => item.id !== data.id), data];
          this.currentCjenik = data;
        });
      }
    });
  }

  delete(cjenik: CjenikDto) {
    this.cjenikService.deleteCjenik(cjenik.id).subscribe((data: any) => {
      this.listaCjenika = [];
      this.currentCjenik = null;
      this.getCjeniciList();
      if (this.listData) {
        this.listData.disconnect();
      }
      this.listData = new MatTableDataSource([]);
    });
  }

  // PopUpStavka
  newStavka() {
        const dialogRef = this.dialog.open(AddEditStavkaComponentComponent, {
          width: '480px',
          height: '650px',
          data: {
            kategorije: kategorijeList,
            opis: '',
            cijenaBezPdva: '',
            cijenaSaPdvom: '',
            pdv: '',
            naziv: ''
          }
        });

        dialogRef.afterClosed().subscribe(result => {
          if (result) {
            const novaStavka: StavkaCjenikaDto = {
              kategorija: result.kategorija,
              opis: result.opis,
              cjenikId: this.currentCjenik.id,
              cijenaBezPdva: result.cijenaBezPdva,
              cijenaSaPdvom: result.cijenaSaPdvom,
              pdv: result.pdv,
              naziv: result.naziv
            };
            this.stavkaService
              .dodajStavku(novaStavka)
              .subscribe((data: any) => {
                this.currentCjenik.stavkeCjenika = [
                  ...this.currentCjenik.stavkeCjenika,
                  data
                ];
                this.listData = new MatTableDataSource(
                  this.currentCjenik.stavkeCjenika
                );
              });
          }
        });
  }

  editStavka(stavka: StavkaCjenikaDto) {
        const dialogRef = this.dialog.open(AddEditStavkaComponentComponent, {
          width: '480px',
          height: '650px',
          data: {
            id: stavka.id,
            kategorija: kategorijeList.find(k => k.id === stavka.kategorija).id,
            kategorije: kategorijeList,
            opis: stavka.opis,
            naziv: stavka.naziv,
            cijenaBezPdva: stavka.cijenaBezPdva,
            cijenaSaPdvom: stavka.cijenaSaPdvom,
            pdv: stavka.pdv
          }
        });

        dialogRef.afterClosed().subscribe(result => {
          if (result) {
            const stavkaEdit: StavkaCjenikaDto = {
              id: result.id,
              kategorija: result.kategorija,
              opis: result.opis,
              naziv: result.naziv,
              cjenikId: this.currentCjenik.id,
              cijenaBezPdva: result.cijenaBezPdva,
              cijenaSaPdvom: result.cijenaSaPdvom,
              pdv: result.pdv
            };

            // update
            this.stavkaService
              .updateStavke(stavkaEdit)
              .subscribe((data: any) => {

                this.currentCjenik.stavkeCjenika = [
                  ...this.currentCjenik.stavkeCjenika.filter(item => item.id !== data.id),
                  data
                ];

                this.listData = new MatTableDataSource(
                  this.currentCjenik.stavkeCjenika
                );
              });
          }
        });
  }

  deleteStavka(stavka: StavkaCjenikaDto) {
    this.stavkaService.deleteStavka(stavka.id).subscribe((data: any) => {
      this.currentCjenik.stavkeCjenika = [
        ...this.currentCjenik.stavkeCjenika.filter(item => item.id !== stavka.id)
      ];

      this.listData = new MatTableDataSource(
        this.currentCjenik.stavkeCjenika
      );
    });
  }

  getNazivKategorije(stavka: StavkaCjenikaDto): string {
    if (stavka.kategorija === 1) {
      return 'Topli napitci';
    } else if (stavka.kategorija === 2) {
      return 'Hladni napitci';
    } else if (stavka.kategorija === 3) {
      return 'Alkoholni napitci';
    } else {
      return 'Ostalo';
    }
  }
}
