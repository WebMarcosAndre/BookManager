import { Component, Output,EventEmitter  } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Book } from '../../../Book';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-book',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './add-book.component.html',
  styleUrl: './add-book.component.css'
})
export class AddBookComponent {
  @Output() onAddBook= new EventEmitter<Book>();
  title:string='';  
  publisherBook:string='';
  edition:number=0;
  yearPublication:string='';
  showAddBook:boolean=false;
  btnValueIncluirNovo:string='Incluir novo livro';

  onSubmit(){
    if(!this.title){
      alert("Preencha o título")
      return;
    }
    if(!this.publisherBook){
      alert("Preencha o editora")
      return;
    }
    if(!this.edition){
      alert("Preencha a edição")
      return;
    }
    if(!this.yearPublication){
      alert("Preencha o ano de publicação")
      return;
    }

    const newBook = {
      title : this.title,
      publisherBook:this.publisherBook,
      edition:this.edition,
      yearPublication:this.yearPublication
    }

    this.onAddBook.emit(newBook);
    this.title = "";
    this.publisherBook = "";
    this.edition = 0;
    this.yearPublication = "";

   
  } 
  
  onClick(){
    this.showAddBook = !this.showAddBook;
    if(this.showAddBook){
      this.btnValueIncluirNovo='Fechar';
    }else{
      this.btnValueIncluirNovo='Incluir novo livro';
    }
  }
}
