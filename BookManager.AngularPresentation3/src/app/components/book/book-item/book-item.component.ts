import { Component, Input ,Output, EventEmitter} from '@angular/core';
import { Book } from '../../../model/Book';
import {MatIconModule} from '@angular/material/icon';
import {MatDividerModule} from '@angular/material/divider';
import {MatButtonModule} from '@angular/material/button';

@Component({
  selector: 'app-book-item',
  standalone: true,
  imports: [MatButtonModule, MatDividerModule, MatIconModule],
  templateUrl: './book-item.component.html',
  styleUrl: './book-item.component.css'
})
export class BookItemComponent {

  @Input() book!:Book;
  @Output() deleteBook = new EventEmitter<Book>();
  @Output() selectToChangeBook = new EventEmitter<Book>();  
  count:number=0;
  // authors:string="teste, teste, teste";

    
  onDeleteBook(book:Book){
    this.deleteBook.emit(book);
  }

  onSelectToChangeBook(book:Book){  
    this.selectToChangeBook.emit(book);
  }

}
