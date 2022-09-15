import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class TaskService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getTasks( page: any, itemsPerPage: any ) {
    const paginatedResult: PaginatedResult<Task[] | null> = new PaginatedResult<
      Task[]
    >();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    var id =12;

    return this.http
      .get<Task[] | null>(this.baseUrl + 'users/' + id + '/todo', {
        observe: 'response',
        params
      })
      .pipe(
        map(response => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') !== null) {
            paginatedResult.pagination = JSON.parse(
              response.headers.get('Pagination')!
            );
          }

          return paginatedResult;
        })
      );
  }


  addTask(id: number, task: Task) {
    return this.http.post(this.baseUrl + 'users/' + id + '/todo', task);
  }

  deleteTask(id: number, userId: number) {
    return this.http.post(
      this.baseUrl + 'users/' + userId + '/todo/' + id,
      {}
    );
  }

  markAsDone(userId: number, taskId: number) {
    this.http
      .post(
        this.baseUrl + 'users/' + userId + '/todo/' + taskId + '/done',
        {}
      )
      .subscribe();
  }
}
