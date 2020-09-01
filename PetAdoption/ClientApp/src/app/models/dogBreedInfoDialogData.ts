import {DogBreedInfo} from './dogBreedInfo';
import { UserPet } from './userPet';

export class DogBreedInfoDialogData{
    breeds: Array<DogBreedInfo>;
    userPetInfo: UserPet;
    isEdit: boolean;
}