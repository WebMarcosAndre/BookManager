import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { BooksComponent } from "./components/books/books.component";
import { ReportComponent } from "./components/report/report.component";

const routes:Routes=[
    {path:'', component: BooksComponent},
    {path:'report', component:ReportComponent}
];

@NgModule({
    declarations:[],
    imports:[RouterModule.forRoot(routes)],
    exports:[RouterModule]
})

export class AppRoutingModule{}