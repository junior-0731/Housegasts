import { Routes } from '@angular/router';
import { LandingComponent } from './compartidos/landing/landing.component';
import { ProductosComponent } from './components/productos/productos.component';
import { CrearProductoComponent } from './components/productos/crear-producto/crear-producto.component';
import { EditarProductoComponent } from './components/productos/editar-producto/editar-producto.component';

export const routes: Routes = [
    {path:"", component:LandingComponent},
    {path:"products", component:ProductosComponent},
    {path:"products/create", component:CrearProductoComponent},
    {path:"products/editar/:id", component:EditarProductoComponent},
/*     {path:"categorys", component:CrearGenerosComponent},
    {path:"generos/editar/:id", component:EditarGenerosComponent}, */
   /*  <{path:"**", component:ComponentError}> */
];


   