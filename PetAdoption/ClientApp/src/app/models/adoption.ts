import { AdoptionAdditionalDetails } from "./adoptionAdditionalDetails";

export class Adoption extends AdoptionAdditionalDetails{
    id?: number;
    adopteeId: string;
    adopterId?: string;
    petId: number;
    petName: string;
    status: string;
    breedName: string;
}