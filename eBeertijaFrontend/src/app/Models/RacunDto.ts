import { StavkaRacunaDto } from './StavkaRacunaDto';

export class RacunDto {
    id?: number;
    broj: number;
    brojOpis: string;
    datum?: Date;
    narudzbaId?: number;
    ukupnoSaPdvom?: number;
    ukupnoBezPdva?: number;
    iznosPdv?: number;
    stolId: number;
    oznakaStola: string;
    userId?: number;
    isStorniran: boolean;
    storniraniRacunId?: number;
    nazivKorisnika?: string;
    stavkeRacuna?: StavkaRacunaDto[];
}
