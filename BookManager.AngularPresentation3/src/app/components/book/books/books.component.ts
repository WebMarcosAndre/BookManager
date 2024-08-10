import { Component, OnInit, ViewChild } from '@angular/core';
import { BooksService } from '../../../services/books.service';
import { CommonModule } from '@angular/common';
import { BookParent } from '../../../model/BookParent';
import { Book } from '../../../model/Book';
import { BookItemComponent } from '../book-item/book-item.component';
import { AddBookComponent } from '../add-book/add-book.component';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-books',
  standalone: true,
  imports: [CommonModule, BookItemComponent, AddBookComponent],
  templateUrl: './books.component.html',
  styleUrl: './books.component.css'
})
export class BooksComponent implements OnInit {
  @ViewChild(AddBookComponent) addBookComponent!: AddBookComponent;
  bookParent?: BookParent;
  books: Book[] = [];
  bookToUpdate!: Book;
  count: number = 0;

  constructor(private booksService: BooksService, private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.booksService.getBooks().subscribe((data) => {
      this.bookParent = data;
      this.books = data.books;
    });
  }

  onDeleteBook(book: Book) {
    this.booksService.deleteBook(book).subscribe(() =>
      (this.books = this.books.filter((b) => b.id !== book.id)));
  }

  onAddBook(book: Book) {

    book.authorIds = book?.authors.map(author => author.id);

    this.booksService.addBook(book).subscribe((book) => {
      this.books.push(book);
      alert("Novo livro incluído com sucesso");
    });
  }

  onUpdateBook(book: Book) {

    book.authorIds = book?.authors.map(author => author.id);

    this.booksService.updateBook(book).subscribe((bookUpdate) => {

      this.booksService.getBooks().subscribe((data) => {
        this.bookParent = data;
        this.books = data.books;
      });

      alert("Livro atualizado com sucesso");
    });

  }

  onSelectToChangeBook(book: Book) {
    this.addBookComponent.LoadingForm(book);
  }
}
