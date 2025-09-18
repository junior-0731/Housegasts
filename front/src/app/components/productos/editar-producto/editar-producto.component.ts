import { Component, inject, Input, numberAttribute, OnInit } from '@angular/core';
import { ProductoCreacionDTO, ProductoDTO } from '../Productos';
import { FormularioProductoComponent } from '../formulario-producto/formulario-producto.component';
import { ProductosService } from '../Productos.service';
import { MostrarErroresComponent } from "../../../compartidos/componentes/mostrar-errores/mostrar-errores.component";
import { Router } from '@angular/router';

@Component({
  selector: 'app-editar-producto',
  imports: [FormularioProductoComponent, MostrarErroresComponent],
  templateUrl: './editar-producto.component.html',
  styleUrl: './editar-producto.component.css'
})
export class EditarProductoComponent implements OnInit {
  private router = inject(Router);
  ngOnInit(): void {
    this.productosService.obtenerPorId(this.id).subscribe(producto=>{
      this.producto = producto;
      

    })
  }
  @Input({transform:numberAttribute})
  id!:number;
  producto!:ProductoCreacionDTO ;
  productosService = inject(ProductosService);
  errores:string[] = [];

  guardarCambios(producto:ProductoCreacionDTO){
    this.productosService.actualizar(this.id, producto).subscribe({
      next:()=>{
        this.router.navigate(['/products']);
      },
      error:(error)=>{
        this.errores = error.error;
      }
    })
  }
  
  
}
