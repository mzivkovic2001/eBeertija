export class StavkaRacunaDto {
    id?: number;
    racunId?: number;
    stavkaCjenikaId: number;
    jedinicnaCijenaStavke?: number;
    kolicina: number;
    ukupnoSaPdvom?: number;
    ukupnoBezPdva?: number;
    iznosPdv?: number;
    nazivStavke?: string;
}
