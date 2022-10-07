import { User } from "./user";

export class EmployeeParams
{
  minAge = 0;
  maxAge = 77;
  pageNumber = 1;
  pageSize = 5;
  userType:string;
  orderBy ='lastActive'
  constructor(user: User)
  {
    // this.gender = user.gender === 'female'? 'male': 'female';
    this.userType = 'Physician';
  }
}
