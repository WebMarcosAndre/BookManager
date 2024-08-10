import { Routes } from '@angular/router';
import { BooksComponent } from './components/book/books/books.component';
import { ReportComponent } from './components/report/report.component';

export const routes: Routes = [
    {path:'', component: BooksComponent},
    {path:'report', component:ReportComponent}
];
