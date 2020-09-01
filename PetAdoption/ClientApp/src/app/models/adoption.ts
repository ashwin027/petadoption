import { AdoptionAdditionalDetails } from "./adoptionAdditionalDetails";

import {AdoptionStatus} from './adoptionStatus';

export class Adoption extends AdoptionAdditionalDetails{
    id?: number;
    adopteeId: string;
    adopterId?: string;
    userPetId: number;
    petName: string;
    status: AdoptionStatus;
    breedName: string;
    adopterDetailId?: number;
}