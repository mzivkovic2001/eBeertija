import { Component, OnInit, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormGroup, Validators, FormControl, FormBuilder } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { CjenikPopUpModel } from '../cjenik-prikaz/CjenikPopUpModel';

@Component({
  selector: 'app-add-edit-cjenik',
  templateUrl: './add-edit-cjenik.component.html',
  styleUrls: ['./add-edit-cjenik.component.css']
})
export class AddEditCjenikComponent implements OnInit {
  submitted = false;
  addCjenikForm: FormGroup;

  constructor(public dialogRef: MatDialogRef<AddEditCjenikComponent>,
    @Inject(MAT_DIALOG_DATA) public data: CjenikPopUpModel, private formBuilder: FormBuilder) { }

   datumPocetkaPrimjene = new FormControl(this.data.datumPocetkaPrimjene, Validators.required);

  ngOnInit() {
    this.addCjenikForm = this.formBuilder.group({
      datumPocetkaPrimjene: this.datumPocetkaPrimjene
  });
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

 onSubmit() {
  this.submitted = true;
  if (this.addCjenikForm.invalid) {
      return;
  }

  this.data.datumPocetkaPrimjene = this.datumPocetkaPrimjene.value;
  this.dialogRef.close(this.data);
}
}
