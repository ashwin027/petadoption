import {Image} from './image';

export class UserPet{
    id?: number;
    name: string;
    description: string;
    breedId: number;
    userId: string;
    gender: string;
    images?: Array<Image>;
}