export class UserDto {
    id: number;
    username: string;
    password?: string;
    passwordConfirm?: string;
    email: string;
    fullName: string;
    oib: string;
    role?: number;
    vrsta: VrstaUsera;
    isPasswordUpdate: boolean;
    previousPassword?: string;
}

export enum VrstaUsera {
    ADMINISTRATOR = 1, VODITELJ = 2, KONOBAR = 3
}
