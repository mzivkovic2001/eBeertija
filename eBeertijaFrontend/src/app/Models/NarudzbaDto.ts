import { StavkaNarudzbeDto } from './StavkaNarudzbeDto';

export class NarudzbaDto {
    id?: number;
    datum?: Date;
    ukupnoSaPdvom?: number;
    ukupnoBezPdva?: number;
    pdvIznos?: number;
    broj?: number;
    serijskiBrojUredaja?: string;
    oznakaStola?: string;
    stavkeNarudzbe?: StavkaNarudzbeDto[];
}

