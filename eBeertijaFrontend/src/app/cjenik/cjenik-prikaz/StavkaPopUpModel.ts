
export class StavkaPopUpModel {
    id?: number;
    kategorija: number;
    kategorije: { id: number, naziv: string };
    cijenaBezPdva: number;
    opis: string;
    cijenaSaPdvom: number;
    pdv: number;
    naziv: string;
}
export const kategorijeList: any[] = [
    { id: 1, naziv: 'Topli napitci' },
    { id: 2, naziv: 'Hladni napitci' },
    { id: 3, naziv: 'Alkohol' },
    { id: 4, naziv: 'Ostalo'}
  ];