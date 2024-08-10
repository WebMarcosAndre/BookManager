import { Component, Output, EventEmitter, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';

import { CommonModule } from '@angular/common';
import { Book } from '../../../model/Book';
import { AddAuthorBookComponent } from '../../author/add-author-book/add-author-book.component';

@Component({
  selector: 'app-add-book',
  standalone: true,
  imports: [FormsModule, CommonModule, ReactiveFormsModule, AddAuthorBookComponent],
  templateUrl: './add-book.component.html',
  styleUrl: './add-book.component.css'
})
export class AddBookComponent implements OnInit {
  form: FormGroup;
  book: Book;

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      title: [''],
      publisherBook: [''],
      edition: [''],
      yearPublication: ['']
    });

    this.book = {
      id: 0,
      title: '',
      publisherBook: '',
      edition: 0,
      yearPublication: '',
      authors: []
    };

    /*
     this.myBook = {
      id: 1,
      title: 'The Great Gatsby',
      publisherBook: 'Scribner',
      edition: 1,
      yearPublication: '1925',
      authors: [
        { id: 1, name: 'F. Scott Fitzgerald' }
      ]
    };
     */
  }

  @Output() addBook = new EventEmitter<Book>();
  @Output() updateBook = new EventEmitter<Book>();

  showAddBook: boolean = false;
  btnValueIncluirNovo: string = 'Incluir novo livro';

  ngOnInit(): void {

  }

  LoadingForm(book: Book) {
    this.showAddBook = false;
    this.toggleForm();
    this.form.patchValue(book);
    this.book = book;
  }

  onSubmit() {

    if (!this.form.value.title) {
      alert("Preencha o título")
      return;
    }
    if (!this.form.value.publisherBook) {
      alert("Preencha o editora")
      return;
    }
    if (!this.form.value.edition) {
      alert("Preencha a edição")
      return;
    }
    if (!this.form.value.yearPublication) {
      alert("Preencha o ano de publicação")
      return;
    }

    if (this.book?.id && this.book?.id) {

      this.book.title = this.form.value.title;
      this.book.publisherBook = this.form.value.publisherBook;
      this.book.edition = this.form.value.edition;
      this.book.yearPublication = this.form.value.yearPublication;

      this.updateBook.emit(this.book);
    } else {      
      
      this.form.value.authors = this.book?.authors;    
      this.addBook.emit(this.form.value);

    }
    this.toggleForm();
  }

  toggleForm() {

    window.scrollTo({ top: 0, behavior: 'smooth' });
    this.book = {
      id: 0,
      title: '',
      publisherBook: '',
      edition: 0,
      yearPublication: '',
      authors: [],
      authorIds:[]
    };
    this.form.reset();

    this.showAddBook = !this.showAddBook;

    if (this.showAddBook) {
      this.btnValueIncluirNovo = 'Fechar';
    } else {
      this.btnValueIncluirNovo = 'Incluir novo livro';
    }
  }

}
