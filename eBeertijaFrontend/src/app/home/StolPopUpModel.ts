import { StolDto } from '../Models/StolDto';

export class StolPopUpModel {
    oznakaStola: string;
    serijskiBrojUredaja: string;
    odabranaOrientationStupanj: number;
    orientation: { id: number, stupanj: number, naziv: string } [];
    stariStol: StolDto;
    isEdit: boolean;
    isDeleted: boolean;
}
export const orientationsList: any[] = [
    { 'id': 1, 'stupanj': 0, 'naziv': 'Vodoravno' },
    { 'id': 2, 'stupanj': 90, 'naziv': 'Okomito' }
];
