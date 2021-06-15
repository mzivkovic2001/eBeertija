import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from '@src/app/core/Services/user.service';
import { UserDto } from '@src/app/Models/UserDto';
import { MatTableDataSource, MatSort, MatDialog } from '@angular/material';
import { RegistracijaPopUpComponent } from '../registracija-pop-up/registracija-pop-up.component';
import { vrsteList } from './UserToRegisterPopUpModel';
import { RegistrationUserDto } from '@src/app/Models/RegistrationUserDto';
import { AuthenticationService } from '@src/app/core/Services/authentication.service';

@Component({
  selector: 'app-administrator-page',
  templateUrl: './administrator-page.component.html',
  styleUrls: ['./administrator-page.component.css']
})
export class AdministratorPageComponent implements OnInit {
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  isLoaded: boolean;
  isEdit: boolean;

  constructor(private userService: UserService, public dialog: MatDialog, private authService: AuthenticationService) { }
  korisniciList: UserDto[];

  korisnicilistData: MatTableDataSource<any>;
  displayedColumns: string[] = ['korisnickoIme', 'fullName', 'oib', 'email', 'uloga', 'actions'];
  trenutniUser: UserDto;

  ngOnInit() {
    this.displayKorisniciList();

    if(this.authService.isLoggedin()) {
      this.trenutniUser = this.authService.getUser();
    }
  }

  displayKorisniciList() {
    this.isLoaded = false;
    this.userService.getAll().subscribe(lista => {
      this.korisniciList = lista;
      this.korisnicilistData = new MatTableDataSource(this.korisniciList);
      this.korisnicilistData.sort = this.sort;
      this.isLoaded = true;
    });
  }

  getStringOfUloga(uloga: number): string {
    if (uloga === 1) {
      return 'ADMINISTRATOR';
    } else if (uloga === 2) {
      return 'VODITELJ';
    } else {
      return 'KONOBAR';
    }
  }

  onNew() {
    this.isEdit = false;
    const dialogRef = this.dialog.open(RegistracijaPopUpComponent, {
      width: '500px',
      height: '620px',
      data: {
        vrstaUsera: vrsteList.filter(v => v['id'] >= this.trenutniUser.role),
        fullName: '',
        oib: '',
        username: '',
        email: '',
        password: '',
        passwordRepeat: '',
        isUpdateMode: false
      }
  });

    dialogRef.afterClosed().subscribe(result => {
    console.log('The dialog was closed' + result);
    if (result) {
    const noviKorisnik: RegistrationUserDto = {
      fullName: result.fullName,
      oib: result.oib,
      username: result.username,
      email: result.email,
      password: result.password,
      vrsta: result.vrstaUseraId
    };

    this.userService.register(noviKorisnik).subscribe((data: any) => {
      console.log(data);
      this.korisniciList = [...this.korisniciList, data];
      this.displayKorisniciList();
    });
  }
  });
  }

  updateKorisnik(korisnikUpdate: UserDto) {
    this.isEdit = true;
    const dialogRef = this.dialog.open(RegistracijaPopUpComponent, {
      width: '500px',
      height: '500px',
      data: {
      id: korisnikUpdate.id,
      isUpdateMode: true,
      oib: korisnikUpdate.oib,
      username: korisnikUpdate.username,
      email: korisnikUpdate.email,
      vrstaUseraId: korisnikUpdate.vrsta,
      vrstaUsera: vrsteList,
      fullName: korisnikUpdate.fullName
    }
  });
  
    dialogRef.afterClosed().subscribe(result => {
    console.log('The dialog was closed' + result);
    const currentKorisnik: UserDto = {
      id: result.id,
      vrsta: result.vrstaUseraId,
      oib: result.oib,
      username: result.username,
      email: result.email,
      fullName: result.fullName,
      isPasswordUpdate: false
    };

    console.log(currentKorisnik);
    this.userService.update(currentKorisnik.id, currentKorisnik).subscribe((data: any) => {
      this.korisniciList = [...this.korisniciList, data];
      this.displayKorisniciList();
    });
  });
  }

  deleteKorisnik(korisnik: UserDto) {
    this.authService.delete(korisnik.id).subscribe((data: any) => {
      this.korisniciList = [
        ...this.korisniciList.filter(item => item.id !== korisnik.id)
      ];

      this.korisnicilistData = new MatTableDataSource(this.korisniciList);
    });
  }
}
