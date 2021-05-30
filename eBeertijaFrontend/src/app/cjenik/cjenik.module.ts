import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CjenikRoutingModule } from './cjenik-routing.module';
import { CjenikPrikazComponent } from './cjenik-prikaz/cjenik-prikaz.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule, MatInputModule, MatFormFieldModule, MatOptionModule, MatSelectModule,
  MatTableModule, MatPaginatorModule, MatSortModule, MatButtonModule, MatIconModule, MatCheckboxModule } from '@angular/material';
import { AddEditCjenikComponent } from './add-edit-cjenik/add-edit-cjenik.component';
import { MaterialModule } from '../material/material.module';
import { AddEditStavkaComponentComponent } from './add-edit-stavka-component/add-edit-stavka-component.component';


@NgModule({
  declarations: [CjenikPrikazComponent, AddEditCjenikComponent, AddEditStavkaComponentComponent],
  imports: [
    CommonModule,
    CjenikRoutingModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatInputModule,
    MatFormFieldModule,
    MatOptionModule,
    MatSelectModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatButtonModule,
    MatIconModule,
    MaterialModule,
    MatCheckboxModule
  ],
  exports: [FormsModule,
     MatDialogModule,
      MatFormFieldModule,
       MatInputModule,
        MatOptionModule,
         MatSelectModule,
         ReactiveFormsModule,
          MatTableModule,
        MatPaginatorModule,
      MatSortModule,
    MatButtonModule,
  MatIconModule,
MaterialModule,
MatCheckboxModule],
  entryComponents: [AddEditCjenikComponent, AddEditStavkaComponentComponent]
})
export class CjenikModule { }
