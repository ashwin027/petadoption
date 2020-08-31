import {UserPet} from './userPet';
import {Adoption} from './adoption';

export class UserPetExtended extends UserPet{
    adoption?: Adoption;
    adoptionStatus?: string;
}