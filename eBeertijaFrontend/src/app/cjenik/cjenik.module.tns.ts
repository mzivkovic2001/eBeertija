import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { CjenikRoutingModule } from './cjenik-routing.module';
import { NativeScriptCommonModule } from 'nativescript-angular/common';

@NgModule({
  declarations: [],
  imports: [
    CjenikRoutingModule,
    NativeScriptCommonModule
  ],
  schemas: [NO_ERRORS_SCHEMA]
})
export class CjenikModule { }
