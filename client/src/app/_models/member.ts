import { Photo } from "./photo";
import {Triage} from "./triage";

  export interface Member {
    _id: string;
    id:number;
    username:string;
    photoUrl:string;
    userType:string;
    email:string;
    gender:string;
    phoneNumber:string;
    city: string;
    country: string;
    lastName:string;
    firstName: string;
    photos: Photo[];
    triage: Triage[];
  }


