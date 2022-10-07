
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Member } from '../_models/member';
import { of, pipe } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { AccountService } from './account.service';
import { User } from '../_models/user';
import {Observable} from 'rxjs';
import { Triage } from '../_models/triage';
import { Billing } from '../_models/billing';
import { Test } from '../_models/test';
import { Medication } from '../_models/medication';
import { PaginatedResult } from '../_models/pagination';
import { UserParams } from '../_models/userParams';
import { EmployeeParams } from '../_models/employeeParams';

const httpOptions = {
  headers: new HttpHeaders({
    Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('user'))?.token
  })
}
@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;
  members: Member[] = [];
  triage: Triage[] = [];
  billing: Billing[] = [];
  test: Test[] = [];
  medication: Medication[] = [];
  memberCache = new Map();
  //Currently logged in user
  user: User;
  userParams: UserParams;
  employeeParams: EmployeeParams;

  constructor(private http: HttpClient, private accountService: AccountService)
  {
    //Uses pipe to connect to account
    // and retrieve information from currently logged in user
    this.accountService.currentUser$.pipe(take(1)).subscribe(user =>
    {
      this.user = user;
      this.userParams = new UserParams(user);
      this.employeeParams = new EmployeeParams(user);
    })
  }

  //Returns parameters of a particular users
  getUserParams()
  {
    return this.userParams;
  }

  //Sets parameters to filter information from a particular user
  setUserParams(params: UserParams)
  {
    this.userParams = params;
  }


  setEmployeeParam(params: UserParams)
  {
    this.employeeParams = params;
  }


    //Returns parameters of a particular users
    getEmployeeParam()
    {
      return this.employeeParams;
    }

  //Resets parameters upon reseting the filter
  resetUserParams()
  {
    this.userParams = new UserParams(this.user);
    return this.userParams;
  }

    //Resets parameters upon reseting the filter
    resetEmployeeParams()
    {
      this.employeeParams = new EmployeeParams(this.user);
      return this.employeeParams;
    }

  //Get members according to the parameters set
  getMembers(userParams: UserParams)
  {
    var response = this.memberCache.get(Object.values(userParams).join('-'));
    if (response)
    {
      return of(response);
    }
    let params = this.getPaginationHeaders(userParams.pageNumber, userParams.pageSize);

    params = params.append('minAge', userParams.minAge.toString());
    params = params.append('maxAge', userParams.maxAge.toString());
    params = params.append('gender', userParams.gender);
    params = params.append('UserType', userParams.userType);
    params = params.append('orderBy', userParams.orderBy);

    return this.getPaginatedResult<Member[]>(this.baseUrl + 'users', params).
      pipe(map(response =>
    {
      this.memberCache.set(Object.values(userParams).join('-'), response);
      return response;
    }))
  }

  getEmployee(employeeParams: EmployeeParams)
  {
    var response = this.memberCache.get(Object.values(employeeParams).join('-'));
    if (response)
    {
      return of(response);
    }
    let allParams = this.getPaginationHeaders(employeeParams.pageNumber, employeeParams.pageSize);

    allParams = allParams.append('minAge', employeeParams.minAge.toString());
    allParams = allParams.append('maxAge', employeeParams.maxAge.toString());
    allParams = allParams.append('UserType', employeeParams.userType.toString());
    allParams = allParams.append('orderBy', employeeParams.orderBy);

    return this.getPaginatedResult<Member[]>(this.baseUrl + 'employee', allParams).
    pipe(map(response =>
    {
      this.memberCache.set(Object.values(employeeParams).join('-'), response);
      return response;
    }))
  }

  setAppointment(username: string)
  {
    return this.http.post(this.baseUrl + 'Appointment/' + username, {});
  }

  getAppointments(predicate: string) {
    return this.http.get<Partial<Member[]>>(this.baseUrl + 'Appointments?predicate=' + predicate);

  }

  //Function to enable pagination according to user parameters
  private getPaginatedResult<T>(url, params) {
    const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>();
    return this.http.get<T>(url, { observe: 'response', params }).pipe(
      map(response => {
        paginatedResult.result = response.body;
        if (response.headers.get('Pagination') !== null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return paginatedResult;
      })
    );

  }

  //Function to enable pagination
  //This sets up total number of page in the variable pagenumber
  //Also sets the number of item in each page
  private getPaginationHeaders(pageNumber: number, pageSize: number)
  {
    let params = new HttpParams();
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    return params;
  }


  //Function to get information of a particular member using their username
  getMember(username : string)
  {
    //Store the information of the member in cache
    //This helps in faster retrieval of information upon refreshing the page
    const member =[...this.memberCache.values()].
      reduce((array, element) => array.concat(element.result),[])
      .find((member:Member) => member.username === username);

    if(member)
    {
      return of(member);
    }

    // const member = this.members.find(x => x.username === username);
    // if (member !== undefined) return of(member);
    return this.http.get<Member>(this.baseUrl + 'users/'+ username);
  }


//Function to update information of a particular member
  updateMember(member: Member)
  {
    return this.http.put(this.baseUrl+'users',member).pipe(
      map(() =>
      {
        const index = this.members.indexOf(member);
        this.members[index] = member;
      })
    )
  }


  //This function returns all the triage information of a user
  //Takes parameter of user id
  getTriage(id : number)
  {
    const triage = this.triage.find(x => x.id === id);
    if (triage !== undefined) return of(triage);
    return this.http.get<Triage>(this.baseUrl + 'patientinfo/triage/'+ id);
  }

  //This function returns the list of triage data to all the users
  getTriages()
  {
    if (this.triage.length > 0) return of(this.triage);
    return this.http.get<Triage[]>(this.baseUrl + 'patientinfo/triage').pipe(
      map(triage =>
      {
        this.triage = triage;
        return triage;
      })
    );
  }

  //This function returns all the billing items of a user
  //Takes parameter of user id
  getBilling(id : number)
  {
    const billing = this.billing.find(x => x.id === id);
    if (billing !== undefined) return of(billing);
    return this.http.get<Billing>(this.baseUrl + 'patientinfo/billing/'+ id);
  }

  //This function returns all the list of billings prescribed to all the users
  getBillings()
  {
    if (this.billing.length > 0) return of(this.billing);
    return this.http.get<Billing[]>(this.baseUrl + 'patientinfo/billing').pipe(
      map(billing =>
      {
        this.billing = billing;
        return billing;
      })
    );
  }


  //This function returns all the list of test result of a user
  //Takes parameter of user id
  getTest(id : number)
  {
    const test = this.test.find(x => x.id === id);
    if (test !== undefined) return of(test);
    return this.http.get<Test>(this.baseUrl + 'patientinfo/test/'+ id);
  }

  //This function returns all the list of test results prescribed to all the users
  getTests()
  {
    if (this.test.length > 0) return of(this.test);
    return this.http.get<Test[]>(this.baseUrl + 'patientinfo/test').pipe(
      map(test =>
      {
        this.test = test;
        return test;
      })
    );
  }

  //This function returns all the list of medications prescribed to a user
  //Takes parameter of user id
  getMedication(id : number)
  {
    const medication = this.medication.find(x => x.id === id);
    if (medication !== undefined) return of(medication);
    return this.http.get<Medication>(this.baseUrl + 'patientinfo/medication/'+ id);
  }

  //This function returns all the list of medications prescribed to all the users
  getMedications()
  {
    if (this.medication.length > 0) return of(this.medication);
    return this.http.get<Medication[]>(this.baseUrl + 'patientinfo/medication').pipe(
      map(medication =>
      {
        this.medication = medication;
        return medication;
      })
    );
  }
}
