import { Component, inject } from '@angular/core';
import { ProductosService } from './Productos.service';

@Component({
  selector: 'app-productos',
  imports: [],
  templateUrl: './productos.component.html',
  styleUrl: './productos.component.css'
})
export class ProductosComponent {
  productsService = inject(ProductosService)
  constructor(){
    this.productsService.obtenerTodos().subscribe(products =>{
      console.log(products);
    })

  }
}