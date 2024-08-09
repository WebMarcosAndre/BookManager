import { Component, Input ,Output, EventEmitter} from '@angular/core';
import { Book } from '../../model/Book';

@Component({
  selector: 'app-book-item',
  standalone: true,
  imports: [],
  templateUrl: './book-item.component.html',
  styleUrl: './book-item.component.css'
})
export class BookItemComponent {

  @Input() book!:Book;
  @Output() deleteBook = new EventEmitter<Book>();
  @Output() selectToChangeBook = new EventEmitter<Book>();  

  authors:string="teste, teste, teste";

  onDeleteBook(book:Book){
    this.deleteBook.emit(book);
  }

  onSelectToChangeBook(book:Book){  
    this.selectToChangeBook.emit(book);
  }

}
