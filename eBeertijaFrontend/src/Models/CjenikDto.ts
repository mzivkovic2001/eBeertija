import { StavkaCjenikaDto } from './StavkaCjenikaDto';

export class CjenikDto {
    id?: number;
    datumPocetkaPrimjene: Date;
    isEditable?: boolean;
    stavkeCjenika?: StavkaCjenikaDto[];
}
