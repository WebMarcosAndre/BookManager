import { Component, Output, EventEmitter, Input, OnChanges, SimpleChanges, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';

import { CommonModule } from '@angular/common';
import { Book } from '../../model/Book';

@Component({
  selector: 'app-add-book',
  standalone: true,
  imports: [FormsModule, CommonModule, ReactiveFormsModule],
  templateUrl: './add-book.component.html',
  styleUrl: './add-book.component.css'
})
export class AddBookComponent {
  form: FormGroup;
  book?: Book;
  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      title: [''],
      publisherBook: [''],
      edition: [''],
      yearPublication: ['']
    });
  }

  @Output() addBook = new EventEmitter<Book>();
  @Output() updateBook = new EventEmitter<Book>();

  showAddBook: boolean = false;
  btnValueIncluirNovo: string = 'Incluir novo livro';

  LoadingForm(book: Book) {
    this.showAddBook = false;
    this.toggleForm();
    this.form.patchValue(book);
    this.book = book;
  }

  onSubmit() {

    // if (!this.form.value.title) {
    //   alert("Preencha o título")
    //   return;
    // }
    // if (!this.form.value.publisherBook) {
    //   alert("Preencha o editora")
    //   return;
    // }
    // if (!this.form.value.edition) {
    //   alert("Preencha a edição")
    //   return;
    // }
    // if (!this.form.value.yearPublication) {
    //   alert("Preencha o ano de publicação")
    //   return;
    // }

    if (this.book?.id) {

      this.book.title = this.form.value.title;
      this.book.publisherBook = this.form.value.publisherBook;
      this.book.edition = this.form.value.edition;
      this.book.yearPublication = this.form.value.yearPublication;

      this.updateBook.emit(this.book);
    } else {
      this.addBook.emit(this.form.value);
    }
    this.toggleForm();
  }

  toggleForm() {
    
    window.scrollTo({ top: 0, behavior: 'smooth' });
    this.book = undefined;
    this.form.reset();

    this.showAddBook = !this.showAddBook;

    if (this.showAddBook) {
      this.btnValueIncluirNovo = 'Fechar';
    } else {
      this.btnValueIncluirNovo = 'Incluir novo livro';
    }
  }
}
