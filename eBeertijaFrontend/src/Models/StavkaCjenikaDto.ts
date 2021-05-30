export class StavkaCjenikaDto {
    id?: number;
    cjenikId: number;
    naziv: string;
    opis: string;
    kategorija: Kategorije;
    cijenaSaPdvom: number;
    cijenaBezPdva: number;
    pdv: number;
    kolicina?: number;
    ukupnoNewRacun?: number;
}

export enum Kategorije {
    HOT = 1, COLD = 2, ALCOHOL = 3, ELSE = 4
}
