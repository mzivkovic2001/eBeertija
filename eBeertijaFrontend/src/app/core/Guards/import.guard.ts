import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ImportGuard  {

 
} export function  throwIfAlreadyLoaded ( parentModule: any, moduleName: string )   { 
  if ( parentModule ) {
  throw new Error(`${moduleName } has already been loaded. Import Core modules in the AppModule AppModule AppModule AppModuleonly.`);
  }
}