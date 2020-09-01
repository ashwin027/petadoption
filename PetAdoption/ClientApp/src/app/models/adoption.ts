import { AdoptionAdditionalDetails } from "./adoptionAdditionalDetails";

import {AdoptionStatus} from './adoptionStatus';
import { AdopterDetail } from './adopterDetail';

export class Adoption extends AdoptionAdditionalDetails{
    id?: number;
    adopteeId: string;
    adopterId?: string;
    userPetId: number;
    petName: string;
    status: AdoptionStatus;
    breedName: string;
    adopterDetailId?: number;
    adopterDetails?: AdopterDetail;
}