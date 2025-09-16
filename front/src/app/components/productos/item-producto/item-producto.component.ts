import { Component, inject, Input } from '@angular/core';
import { ProductoDTO } from '../Productos';
import { Router } from '@angular/router';
import { ProductosService } from '../Productos.service';

@Component({
  selector: 'app-item-producto',
  imports: [],
  templateUrl: './item-producto.component.html',
  styleUrl: './item-producto.component.css'
})
export class ItemProductoComponent {
  public productosService = inject(ProductosService);
  public router = inject(Router);
  @Input({required:true})
  producto!:ProductoDTO;
  actualizar(){
    this.router.navigateByUrl(`/products/editar/${this.producto.id}`);
  }
  eliminar(id:number){
    this.productosService.eliminar(id).subscribe({
      next:()=>{
         this.router.navigate(['/products'])
      }
    })
  }

 
}
