import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProductosService } from '../Productos.service';
import { ProductoDTO } from '../Productos';
import { ItemProductoComponent } from "../item-producto/item-producto.component";

@Component({
  selector: 'app-mostrar-productos',
  imports: [ItemProductoComponent],
  templateUrl: './mostrar-productos.component.html',
  styleUrl: './mostrar-productos.component.css'
})
export class MostrarProductosComponent implements OnInit {
    private router = inject(Router);/* Esto nos devuelve una instancia de la clase */
    private productosService = inject(ProductosService);
    public productos : ProductoDTO[] = []
    ngOnInit(): void {
      alert('dccc')
      this.productosService.obtenerTodos().subscribe(((productos:ProductoDTO[])=>{
        this.productos = productos
        console.log(productos)
      }))
      
    }


}
