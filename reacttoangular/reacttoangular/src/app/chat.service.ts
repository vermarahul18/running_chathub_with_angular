import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import {User} from './User';
import {Message} from './Message';
import {Channel} from './Channel';
import {Workspace} from './Workspace';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class ChatService {

private _chaturl="https://localhost:5001/api/chat/workspaces";///////check port

  constructor(private http: HttpClient) { }

  CreateWorkspace(workspaceName:string): any
  { console.log("in service method");

    return this.http.post(this._chaturl+"/"+workspaceName, {}, {
      headers: new HttpHeaders({
        'Content-Type': 'text/plain',
        'Accept': 'text/plain',
      }),
    })
  }


  addUserToWorkSpace (user: User, workSpaceName:string): Observable<User> {
    const url = `${this._chaturl+"/user"}/${workSpaceName}`;
    return this.http.put(url, user, httpOptions).pipe(
      catchError(this.handleError<any>('addUserToWorkSpace'))
    );
   }
  /**
 * Handle Http operation that failed.
 * Let the app continue.
 * @param operation - name of the operation that failed
 * @param result - optional value to return as the observable result
 */
private handleError<T> (operation = 'operation', result?: T) {
  return (error: any): Observable<T> => {

    // TODO: send the error to remote logging infrastructure
    console.error(error); // log to console instead

    // TODO: better job of transforming error for user consumption
    ///this.log(`${operation} failed: ${error.message}`);

    // Let the app keep running by returning an empty result.
    return of(result as T);
  };
}
}
