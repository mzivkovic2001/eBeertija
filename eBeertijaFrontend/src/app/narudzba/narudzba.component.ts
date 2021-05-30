import { Component, OnInit, ViewChildren, QueryList, AfterViewInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NarudzbaDto } from '../Models/NarudzbaDto';
import { NarudzbaService } from '../core/Services/Narudzba.service';
import { Subject } from 'rxjs';
import { DataTableDirective } from 'angular-datatables';

@Component({
  selector: 'app-narudzba',
  templateUrl: './narudzba.component.html',
  styleUrls: ['./narudzba.component.css']
})
export class NarudzbaComponent implements OnInit, AfterViewInit, OnDestroy {
  currentNarudzbaId: number;
  narudzba: NarudzbaDto;
  isLoaded: boolean;
  dtTrigger: Subject<any> = new Subject();
  options: any = {};
  @ViewChildren(DataTableDirective)
  dtElements: QueryList<DataTableDirective>;

  constructor(private activatedRoute: ActivatedRoute, private narudzbaService: NarudzbaService, private router: Router) { }

  ngOnInit() {
    this.options = {
      // Declare the use of the extension in the dom parameter
      dom: 'Bfrtip',
      // Configure the buttons
      buttons: [
        'copy',
        'print',
        'excel'
      ]
    };
    this.isLoaded = false;
    this.activatedRoute.params.subscribe(params => {
      this.currentNarudzbaId = params.id;
      this.narudzbaService.getNarudzbaById(this.currentNarudzbaId).subscribe(data => {
        this.narudzba = data;
        this.isLoaded = true;
      });
    });
  }

  ngAfterViewInit(): void {
    if (this.dtTrigger) {
      this.dtTrigger.next();
    }
  }

  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    if (this.dtTrigger) {
      this.dtTrigger.unsubscribe();
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
  }

  createRacun() {
    this.narudzbaService.stvoriRacun(this.narudzba.id, this.narudzba).subscribe(data => {
      this.router.navigate(['/home']);
    });
  }

  deleteNarudzba() {
    this.narudzbaService.deleteNarudzba(this.narudzba.id).subscribe(data => {
      this.router.navigate(['/home']);
    });
  }

}
