import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';

import { SharedRoutingModule } from './shared-routing.module';
import { NativeScriptCommonModule } from 'nativescript-angular/common';


@NgModule({
  declarations: [],
  imports: [
    SharedRoutingModule,
    NativeScriptCommonModule
  ],
  schemas: [NO_ERRORS_SCHEMA]
})
export class SharedModule { }
