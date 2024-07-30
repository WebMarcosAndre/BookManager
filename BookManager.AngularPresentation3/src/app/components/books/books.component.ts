import { Component, OnInit } from '@angular/core';
import { BooksService } from '../../services/books.service';
import { CommonModule } from '@angular/common';
import { BookParent } from '../../../BookParent';
import { Book } from '../../../Book';
import { BookItemComponent } from '../book-item/book-item.component';
import { AddBookComponent } from '../add-book/add-book.component';

@Component({
  selector: 'app-books',
  standalone: true,
  imports: [CommonModule, BookItemComponent, AddBookComponent],
  templateUrl: './books.component.html',
  styleUrl: './books.component.css'
})
export class BooksComponent implements OnInit{

  bookParent? : BookParent;
  books : Book[]=[];

  constructor(private booksService:BooksService){}

  ngOnInit():void{
    this.booksService.getBooks().subscribe((data)=>{
      this.bookParent = data;
      this.books = data.books;
      console.log(data.books);
      console.log(this.bookParent.books[0]);
    });
  }

  deleteBook(book:Book){
    this.booksService.deleteBook(book).subscribe(()=>
      (this.books = this.books.filter((b)=>b.id!==book.id)));
  }

  addBook(book:Book){
    this.booksService.addBook(book).subscribe((book)=>{
      console.log("cheguei aqui")
      this.books.push(book);
      alert("Novo livro incluído com sucesso");
    });
  }
}
