import { User } from "./user";

export class UserParams
{
  gender?:string;
  minAge = 0;
  maxAge = 77;
  pageNumber = 1;
  pageSize = 5;
  userType:string;
  orderBy ='lastActive'
  constructor(user: User)
  {
    this.gender = 'female';
    // this.gender = user.gender === 'female'? 'male': 'female';
    this.userType = 'Patient';
  }
}
