import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BookParent } from '../model/BookParent';
import { Book } from '../model/Book';


@Injectable({
  providedIn: 'root'
})
export class BooksService {

  private apiUrl = 'http://localhost:5258/book';
  constructor(private http: HttpClient) { }

  getBooks() : Observable<BookParent>{
    return this.http.get<BookParent>(this.apiUrl)
  }

  deleteBook(book:Book):Observable<Book>{
    return this.http.delete<Book>(`${this.apiUrl}/${book.id}`);
  }

  addBook(book:Book) : Observable<Book>{
    return this.http.post<Book>(`${this.apiUrl}`, book);
  }

  updateBook(book:Book):Observable<Book>{
    console.log("update ", book);
    return this.http.put<Book>(`${this.apiUrl}/${book.id}`, book);
  }
}
