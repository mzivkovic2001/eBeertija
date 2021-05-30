import { NarudzbaDto } from './NarudzbaDto';
import { StavkaRacunaDto } from './StavkaRacunaDto';

export class RacunIzvjestajDto {
    id?: number;
    broj?: number;
    brojOpis?: string;
    datum?: Date;
    narudzbaId?: number;
    narudzba?: NarudzbaDto;
    ukupnoSaPdvom?: number;
    ukupnoBezPdva?: number;
    iznosPdv?: number;
    stolId?: number;
    oznakaStola?: string;
    userId?: number;
    isStorniran?: boolean;
    storniraniRacunId?: number;
    brojStorniranogRacuna?: string;
    nazivKorisnika?: string;
    stavkeRacuna?: StavkaRacunaDto[];
}
