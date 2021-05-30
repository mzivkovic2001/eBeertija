import { Component, OnInit, Inject, Input, ChangeDetectorRef, DoCheck } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { StavkaPopUpModel } from '../cjenik-prikaz/StavkaPopUpModel';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-edit-stavka-component',
  templateUrl: './add-edit-stavka-component.component.html',
  styleUrls: ['./add-edit-stavka-component.component.css']
})
export class AddEditStavkaComponentComponent implements OnInit, DoCheck {
  constructor(public dialogRef: MatDialogRef<AddEditStavkaComponentComponent>,
    @Inject(MAT_DIALOG_DATA) public data: StavkaPopUpModel, private formBuilder: FormBuilder, private cdRef: ChangeDetectorRef) { }

  addStavkaForm: FormGroup;
  submitted = false;

  kategorija = new FormControl(this.data.kategorija, Validators.required);
  opis = new FormControl(this.data.opis, Validators.required);
  naziv = new FormControl(this.data.naziv, Validators.required);
  cijenaBezPdva = new FormControl(this.data.cijenaBezPdva, Validators.required);
  cijenaSaPdvom = new FormControl(this.data.cijenaSaPdvom, Validators.required);
  pdv = new FormControl(this.data.pdv);

  ngOnInit() {
    this.addStavkaForm = this.formBuilder.group({
      kategorija: this.kategorija,
      opis: this.opis,
      naziv: this.naziv,
      cijenaBezPdva: this.cijenaBezPdva,
      cijenaSaPdvom: this.cijenaSaPdvom,
      pdv: this.pdv
  });
  }

  ngDoCheck() {
    if (this.cijenaBezPdva.valueChanges) {
      this.cdRef.detectChanges();

      this.cijenaSaPdvom.setValue(this.cijenaBezPdva.value * 1.25);
      this.pdv.setValue(this.cijenaSaPdvom.value - this.cijenaBezPdva.value);
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onSubmit() {
    this.submitted = true;
    if (this.addStavkaForm.invalid) {
        return;
    }

    this.data.kategorija = this.kategorija.value;
    this.data.opis = this.opis.value;
    this.data.cijenaBezPdva = this.cijenaBezPdva.value;
    this.data.cijenaSaPdvom = this.cijenaSaPdvom.value;
    this.data.pdv = this.pdv.value;
    this.data.naziv = this.naziv.value;
    this.dialogRef.close(this.data);
  }

  unosCijene(cbp) {
    this.cijenaSaPdvom.setValue(cbp * 1.25);
    const razlika = this.cijenaSaPdvom.value - cbp;
    this.pdv.setValue(razlika);

    console.log(cbp + ' /' + this.cijenaSaPdvom.value + ' /' + this.pdv.value);
  }
}
