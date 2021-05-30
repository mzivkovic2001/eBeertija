import { StavkaCjenikaDto } from './StavkaCjenikaDto';
import { StavkaRacunaDto } from './StavkaRacunaDto';

export class NewRacunDto {
    broj: number;
    brojOpis: string;
    stavkeCjenikaNaSnazi: StavkaCjenikaDto[];
    stavkeRacuna: StavkaRacunaDto[];
    stolId: number;
    oznakaStola: string;
    ukupnoSaPdvom?: number;
}
