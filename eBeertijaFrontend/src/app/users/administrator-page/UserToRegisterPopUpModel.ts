export class UserToRegisterPopUpModel {
    id?: number;
    email?: string;
    fullName?: string;
    username?: string;
    oib?: string;
    password?: string;
    passwordRepeat?: string;
    vrstaUseraId?: number;
    vrstaUsera?: { id: number, naziv: string } [];
    isUpdateMode?: boolean;
    // passwordAdmin?: string;
}
export const vrsteList: any[] = [
    { 'id': 1, 'naziv': 'ADMINISTRATOR' },
    { 'id': 2, 'naziv': 'VODITELJ'},
    { 'id': 3, 'naziv': 'KONOBAR'}
];
