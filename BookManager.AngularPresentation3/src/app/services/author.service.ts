import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthorParent } from '../model/AuthorParent';
import { Author } from '../model/Author';

@Injectable({
  providedIn: 'root'
})
export class AuthorService {

  private apiUrl = 'http://localhost:5258/author';

  constructor(private http: HttpClient) { }

  getAuthors(author:Author):Observable<AuthorParent>{
    return this.http.get<AuthorParent>(`${this.apiUrl}/${author.name}`);
  }
}
