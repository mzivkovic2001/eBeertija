import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, Validators, FormControl, FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { StolPopUpModel } from '../home/StolPopUpModel';

@Component({
  selector: 'app-add-stol',
  templateUrl: './add-stol.component.html',
  styleUrls: ['./add-stol.component.css']
})
export class AddStolComponent implements OnInit {

  addEditStolForm: FormGroup;
  submitted = false;
  oznakaStola =  new FormControl(this.data.oznakaStola, Validators.required);
  orientation =  new FormControl(this.data.odabranaOrientationStupanj, Validators.required);
  serijskiBrojUredaja = new FormControl(this.data.serijskiBrojUredaja);
  constructor(public dialogRef: MatDialogRef<AddStolComponent>,
    @Inject(MAT_DIALOG_DATA) public data: StolPopUpModel, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.addEditStolForm = this.formBuilder.group({
      oznakaStola: this.oznakaStola,
      orientation: this.orientation,
      serijskiBrojUredaja: this.serijskiBrojUredaja
  });
  }

  onNoClick(): void {
    this.dialogRef.close(null);
  }
 
  onDelete(): void {
  this.data.isDeleted = true;
  this.dialogRef.close(this.data);
 }

 onSubmit() {
  console.log('submitting...');
  this.submitted = true;

  // stop here if form is invalid
  if (this.addEditStolForm.invalid) {
      return;
  }

  this.data.oznakaStola = this.oznakaStola.value;
  this.data.odabranaOrientationStupanj = this.orientation.value;
  this.data.serijskiBrojUredaja = this.serijskiBrojUredaja.value;

  this.dialogRef.close(this.data);
}

}
