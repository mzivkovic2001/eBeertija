import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { UserToRegisterPopUpModel } from '../administrator-page/UserToRegisterPopUpModel';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-registracija-pop-up',
  templateUrl: './registracija-pop-up.component.html',
  styleUrls: ['./registracija-pop-up.component.css']
})
export class RegistracijaPopUpComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<RegistracijaPopUpComponent>,
    @Inject(MAT_DIALOG_DATA) public data: UserToRegisterPopUpModel,
    private formBuilder: FormBuilder) { }

  addUpdateKorisnikForm: FormGroup;
  submitted = false;

  username = new FormControl(this.data.username, Validators.required);
  password = new FormControl(this.data.password, Validators.required);
  passwordRepeat = new FormControl(this.data.passwordRepeat, Validators.required);
  oib = new FormControl(this.data.oib, Validators.required);
  email = new FormControl(this.data.email, Validators.required);
  fullName = new FormControl(this.data.fullName, Validators.required);
  vrstaUsera = new FormControl(this.data.vrstaUseraId, Validators.required);
  isUpdateMode = new FormControl(this.data.isUpdateMode, Validators.nullValidator);

  ngOnInit() {
    if(this.isUpdateMode.value == true) {
      this.password = new FormControl(this.data.password, Validators.nullValidator);
      this.passwordRepeat = new FormControl(this.data.passwordRepeat, Validators.nullValidator);

      this.addUpdateKorisnikForm = this.formBuilder.group({
        username: {value: this.username.value, disabled: this.isUpdateMode.value === true},
        password: this.password,
        passwordRepeat: this.passwordRepeat,
        oib: this.oib,
        email: this.email,
        fullName: this.fullName,
        vrstaUsera: this.vrstaUsera,
        isUpdateMode: this.isUpdateMode
      });
    } else {
      this.addUpdateKorisnikForm = this.formBuilder.group({
        username: this.username,
        password: this.password,
        passwordRepeat: this.passwordRepeat,
        oib: this.oib,
        email: this.email,
        fullName: this.fullName,
        vrstaUsera: this.vrstaUsera,
        isUpdateMode: this.isUpdateMode
      });
    } 
  }

  onSubmit() {
    this.submitted = true;
    if (this.addUpdateKorisnikForm.invalid) {
      return;
    }

    this.data.vrstaUseraId = this.vrstaUsera.value;
    this.data.username = this.username.value;
    this.data.password = this.password.value;
    this.data.oib = this.oib.value;
    this.data.email = this.email.value;
    this.data.fullName = this.fullName.value;

    this.dialogRef.close(this.data);
  }

  onNoClick(): void {
    this.dialogRef.close(null);
  }

}
