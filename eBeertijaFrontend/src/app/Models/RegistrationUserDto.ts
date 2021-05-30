import { VrstaUsera } from './UserDto';

export class RegistrationUserDto {
    username: string;
    password: string;
    fullName: string;
    email: string;
    oib: string;
    vrsta: VrstaUsera;
}
